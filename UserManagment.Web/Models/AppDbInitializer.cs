using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Hosting;
using System.Xml.Linq;
using UserManagment.Core.DAL;

namespace UserManagment.Web.Models
{
    public class AppDbInitializer : DropCreateDatabaseIfModelChanges<AppContext>
    {
        static string _path = HostingEnvironment.MapPath("~/App_Data/Employeers.xml");
        protected override void Seed(AppContext context)
        {
            InitializeIdentityForEf(context);
            base.Seed(context);
        }

        public static void InitializeIdentityForEf(AppContext context)
        {
            GetUsersFromXml(context, _path);
        }
        public static void GetUsersFromXml(AppContext context, string userpath)
        {
            var xml = XDocument.Load(userpath);
            if (xml.Root != null)
            {
                var collection = xml.Root.Descendants("Employeer");
                try
                {
                    int i = 1;
                    User[] users = collection.AsEnumerable().Select(el =>
                    {
                        XElement xElement = el.Element("FirstName");
                        if (xElement != null)
                        {
                            XElement element = el.Element("LastName");
                            if (element != null)
                                return new User
                                {
                                    UserName =
                                        string.Format("{0} {1}", element.Value, xElement.Value),
                                    Email = string.Format("test{0}@test.com",i),
                                    SkypeLogin = String.Format("skype_test{0}",i),
                                    Login = string.Format("user{0}",i++),
                                    Avatar = "256.jpg"
                                };
                        }
                        return null;
                    }).ToArray();
                    context.Users.AddRange(users);
                    context.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        System.Diagnostics.Debug.Print(
                            "Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            System.Diagnostics.Debug.Write(ve.ErrorMessage, ve.PropertyName);
                        }
                    }
                    throw;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Write(ex.InnerException, ex.Message);
                }
            }
        }
    }
}