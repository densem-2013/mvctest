using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UserManagment.Core;
using UserManagment.Core.DAL;
using UserManagment.Core.Models;
using UserManagment.Infrastructure.Repositories;

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
            return View("AddUser", new UserViewModel {Avatar = "256.jpg"});
        }

        // GET: 
        [HttpGet, ActionName("GetAll")]
        [NoCache]
        public ActionResult GetAll()
        {
            return View("UsersList", _userRepository.GetAll());
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            User user = _userRepository.Find(id);

            return View("UserDetails", UserViewModel.GetViewModel(user));
        }

        [HttpPost]
        public ActionResult UpdateFeild(UpdateModel upmodel)
        {
            if (Request.IsAjaxRequest() && upmodel != null)
            {
                _userRepository.UpdateField(upmodel);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            _userRepository.DeleteUser(id);

            return RedirectToAction("GetAll");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserViewModel model, HttpPostedFileBase uploader)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "User");
            }

            User user = UserViewModel.GetUser(model);


            OperationStatus status = _userRepository.CreateUser(user, uploader);

            return View("UsersList", _userRepository.GetAll());
        }

        [HttpPost]
        public JsonResult UpdateAvatar(int id)
        {
            HttpPostedFileBase file = Request.Files != null ? Request.Files[0] : null;
            if (file != null && file.ContentLength > 0)
            {
                _userRepository.AvaUpdate(id, file);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult VerifyEmail(int? id,string email)
        {
            return Json(_userRepository.VerifyEmail(id,email), JsonRequestBehavior.AllowGet);
        }

    }
}