using GymManagement.Data;
using GymManagement.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GymManagement.Controllers
{
    public class SubscriptionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Subscription
        public async Task<ActionResult> Index()
        {
            ViewBag.UserLoggedIn = false;
            var user = await GetCurrentUser();
            if (user == null)
            {
                return View(new Subscription());
            }
            ViewBag.UserLoggedIn = true;

            var subscription = user.Subscription;

            if (subscription == null)
            {
                ViewBag.HasSubscription = false;
                return View(new Subscription());
            }

            ViewBag.HasSubscription = true;
            var subscriptionLastDay = subscription.StartDate.AddDays(subscription.Validity);
            ViewBag.SubscriptionLastDay = subscriptionLastDay;
            return View(user.Subscription);
        }

        private async Task<User> GetCurrentUser()
        {
            var signInManager = HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            var user = await signInManager.UserManager.FindByNameAsync(User.Identity.Name);
            return user;
        }

        [HttpGet]
        public ActionResult Create()
        {
            var subscriptionPackages = new SubscriptionPackagesViewModel()
            {
                Subscription = new Subscription(),
                Packages = db.Packages.ToList()
            };
            return View(subscriptionPackages);
        }

        [HttpPost]
        public async Task<ActionResult> Create(SubscriptionPackagesViewModel subscriptionPackages, FormCollection form)
        {
            var packageName = form["Package"];
            var selectedPackage = db.Packages.Single(package => package.Name.Equals(packageName));
            var subscription = new Subscription()
            {
                PackageId = selectedPackage.Id,
                StartDate = subscriptionPackages.Date,
                Validity = 30
            };

            db.Subscriptions.Add(subscription);
            var userEmail = User.Identity.Name;
            var currentUser = db.Users.Single(u => u.Email.Equals(userEmail));
            currentUser.SubscriptionId = subscription.Id;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}