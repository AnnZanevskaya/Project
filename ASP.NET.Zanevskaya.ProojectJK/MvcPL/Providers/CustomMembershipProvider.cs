using Ninject;
using System;
using System.Linq;
using System.Web.Helpers;
using System.Web.Security;
using DependencyResolver;
using BLL.Interface.Services;
using BLL.Interface.Entities;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MvcPL.Providers
{
    public class CustomMembershipProvider : MembershipProvider
    {
        private IService<UserEntity> userService;
        private IService<RoleEntity> roleService;

        public MembershipUser CreateUser(string email, string password)
        {
            userService = (IService<UserEntity>)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IService<UserEntity>));
            roleService = (IService<RoleEntity>)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IService<RoleEntity>));
            MembershipUser membershipUser = GetUser(email, false);
            if (membershipUser != null)
                return null;
                var user = new UserEntity
                {
                    Email = email,
                    Password = Crypto.HashPassword(password),
                    Files = new List<FileEntity>(),
                    Roles = new List<RoleEntity>(),
                    Profile = new ProfileEntity{Age=0,FirstName = "Noname",LastName = "Noname",LastUpdate = DateTime.Now}
                };

                var role = roleService.GetAllEntities().FirstOrDefault(r => r.Name == "User");
                if (role != null)
                {
                    user.Roles.Add(role);
                }
                userService.Create(user);
                membershipUser = GetUser(email, false);
                return membershipUser;  
        }

        public override bool ValidateUser(string email, string password)
        {
           userService = (IService<UserEntity>)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IService<UserEntity>));
           var user = userService.GetAllEntities().FirstOrDefault(u => u.Email == email);
           return user != null && Crypto.VerifyHashedPassword(user.Password, password);
        }

        public override MembershipUser GetUser(string email, bool userIsOnline)
        {
            userService = (IService<UserEntity>)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IService<UserEntity>));
            var user = userService.GetAllEntities().FirstOrDefault(u => u.Email == email);
            if (user == null) return null;
            var memberUser = new MembershipUser("CustomMembershipProvider", user.Email,
                null, null, null, null,
                false, false, DateTime.Now, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
            return memberUser;
        }

#region NotImplemented
        public override MembershipUser CreateUser(string username, string password, string email,
            string passwordQuestion,
            string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion,
            string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }
        
        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override string ApplicationName { get; set; }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }
#endregion
    }
}