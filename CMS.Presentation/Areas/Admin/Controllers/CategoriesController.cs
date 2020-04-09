using CMS.Application.Application.ICMS;
using CMS.Application.ViewModels.CMS;
using CMS.Domain.Entities;
using CMS.Domain.Interfaces.Repositories;
using CMS.Presentation.Filters;
using DataTablesDotNet;
using DataTablesDotNet.Models;
using System;
using System.Collections.Generic;
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

        public CategoriesController(ICategoryAppService services, ILanguageAppService languageServices)
        {
            _services = services;
            _languageServices = languageServices;

        }

        //[ClaimsAction(ClaimsActions.Index)]
        public ActionResult Index()
        {
            var model = new CategoryModel();
            model.Languages = _languageServices.GetAllPaging().ToList();

            //get mapping proporties
            AddLocales(_languageServices, model.Locales);

            //var mappingModel = new List<CategoryMappingModel>();
            //var contentModel = _services.GetAll();
            //AddLocales(_languageServices, mappingModel, (locale, languageId) =>
            //{
            //    foreach (var item in contentModel.FirstOrDefault().Locales.Where(l => l.LanguageId == languageId))
            //    {
            //        locale.Title = item.Title;
            //        locale.Description = item.Description;
            //    }
            //});


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