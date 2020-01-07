using HR_Project.Enums;
using HR_Project.ModelConverters;
using HR_Project.ViewModels;
using HR_Project_Database.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Project.ExtensionMethods
{
    public static class DataContextExtensions
    {
        public static bool AddUserIfNotExists(this DataContext context, UserViewModel userModel, out UserRole dbUserRole)
        {
            var dbUser = context.User.FirstOrDefault(entity => entity.ExternalId == userModel.ExternalId);
            if (dbUser == null)
            {
                dbUser = userModel.ToDatabaseModel();
                context.User.Add(dbUser);
                context.SaveChanges();
                dbUserRole = userModel.Role;
                userModel.Id = dbUser.IdUser;
                return true;
            }
            else
            {
                dbUserRole = (UserRole)dbUser.Role;
                return false;
            }
        }
    }
}
