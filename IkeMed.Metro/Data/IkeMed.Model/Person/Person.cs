﻿using IkeCode;
using IkeCode.Core.Xml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity.Migrations;

namespace IkeMed.Model
{
    public class Person : BaseModel
    {
        [Required, MaxLength(250), Display(Name = "Nome")]
        public string Name { get; set; }

        [Index("IX_PEOPLE_EMAIL", IsUnique = true)]
        [Required, MaxLength(250), Display(Name = "Email"), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Médico")]
        public virtual Doctor Doctor { get; set; }

        [Display(Name = "Pessoa Jurídica")]
        public virtual LegalPerson LegalPerson { get; set; }

        [Display(Name = "Pessoa Física")]
        public virtual NaturalPerson NaturalPerson { get; set; }

        [Display(Name = "Endereços")]
        public virtual ICollection<Address> Addresses { get; set; }

        [Display(Name = "Documentos")]
        public virtual ICollection<Document> Documents { get; set; }

        [Display(Name = "Telefones")]
        public virtual ICollection<Phone> Phones { get; set; }

        public Person()
            : base()
        {
            this.IsActive = true;
        }

        private void SaveImages()
        {
            var config = new IkeCodeConfig("General.xml", "default");
            var path = config.GetString("uploadPath");

            if (HttpContext.Current != null)
            {
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(path)))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(path));
                }

                var mappedPath = HttpContext.Current.Server.MapPath(path);

                if (this.NaturalPerson != null && this.NaturalPerson.ProfileImage != null && this.NaturalPerson.ProfileImage.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(this.NaturalPerson.ProfileImage.FileName);
                    string filePath = Path.Combine(mappedPath, fileName);
                    this.NaturalPerson.ProfileImageUrl = string.Format("{0}/{1}", path, fileName);
                    this.NaturalPerson.ProfileImage.SaveAs(filePath);
                }
                if (this.LegalPerson != null && this.LegalPerson.ProfileImage != null && this.LegalPerson.ProfileImage.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(this.LegalPerson.ProfileImage.FileName);
                    string filePath = Path.Combine(mappedPath, fileName);
                    this.LegalPerson.ProfileImageUrl = string.Format("{0}/{1}", path, fileName);
                    this.LegalPerson.ProfileImage.SaveAs(filePath);
                }
                if (this.Doctor != null && this.Doctor.ProfileImage != null && this.Doctor.ProfileImage.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(this.Doctor.ProfileImage.FileName);
                    string filePath = Path.Combine(mappedPath, fileName);
                    this.Doctor.ProfileImageUrl = string.Format("{0}/{1}", path, fileName);
                    this.Doctor.ProfileImage.SaveAs(filePath);
                }
            }
        }

        protected override void SetEntitiesState(IkeMedContext context)
        {
            if (this != null)
            {
                context.Entry(this).State = this.ID > 0 ? EntityState.Modified : EntityState.Added;
                if (this.Doctor != null)
                    context.Entry(this.Doctor).State = this.Doctor.ID > 0 ? EntityState.Modified : EntityState.Added;
                if (this.LegalPerson != null)
                    context.Entry(this.LegalPerson).State = this.LegalPerson.ID > 0 ? EntityState.Modified : EntityState.Added;
                if (this.NaturalPerson != null)
                    context.Entry(this.NaturalPerson).State = this.NaturalPerson.ID > 0 ? EntityState.Modified : EntityState.Added;
            }

            base.SetEntitiesState(context);
        }

        protected void SetEntitiesState(IkeMedContext context, Person person)
        {
            if (person != null)
            {
                var personEntry = context.Entry(person);
                if (personEntry.State == EntityState.Detached)
                    context.People.Attach(person);

                context.Entry(person).State = person.ID > 0 ? EntityState.Modified : EntityState.Added;
                if (person.Doctor != null)
                    context.Entry(person.Doctor).State = person.Doctor.ID > 0 ? EntityState.Modified : EntityState.Added;
                if (person.LegalPerson != null)
                    context.Entry(person.LegalPerson).State = person.LegalPerson.ID > 0 ? EntityState.Modified : EntityState.Added;
                if (person.NaturalPerson != null)
                    context.Entry(person.NaturalPerson).State = person.NaturalPerson.ID > 0 ? EntityState.Modified : EntityState.Added;
            }

            base.SetEntitiesState(context);
        }

        public override int SaveChanges(IkeMedContext context)
        {
            this.SetEntitiesState(context);
            this.SaveImages();

            return base.SaveChanges(context);
        }

        public int SaveChanges(IkeMedContext context, Person person)
        {
            this.SetEntitiesState(context, person);

            return context.SaveChanges();
        }
    }
}
