using HR_Project.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Project.ViewModels
{
    public class ApplicationViewModel
    {
        public int? Id { get; set; }

        public string JobTitle { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string CV { get; set; }

        public string[] OtherAttachments { get; set; }

        public string Message { get; set; }

        public ApplicationStatus Status { get; set; }

        public string StatusCssClass { get { return "application-status-" + Status.ToString().ToLower(); } }
    }
}
