using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Whisper.Models
{
    public class RoleManager : RoleProvider
    {
        DBContext db = new DBContext();
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string userId)
        {
            if (!int.TryParse(userId, out var id))
            {
                // Log error or handle the case where userId is not a valid integer
                return new string[0]; // Return an empty array or handle accordingly
            }

            var userFromDb = db.Users.FirstOrDefault(u => u.UserId == id);
            if (userFromDb == null)
            {
                // Handle the case where no user is found, perhaps log or return a default role
                return new string[0]; // Return an empty array or handle accordingly
            }

            var userRole = userFromDb.Role;
            if (string.IsNullOrEmpty(userRole))
            {
                // Handle the case where the user has no role defined
                return new string[0]; // Return an empty array or a default role
            }

            return new[] { userRole }; // Return the user's role
        }


        //string role = db.Users.Where(u => u.User_ID.ToString() == userId).FirstOrDefault().Ruolo;
        //string[] roles = new string[] { role };
        //return roles;

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}