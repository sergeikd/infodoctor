using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infodoctor.BL.Interfaces;

namespace Infodoctor.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly ITestService _testService;

        public AdminController (ITestService testService)
        {
            if (testService == null) throw new ArgumentNullException(nameof(testService));
            _testService = testService;
        }
        // GET: Admin
        public ActionResult CreateClinic()
        {
            var clinic = _testService.PrepareClinic();
            return View(clinic);
        }
    }
}