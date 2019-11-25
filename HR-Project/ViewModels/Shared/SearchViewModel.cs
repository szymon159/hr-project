using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Project.ViewModels
{
    public class SearchViewModel
    {
        public string SearchString { get; set; }
        public string ComponentTitle { get; set; }

        public SearchViewModel(object text)
        {
            ComponentTitle = text as string;
        }
    }
}
