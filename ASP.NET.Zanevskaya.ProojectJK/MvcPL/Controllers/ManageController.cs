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
using MvcPL.Infrastructure.Helpers;
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

        public ActionResult Index()
        {
            var profile = userService.GetAllEntities().First(u => u.Email == User.Identity.Name).Profile.ToMvcProfile();
            profile.Email = User.Identity.Name;
            return View(profile);
        }
         
        public ActionResult FileView()
        {
            var user = userService.GetAllEntities().First(u => u.Email == User.Identity.Name);
            var file = user.Files;
            return PartialView("_FileView",file.OrderByDescending(e => e.CreationTime).Select(f => f.ToMvcFile()));
        }
        
        [HttpGet]
        [ReferrerPageName]
        public ActionResult Edit(string id)
        {
            var user = userService.GetAllEntities().First(u => u.Email == User.Identity.Name);
            TempData["referrer"] = ControllerContext.RouteData.Values["referrer"];
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
            if (TempData["referrer"] != null)
            {
                return Redirect(TempData["referrer"].ToString()); 
            }
            return RedirectToAction("Index");
        }


        [Authorize(Roles = "Administrator")]
        public ActionResult UserView()
        {
            return PartialView("_UserView", userService.GetAllEntities().Select(user => user.ToMvcUser()));
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult UserDetails(int? id, bool layout = false)
        {
            int userId = id ?? 1;
            var user = userService.GetEntity(userId);
            if (layout) ViewBag.Layout = true;
            if (user != null)
            {
                ViewBag.Id = userId;
                var profile = user.Profile.ToMvcProfile();
                profile.Email = user.Email;
                return View("UserDetails", profile);
            }
            return View("Error");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult UserFileView(int? id)
        {
            int userId = id ?? 1;
            var user = userService.GetEntity(userId);
            if (user != null)
            {
                return
                    PartialView("_FileView", user.Files
                    .OrderByDescending(e=>e.CreationTime).Select(file=>file.ToMvcFile()));
            }
            return View("Error");
        }

        [HttpGet]
        [ReferrerPageName]
        public ActionResult DeleteUser(int? id)
        {
            int entityId = (id ?? 1);
            ViewBag.Id = entityId;
            UserEntity user = userService.GetEntity(entityId);
            TempData["referrer"] = ControllerContext.RouteData.Values["referrer"];
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
            if (TempData["referrer"] != null)
            {
                return Redirect(TempData["referrer"].ToString());
            }
            return RedirectToAction("Index");
        }

    }
}