using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Project.ViewModels
{
    public class JobOfferDetailsViewModel
    {
        public JobOfferViewModel JobOfferModel { get; set; }

        public bool IsEditing { get; set; }
    }
}
