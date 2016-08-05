using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime.Tree;
using UserManagment.Core;
using UserManagment.Core.DAL;
using UserManagment.Infrastructure.Repositories;
using UserManagment.Web.Areas.UserControlArea.Models;

namespace UserManagment.Web.Areas.UserControlArea.Controllers
{
    [RouteArea("UserControl")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository repo)
        {
            _userRepository = repo;
        }

        // GET: 
        public ActionResult Index()
        {
            return View("AddUser");
        }

        // GET: 
        [HttpGet,ActionName("GetAll")]
        public ActionResult GetAll()
        {
            return View("UsersList", _userRepository.GetAll());
        }

        [HttpGet/*,ActionName("Update")*/]
        public ActionResult Update(int id)
        {
             User user = _userRepository.Find(id);

            return View("UserDetails", UserViewModel.GetViewModel(user));
        }


        //[HttpPost, AcceptVerbs(HttpVerbs.Delete)]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            _userRepository.Delete(id);

            return RedirectToAction("GetAll");
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(UserViewModel model, HttpPostedFileBase upload)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "User");
            }
            if (upload != null && upload.ContentLength > 0)
                try
                {
                    model.Avatar = upload.FileName;

                    string path = Path.Combine(Server.MapPath("~/Avatars"),
                                               Path.GetFileName(upload.FileName));
                    upload.SaveAs(path);
                    ViewBag.Message = "File uploaded successfully";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }  
            User user = UserViewModel.GetUser(model);

            OperationStatus status = _userRepository.Insert(user);

            return View("UsersList", _userRepository.GetAll());
        }

        [HttpPost]
        public JsonResult UpdateAvatar(int id)
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i]; //Uploaded file
                //Use the following properties to get file's name, size and MIMEType
                int fileSize = file.ContentLength;
                string fileName = file.FileName;
                string mimeType = file.ContentType;
                System.IO.Stream fileContent = file.InputStream;
                //To save file, use SaveAs method
                file.SaveAs(Path.Combine(Server.MapPath("~/Avatars"), fileName)); //File will be saved in application root
                User user=_userRepository.Find(id);
                user.Avatar = fileName;
                _userRepository.Update(user);
            }
            return Json(true,JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs("Get", "Post")]
        public JsonResult VerifyEmail(string email)
        {
            if (!_userRepository.VerifyEmail(email))
            {
                return Json(string.Format("Email {0} is already in use.", email), JsonRequestBehavior.AllowGet);
            }

            return Json( true, JsonRequestBehavior.AllowGet);
        }

    }
}