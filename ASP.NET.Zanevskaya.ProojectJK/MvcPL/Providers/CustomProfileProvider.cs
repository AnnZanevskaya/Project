using Ninject;
using System;
using System.Linq;
using System.Web.Helpers;
using System.Web.Security;
using DependencyResolver;
using BLL.Interface.Services;
using BLL.Interface.Entities;
using System.Collections.Generic;
using System.Web.Profile;
using System.Configuration;

namespace MvcPL.Providers
{
    public class CustomProfileProvider : ProfileProvider
    {
        private readonly IService<UserEntity> userService;
        private readonly IService<ProfileEntity> profileService;

        public CustomProfileProvider(IService<UserEntity> userService, IService<ProfileEntity> profileService)
        {
            this.userService = userService;
            this.profileService = profileService;
        }
        public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection collection)
        {
            // коллекция, которая возвращает значения свойств профиля
            var result = new SettingsPropertyValueCollection();

            if (collection == null || collection.Count < 1 || context == null)
            {
                return result;
            }

            // получаем из контекста имя пользователя - логин в системе
            var username = (string)context["UserName"];
            if (String.IsNullOrEmpty(username)) return result;
            // получаем id пользователя из таблицы Users по логину
            var user = userService.GetAllEntities().FirstOrDefault(u => u.Email.Equals(username));
            var profile = user.Profile;   // по этому id извлекаем профиль из таблицы профилей               
             if (profile != null)
                {
                    foreach (SettingsProperty prop in collection)
                    {
                        var spv = new SettingsPropertyValue(prop)
                        {
                            PropertyValue = profile.GetType().GetProperty(prop.Name).GetValue(profile, null)
                        };
                        result.Add(spv);
                    }
                }
                else
                {
                    foreach (SettingsProperty prop in collection)
                    {
                        var svp = new SettingsPropertyValue(prop) { PropertyValue = null };
                        result.Add(svp);
                    }
                }
            return result;
        }

        public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection collection)
        {
            // получаем логин пользователя
            var username = (string)context["UserName"];

            if (string.IsNullOrEmpty(username) || collection.Count < 1)
                return;
            var user = userService.GetAllEntities().FirstOrDefault(u => u.Email.Equals(username));
            if (user == null) return;
            var profile = user.Profile;
            // получаем id пользователя из таблицы Users по логину
            if (profile != null)
            {
                    foreach (SettingsPropertyValue val in collection)
                    {
                        profile.GetType().GetProperty(val.Property.Name).SetValue(profile, val.PropertyValue);
                    }
                    profile.LastUpdate = DateTime.Now;
                }
                else
                {
                    // если нет, то создаем новый профиль и добавляем его
                    profile = new ProfileEntity();
                    foreach (SettingsPropertyValue val in collection)
                    {
                        profile.GetType().GetProperty(val.Property.Name).SetValue(profile, val.PropertyValue);
                    }
                    profile.LastUpdate = DateTime.Now;
                    profile.Id = user.Id;
                    profileService.Create(profile);   
                }
            }

        #region NotImplemented
        public override string ApplicationName { get; set; }

        public override int DeleteProfiles(ProfileInfoCollection profiles)
        {
            throw new NotImplementedException();
        }

        public override int DeleteProfiles(string[] usernames)
        {
            throw new NotImplementedException();
        }

        public override int DeleteInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
        {
            throw new NotImplementedException();
        }

        public override ProfileInfoCollection GetAllProfiles(ProfileAuthenticationOption authenticationOption, int pageIndex, int pageSize,
            out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override ProfileInfoCollection GetAllInactiveProfiles(ProfileAuthenticationOption authenticationOption,
            DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override ProfileInfoCollection FindProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch,
            int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override ProfileInfoCollection FindInactiveProfilesByUserName(ProfileAuthenticationOption authenticationOption,
            string usernameToMatch, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}