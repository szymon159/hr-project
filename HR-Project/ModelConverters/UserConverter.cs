using HR_Project.ViewModels;
using HR_Project_Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Project.ModelConverters
{
    public static class UserConverter
    {
        public static User ToDatabaseModel(this UserViewModel viewModel)
        {
            return new User()
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Role = (UserRole)viewModel.Role,
                ExternalId = viewModel.ExternalId,
                Email = viewModel.Email
            };
        }

        public static UserViewModel ToViewModel(this User databaseModel)
        {
            return new UserViewModel
            {
                FirstName = databaseModel.FirstName,
                LastName = databaseModel.LastName,
                Role = (Enums.UserRole)databaseModel.Role,
                ExternalId = databaseModel.ExternalId,
                Email = databaseModel.Email
            };
        }
    }
}
