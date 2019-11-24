using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Project.ViewModels
{
    public class ApplicationDetailsViewModel
    {
        public ApplicationViewModel ApplicationModel { get; set; }

        public bool IsEditing { get; set; }
    }
}
