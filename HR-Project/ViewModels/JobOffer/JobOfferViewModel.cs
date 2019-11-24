using HR_Project.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Project.ViewModels
{
    public class JobOfferViewModel
    {
        public int? Id { get; set; }

        public string JobTitle { get; set; }

        public string Description { get; set; }

        public JobOfferStatus Status {get; set; }

        public string StatusCssClass { get { return "joboffer-status-" + Status.ToString().ToLower(); } }
    }
}
