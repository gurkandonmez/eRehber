﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eRehber.ViewModels
{
    public class OkViewModel : NotifyViewModelBase<string>
    {
        public OkViewModel()
        {
            Title = "Islem Basarili";
        }
    }
}