using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using UserManagment.Core;
using UserManagment.Core.DAL;
using UserManagment.Core.Models;

namespace UserManagment.Infrastructure.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        //Verify email remote
        bool VerifyEmail(int? id, string email);
        IEnumerable<User> GetAll();
        OperationStatus AvaUpdate(int userid, HttpPostedFileBase file);
        OperationStatus DeleteUser(int userid);
        OperationStatus CreateUser(User user, HttpPostedFileBase file);
        OperationStatus UpdateField(UpdateModel upmodel);
    }

    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context) { }

        public bool VerifyEmail(int? id,string email)
        {
            if (id!=null)
            {
                return Context.Set<User>().FirstOrDefault(u => string.Equals(u.Email, email) && u.Id != id) == null;
            }
            return Context.Set<User>().FirstOrDefault(u => string.Equals(u.Email, email)) == null;
        }

        public IEnumerable<User> GetAll()
        {
            return Context.Set<User>().OrderBy(u => u.UserName).ToList();
        }

        public OperationStatus AvaUpdate(int userid, HttpPostedFileBase file)
        {
            User user = Find(userid);

            string fileName = user.Email.Substring(0, user.Email.IndexOf("@", StringComparison.CurrentCulture)) +
                              "_ava_" +
                              file.FileName.Substring(file.FileName.IndexOf(".", StringComparison.CurrentCulture));

            string path = Path.Combine(HostingEnvironment.MapPath("~/Avatars"), user.Avatar);

            //if user had avatar or avatar was same image type
            if (!(string.Equals(user.Avatar, "256.jpg") || string.Equals(user.Avatar, fileName)))
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            //To save file, use SaveAs method
            path = path.Replace(user.Avatar, fileName);

            file.SaveAs(path); //File will be saved in application root

            user.Avatar = fileName;
            return Update(user);
        }

        public OperationStatus UpdateField(UpdateModel upmodel)
        {
            User user = Find(upmodel.Id);

            typeof (User).GetProperty(upmodel.Field).SetValue(user, upmodel.NewValue);

            return Update(user);
        }

        public OperationStatus DeleteUser(int userid)
        {
            User user = Find(userid);
            //if user had avatar
            if (!(user.Avatar==null||string.Equals(user.Avatar, "256.jpg")))
            {
                string path = HostingEnvironment.MapPath("~/Avatars");
                path = Path.Combine(path, user.Avatar);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }

            return Delete(user);
        }

        public OperationStatus CreateUser(User user, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                string fileName =
                    user.Email.Substring(0, user.Email.IndexOf("@", StringComparison.CurrentCulture)) +
                    "_ava_" +
                    file.FileName.Substring(file.FileName.IndexOf(".", StringComparison.CurrentCulture));
                user.Avatar = fileName;

                string path = HostingEnvironment.MapPath("~/Avatars");
                path = Path.Combine(path, fileName);
                file.SaveAs(path);
            }
            else
            {
                user.Avatar = "256.jpg";
            }
            return Insert(user);
        }
    }
}
