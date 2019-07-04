using CMS.Application.Application.ICMS;
using CMS.Application.ViewModels.CMS;
using CMS.Domain.Entities;
using CMS.Domain.Interfaces.Repositories;
using CMS.Presentation.Filters;
using DataTablesDotNet;
using DataTablesDotNet.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace CMS.Presentation.Areas.Admin.Controllers
{
    [MvcAuthorize]
    //[ClaimsGroup(ClaimResources.Configs)]
    public class CategoriesController : BaseController
    {
        private readonly ICategoryAppService _services;
        private readonly ILanguageAppService _languageServices;
        private readonly ILanguageRepository _languageRepository;

        public CategoriesController(ICategoryAppService services, ILanguageAppService languageServices
            , ILanguageRepository languageRepository)
        {
            _services = services;
            _languageServices = languageServices;
            _languageRepository = languageRepository;

        }

        //[ClaimsAction(ClaimsActions.Index)]
        public ActionResult Index()
        {
            var model = new CategoryModel();
            model.Languages = _languageServices.GetAllPaging().ToList();

            //get mapping proporties
            AddLocales(_languageRepository, model.Locales);

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _services.Add(model);
                    TempData["Message"] = "The new content has been added successfully.";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.ToString();
                }
            }
            return View(model);
        }

        public JsonResult Data(DataTablesRequest model)
        {
            var data = _services.GetAllPaging();
            var dataTableParser = new DataTablesParser<Category>(model, data);
            var formattedList = dataTableParser.Process();
            return Json(formattedList, JsonRequestBehavior.AllowGet);
        }
    }
}