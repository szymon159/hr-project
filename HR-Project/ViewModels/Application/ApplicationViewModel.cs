using HR_Project.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Project.ViewModels
{
    public class ApplicationViewModel
    {
        public int Id { get; set; }

        public string JobTitle { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public IFormFile CV { get; set; }
        public bool IsCvUploaded { get; set; }

        public List<IFormFile> OtherAttachments { get; set; }
        public bool IsAttachmentsUploaded { get; set; }

        public string Message { get; set; }

        public ApplicationStatus Status { get; set; }

        public string StatusCssClass { get { return "application-status-" + Status.ToString().ToLower(); } }
    }
}
