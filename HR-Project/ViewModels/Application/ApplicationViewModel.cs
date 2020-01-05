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

        public bool IsCvUploaded => !string.IsNullOrEmpty(UploadedCvPath);
        public IFormFile CV { get; set; }
        public string UploadedCvPath { get; set; }

        public bool IsAttachmentsUploaded => UploadedAttachmentPaths != null && UploadedAttachmentPaths.Count > 0;
        public List<IFormFile> OtherAttachments { get; set; }
        public List<string> UploadedAttachmentPaths { get; set; }

        public string Message { get; set; }

        public ApplicationStatus Status { get; set; }

        public string StatusCssClass { get { return "application-status-" + Status.ToString().ToLower(); } }
    }
}
