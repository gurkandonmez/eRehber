using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eRehber.ViewModels
{
    public class ErrorViewModel : NotifyViewModelBase<string>
    {
        public ErrorViewModel()
        {
            Title = "Bir Hata Oluştu";
        }
    }
}