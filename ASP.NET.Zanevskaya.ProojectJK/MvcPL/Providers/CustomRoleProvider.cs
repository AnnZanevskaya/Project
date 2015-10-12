using Ninject;
using System;
using System.Linq;
using System.Web.Helpers;
using System.Web.Security;
using DependencyResolver;
using BLL.Interface.Services;
using BLL.Interface.Entities;
using System.Collections.Generic;


namespace MvcPL.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        private IKernel kernel;
        public override bool IsUserInRole(string email, string roleName)
        {
            kernel = new StandardKernel();
            kernel.ConfigurateResolverWeb();
            var userService = kernel.Get<IService<UserEntity>>();
            var user = userService.GetAllEntities().FirstOrDefault(u => u.Email == email);
            if (user == null) return false;
            var role = user.Roles.Select(r => r.Name).Contains(roleName);
            return role;
        }

        public override string[] GetRolesForUser(string email)
        {
            kernel = new StandardKernel();
            kernel.ConfigurateResolverWeb();
            var userService = kernel.Get<IService<UserEntity>>();
            var roles = new List<string>();
            var user = userService.GetAllEntities().FirstOrDefault(u => u.Email == email);
            if (user == null) return roles.ToArray();
            if (user.Roles == null) return roles.ToArray();
            roles.AddRange(user.Roles.Select(role => role.Name));
            return roles.ToArray();          
        }

        public override void CreateRole(string roleName)
        {
            kernel = new StandardKernel();
            kernel.ConfigurateResolverWeb();
            var roleService = kernel.Get<IService<RoleEntity>>();
            roleService.Create(new RoleEntity { Name = roleName });
        }

#region NotImplemented 
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
#endregion
    }
}