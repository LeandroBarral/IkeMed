﻿using IkeMed.Metro.Web.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IkeMed.Metro.Web.Base
{
    public class BaseViewModel
    {
        public NotifyViewModel Notify { get; set; }
        
        public BaseViewModel()
        {
            this.Notify = new NotifyViewModel();
        }
    }
}