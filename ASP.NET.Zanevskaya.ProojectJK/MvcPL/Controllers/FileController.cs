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
    public class FileController : Controller
    {
        //
        // GET: /Home/
        private readonly IService<FileEntity> service;
        private readonly IService<CommentEntity> serviceC;
        public FileController(IService<FileEntity> service, IService<CommentEntity> serviceC)
        {
            this.service = service;
            this.serviceC = serviceC;
        }

        public ActionResult Index(string filter, string sort, int? page, string search)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            ViewBag.Filter = filter;
            ViewBag.Sort = sort;
            ViewBag.Search = search;
            IEnumerable<FileEntity> files = service.GetAllEntities()
                                .OrderByDescending(e => e.CreationTime);

            if (!String.IsNullOrEmpty(search))
            {
                files = files.Where(e => e.Name.ToLower()
                                          .Contains(search.ToLower()) || e.Description.ToLower().Contains(search.ToLower()));
            } 
            filter = String.IsNullOrEmpty(filter) ? "0" : filter;
            if (!string.IsNullOrEmpty(filter) && filter != "0")
            {
                files = files.Where(e => e.FileType == filter);          
            }
            if (sort == "1")
            {
                files = files.OrderBy(e => e.Name);              
            }
            if (sort == "2")
            {
                files = files.OrderByDescending(e => e.Rating);             
            }

            return View(files.Select(file => file.ToMvcFile()).ToPagedList(pageNumber, pageSize));           
        }
        
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FileViewModel fileViewModel)
        {
            service.Create(fileViewModel.ToBllFile());
            return RedirectToAction("Index");
        }

        ////GET-запрос к методу Delete несет потенциальную уязвимость!
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            int entityId = (id ?? 1);
            FileEntity file = service.GetEntity(entityId);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file.ToMvcFile());
        }

        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            int entityId = (id ?? 1);
            service.Delete(entityId);
            return RedirectToAction("Index");
        }

        public ActionResult Content(int? id)
        {
            ViewBag.Id = (id ?? 1);
            return View();
        }
        public ActionResult ContentPartial(int? id)
        {
            int entityId = (id ?? 1);
            FileEntity file = service.GetEntity(entityId);
            if (file == null)
            {
                return HttpNotFound();
            }
            return PartialView(file.ToMvcFile());
        }
        [HttpGet]
        public ActionResult ComentsData(int? id)
        {
            ViewBag.Id = id;
            return PartialView(serviceC.GetAllEntities().Where(e => e.FileId == id)
                                                        .OrderByDescending(e => e.CreationTime)
                                                        .Select(comment => comment.ToMvcComment()));              
        }

        [HttpPost]
        public ActionResult ComentsData(CommentViewModel model, int? id)
        {
            
            int entityid = (id ?? 1);
            ViewBag.Id = entityid;
            model.FileId = entityid;
            model.CreationTime = DateTime.Now;
            serviceC.Create(model.ToBllComment());
            return PartialView(serviceC.GetAllEntities().Where(e => e.FileId == id)
                                                        .OrderByDescending(e => e.CreationTime)
                                                        .Select(comment => comment.ToMvcComment()));
        }

        public ActionResult DeleteComments(int? id, int? pageId)
        {
            int entityid = (pageId ?? 1);
            int idC = (id ?? 1);
            ViewBag.Id = entityid;
            serviceC.Delete(idC);
            return View("Content");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            int entityId = (id ?? 1);
            FileEntity file = service.GetEntity(entityId);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file.ToMvcFile());
        }

        [HttpPost]
        public ActionResult Edit(FileViewModel fileViewModel)
        {
            service.Edit(fileViewModel.ToBllFile());
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
            string fileName = Guid.NewGuid().ToString();
            string extension = Path.GetExtension(file.FileName);
            fileName += extension;

            List<string> extensions = new List<string>() { ".txt", ".docx", ".pdf", ".mp3", "mp4" };

            if (extensions.Contains(extension))
            {
                file.SaveAs(Server.MapPath("/Uploads/" + fileName));
                ViewBag.Message = "Файл сохранен";
                fileViewModel.Path = fileName;

                extensions = new List<string>() { ".txt", ".docx", ".pdf" };
                if (extensions.Contains(extension)) fileViewModel.FileType = "1";

                if (extensions.Contains(".mp3")) fileViewModel.FileType = "2";

                if (extensions.Contains(".mp4")) fileViewModel.FileType = "3";

                fileViewModel.CreationTime = DateTime.Now;
                fileViewModel.Rating = 3.4;
                ViewBag.Path = fileName;
                service.Create(fileViewModel.ToBllFile());

            }
            else
            {
                ViewBag.Message = "Ошибка. Допустимые расширения файлов - '.txt', '.docx','.pdf','mp3', 'avi', 'mp4'";
            }

            // return RedirectToAction("Index");
            return RedirectToAction("Index");
        }

        public ActionResult DownloadFile(int? id)
        {
            int entityId = (id ?? 1);
            FileEntity file = service.GetEntity(entityId);
            if (file == null)
            {
                return HttpNotFound();
            }
            string filename = Server.MapPath("/Uploads/" + file.ToMvcFile().Path);
            string contentType = "application/pdf"; // MIME Type image/png  image/jpg application/pdf
            string downloadName = "PDF File";
            // Если имя файла для скачивания не указано и если 
            // браузер поддерживает тип файла, файл откроется в самом браузере.
            //downloadName = null; 
            return File(filename, contentType, downloadName);
        }

        public ActionResult DownloadBytes(int? id)
        {
            int entityId = (id ?? 1);
            FileEntity file = service.GetEntity(entityId);
            if (file == null)
            {
                return HttpNotFound();
            }
            string filename = Server.MapPath("/Uploads/" + file.ToMvcFile().Path);
            string contentType = "application/pdf";
            byte[] data = System.IO.File.ReadAllBytes(filename);
            return File(data, contentType);
        }
    }
}
