using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System;
using PagedList;
namespace MvcPL.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private readonly IService<UserEntity> userService;
        private readonly IService<ProfileEntity> profileService;
        public ManageController(IService<ProfileEntity> profileService, IService<UserEntity> userService)
        {
            this.profileService = profileService;
            this.userService = userService;
        }

        public ActionResult Index(string pageName, string actionName)
        {
            
            if (String.IsNullOrEmpty(pageName))
                return
                    View(userService.GetAllEntities().First(u => u.Email == User.Identity.Name).Profile.ToMvcProfile());
            else if (!String.IsNullOrEmpty(actionName)) ViewBag.Title = actionName;
                return View(pageName,
                    userService.GetAllEntities().First(u => u.Email == User.Identity.Name).Profile.ToMvcProfile());
        }
         
        public ActionResult FileView()
        {
            var user = userService.GetAllEntities().First(u => u.Email == User.Identity.Name);
            var file = user.Files;
            return PartialView(file.OrderByDescending(e => e.CreationTime).Select(f => f.ToMvcFile()));
        }
        public ActionResult UserView()
        {
            return PartialView(userService.GetAllEntities().Select(user => user.ToMvcUser()));
        }
        [HttpGet]
        public ActionResult Edit(string id)
        {
            var user = userService.GetAllEntities().First(u => u.Email == User.Identity.Name);
            ProfileEntity profile = user.Profile;
            return View(profile.ToMvcProfile());
        }
        [HttpPost]
        public ActionResult Edit(ProfileViewModel profileViewModel)
        {
            var user = userService.GetAllEntities().First(u => u.Email == User.Identity.Name);
            profileViewModel.Id = user.Id;
            profileViewModel.LastUpdate = DateTime.Now;
            profileService.Edit(profileViewModel.ToBllProfile());
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult DeleteUser(int? id)
        {
            int entityId = (id ?? 1);
            ViewBag.Id = entityId;
            UserEntity user = userService.GetEntity(entityId);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user.ToMvcUser());
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            int entityId = (id ?? 1);
            userService.Delete(entityId);
            return RedirectToAction("Index");
        }

    }
}