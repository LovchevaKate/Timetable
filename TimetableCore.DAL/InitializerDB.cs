using System;
using System.Collections.Generic;
using System.Linq;
using TimetableCore.DAL.Entities;

namespace TimetableCore.DAL
{
    public static class InitializerDB
    {
        public static void Initialize(ContextDB context)
        {
            IList<User> defaultUsers = new List<User>();
            IList<Role> defaultRoles = new List<Role>();

            if (!context.Roles.Any())
            {
                defaultRoles.Add(new Role() { Id = 1, Type = "admin" });
                defaultRoles.Add(new Role() { Id = 2, Type = "student" });
                defaultRoles.Add(new Role() { Id = 3, Type = "professor" });

                context.Roles.AddRange(defaultRoles);
            }

            if (!context.Users.Any())
            {
                defaultUsers.Add(new User() { Login = "admin", Password = "123", IdRole = 1 });
                
                context.Users.AddRange(defaultUsers);
            }
                

            context.SaveChanges();
        }
    }
}