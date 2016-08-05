using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using UserManagment.Core.DAL;

namespace UserManagment.Web.Areas.UserControlArea.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [StringLength(30, MinimumLength = 6)]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(128, MinimumLength = 6)]
        [Remote(action: "VerifyEmail", controller: "User")]
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string SkypeLogin { get; set; }
        public string Login { get; set; }


        public static UserViewModel GetViewModel(User user)
        {
            return new UserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Avatar = user.Avatar,
                SkypeLogin = user.SkypeLogin,
                Login = user.Login
            };
        }
        public static User GetUser(UserViewModel model)
        {
            return new User
            {
                Id = model.Id,
                UserName = model.UserName,
                Email = model.Email,
                Avatar = model.Avatar,
                SkypeLogin = model.SkypeLogin,
                Login = model.Login
            };
        }
    }
}