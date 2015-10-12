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
    public class FileController : Controller
    {
        //
        // GET: /Home/
        private readonly IService<FileEntity> fileService;
        private readonly IService<CommentEntity> commentService;
        private readonly IService<UserEntity> userService;
        public FileController(IService<FileEntity> fileService, IService<CommentEntity> commentService, IService<UserEntity> userService)
        {
            this.fileService = fileService;
            this.commentService = commentService;
            this.userService = userService;
        }
        public ActionResult Index(string filter, string sort, int? page, string search)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            ViewBag.Filter = filter;
            ViewBag.Sort = sort;
            ViewBag.Search = search;
            IEnumerable<FileEntity> files;
            files = fileService.GetAllEntities();
            files = files.OrderByDescending(e => e.CreationTime);

            if (!String.IsNullOrEmpty(search))
            {
                files = files.Where(e => e.Name.ToLower()
                                          .Contains(search.ToLower()) || e.Description.ToLower().Contains(search.ToLower()));
            }
            if (!string.IsNullOrEmpty(filter) && filter != "all")
            {
                files = files.Where(e => e.FileType.Contains(filter));
            }
            if (!string.IsNullOrEmpty(sort) && sort != "date")
            {
                files = (sort == "title") ? files.OrderBy(e => e.Name) : files.OrderByDescending(e => e.Rating);
            }

            if (Request.IsAjaxRequest())
                return PartialView(files.Select(file => file.ToMvcFile()).ToPagedList(pageNumber, pageSize));
            return View(files.Select(file => file.ToMvcFile()).ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            int entityId = (id ?? 1);
            ViewBag.Id = entityId;
            FileEntity file = fileService.GetEntity(entityId);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file.ToMvcFile());
        }
        public ActionResult Content(int? id)
        {
            ViewBag.Id = (id ?? 1);
            return View();
        }
        public ActionResult ContentPartial(int? id)
        {
            int entityId = (id ?? 1);
            FileEntity file = fileService.GetEntity(entityId);
            if (file == null)
            {
                return HttpNotFound();
            }
            return PartialView(file.ToMvcFile());
        }
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            int entityId = (id ?? 1);
            fileService.Delete(entityId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RatingResult(int? id, string submit)
        {
            int entityId = (id ?? 1);
            FileEntity file = fileService.GetEntity(entityId);
            if (file == null)
            {
                return HttpNotFound();
            }
            var rating = file.Rating;

            if (submit == "like") rating = rating + 1;
            else rating = rating - 0.5;

            file.Rating = rating;
            fileService.Edit(file);
            if (Request.IsAjaxRequest()) return PartialView("ContentPartial", file.ToMvcFile());
            return RedirectToAction("Content", new {id = entityId});

        }

        public ActionResult SetRating(int? id, string submit)
        {
            int entityId = (id ?? 1);
            try
            {
                if (CanUserVote(entityId, 1) == false)
                    return RedirectToAction("Index");
                else
                {
                    FileEntity file = fileService.GetEntity(entityId);
                    if (file == null)
                    {
                        return HttpNotFound();
                    }
                    var rating = file.Rating;

                    if (submit == "like") rating = rating + 1;
                    else rating = rating - 0.5;

                    file.Rating = rating;
                    fileService.Edit(file);
                    if (Request.IsAjaxRequest()) return PartialView("ContentPartial", file.ToMvcFile());
                    return RedirectToAction("Content", new { id = entityId });
                }
            } 
            catch (Exception ex) { return  RedirectToAction("Index");}
        }

        private bool CanUserVote(int id, double rating)
        {
            HttpCookie voteCookie = Request.Cookies["Votes"];
            //if (voteCookie != null)
            //{
            //    if (voteCookie[id.ToString()] != null)
            //    {
            //        return false;
            //    }
            //    else
            //    {
            //        voteCookie[id.ToString()] = rating.ToString();
            //        Response.Cookies.Add(voteCookie);
            //        return true;
            //    }
            //}
            //voteCookie = new HttpCookie("Votes"); 
            //voteCookie[id.ToString()] = rating.ToString(); 
            //Response.Cookies.Add(voteCookie); 
            //return true;

            if (voteCookie != null && voteCookie[id.ToString()] != null)
                return false;
            else if (voteCookie == null) voteCookie = new HttpCookie("Votes");
            voteCookie[id.ToString()] = rating.ToString(); 
            Response.Cookies.Add(voteCookie); 
            return true;
        }
      
        [HttpGet]
        public ActionResult CommentsData(int? id)
        {
            ViewBag.Id = (id ?? 1);
            //ViewBag.Id = id;
            //int entity = (id??1);
            //return PartialView(serviceC.GetAllEntities().Where(e => e.FileId == id)
            //                                           .OrderByDescending(e => e.CreationTime)
            //                                            .Select(comment => comment.ToMvcComment()));
            return View();

        }

        [HttpPost]
        public ActionResult CommentsData(CommentViewModel model, int? id)
        {
            int entityid = (id ?? 1);
            ViewBag.Id = entityid;
            model.FileId = entityid;
            var user = userService.GetAllEntities().First(u => u.Email == User.Identity.Name);
            model.UserName = user.Email;
            model.CreationTime = DateTime.Now;
            commentService.Create(model.ToBllComment());
            if (Request.IsAjaxRequest())
                return RedirectToAction("CommentsData");
            //return PartialView(serviceC.GetAllEntities().Where(e => e.FileId == id)
            //                                       .OrderByDescending(e => e.CreationTime)
            //                                       .Select(comment => comment.ToMvcComment()));
            return RedirectToAction("Content", new { id = id });

        }

        [HttpGet]
        public ActionResult CommentsView(int? id)
        {
            ViewBag.Id = (id ?? 1);

            if (Request.IsAjaxRequest())
                return PartialView(commentService.GetAllEntities().Where(e => e.FileId == id)
                                                           .OrderByDescending(e => e.CreationTime)
                                                           .Select(comment => comment.ToMvcComment()));
            return PartialView(commentService.GetAllEntities().Where(e => e.FileId == id)
                                                           .OrderByDescending(e => e.CreationTime)
                                                           .Select(comment => comment.ToMvcComment()));
        }
        public ActionResult DeleteComments(int? id, int? pageId)
        {
            int entityid = (pageId ?? 1);
            int idC = (id ?? 1);
            ViewBag.Id = entityid;
            commentService.Delete(idC);
            if (Request.IsAjaxRequest())
                return RedirectToAction("CommentsView", new { id = entityid });
            return RedirectToAction("Content", new { id = entityid });
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            int entityId = (id ?? 1);
            FileEntity file = fileService.GetEntity(entityId);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file.ToMvcFile());
        }

        [HttpPost]
        public ActionResult Edit(FileViewModel fileViewModel)
        {
            fileService.Edit(fileViewModel.ToBllFile());
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file, FileViewModel fileViewModel)
        {
            //char[] a = fileViewModel.FileName.ToCharArray();
            //a[0] = char.ToUpper(a[0]);
            //string n = new string(a);
            //string[] word = fileViewModel.FileName.Split(' ');
            //string newName = "";
            //for (int i = 0; i < word.Length; i++)
            //{
            //    if (word[i].Length > 1)
            //        word[i] = word[i].Substring(0, 1).ToUpper() + word[i].Substring(1, word[i].Length - 1).ToLower();
            //    else word[i] = word[i].ToUpper();
            //}
            //newName = string.Join(" ", word);
            //fileViewModel.FileName = n;
            fileViewModel.FileName = fileViewModel.FileName.ToCapitalLetter();
            fileViewModel.Description = fileViewModel.Description.ToCapitalLetter();
            string fileName = Guid.NewGuid().ToString();
            string extension = Path.GetExtension(file.FileName);
            fileName += extension;
            file.SaveAs(Server.MapPath("/Uploads/" + fileName));
            ViewBag.Message = "Файл сохранен";
            fileViewModel.Path = fileName;

            fileViewModel.FileType = file.ContentType;
            fileViewModel.CreationTime = DateTime.Now;
            fileViewModel.Rating = 3.4;
            fileViewModel.UserId = userService.GetAllEntities().First(u => u.Email == User.Identity.Name).Id;
            ViewBag.Path = fileName;
            fileService.Create(fileViewModel.ToBllFile());
            return RedirectToAction("Index");
        }

        public ActionResult DownloadFile(int? id)
        {
            int entityId = (id ?? 1);
            FileEntity file = fileService.GetEntity(entityId);
            if (file == null)
            {
                return HttpNotFound();
            }
            string filename = Server.MapPath("/Uploads/" + file.ToMvcFile().Path);
            string contentType = file.FileType;
            string downloadName = file.Name;
            return File(filename, contentType, downloadName);
        }

        public ActionResult DownloadBytes(int? id)
        {
            int entityId = (id ?? 1);
            FileEntity file = fileService.GetEntity(entityId);
            if (file == null)
            {
                return HttpNotFound();
            }
            string filename = Server.MapPath("/Uploads/" + file.ToMvcFile().Path);
            string contentType = file.FileType;
            byte[] data = System.IO.File.ReadAllBytes(filename);
            return File(data, contentType);
        }
    }
}
