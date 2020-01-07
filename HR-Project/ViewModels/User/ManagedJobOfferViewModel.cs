using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Project.ViewModels
{
    public class ManagedJobOfferViewModel
    {
        public string JobOfferTitle { get; set; }

        public int JobOfferId { get; set; }

        public bool IsManaged { get; set; }
    }
}
