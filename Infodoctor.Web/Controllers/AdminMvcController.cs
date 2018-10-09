using System;
using System.Web.Mvc;
using Infodoctor.BL.Interfaces;
using Infodoctor.BL.Interfaces.AddressInterfaces;

namespace Infodoctor.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminMvcController : Controller
    {

        private readonly ICountryService _countryService;
        private readonly ILanguageService _languageService;

        public AdminMvcController(ICountryService countryService, ILanguageService languageService)
        {
            _countryService = countryService ?? throw new ArgumentNullException(nameof(countryService));
            _languageService = languageService ?? throw new ArgumentNullException(nameof(languageService));
        }

        public ActionResult AdminMenu()
        {
            return View("AdminMenu");
        }

        public ActionResult Countries()
        {
            var country = _countryService.GetAllCountriesForAdmin();
            return View("Countries", country);
        }

        // GET: AdminMvc/Country/5
        public ActionResult Country(int id)
        {
            var country = _countryService.GetCountryByIdForAdmin(id);
            return View("CountryEdit", country);
        }

        // GET: AdminMvc/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminMvc/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminMvc/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminMvc/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminMvc/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminMvc/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
