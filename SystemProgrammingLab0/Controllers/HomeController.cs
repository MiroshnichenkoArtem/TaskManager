using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TaskManager;

namespace SystemProgrammingLab0.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ProcessRepository repository = new ProcessRepository();
            IEnumerable<ProcessModel> processes = repository.GetAllProcesses();
            var test = processes.ToList();
            ViewBag.Proc = test;
            return View();
        }
    }
}