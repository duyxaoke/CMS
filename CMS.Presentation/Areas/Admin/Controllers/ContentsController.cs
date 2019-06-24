using System.Web.Mvc;
using DataTablesDotNet;
using DataTablesDotNet.Models;
using CMS.Domain.Entities;
using CMS.Presentation.Filters;
using CMS.Application.Application.ICMS;
using CMS.Application.ViewModels.CMS;
using System.Linq;
using CMS.Domain.Interfaces.Repositories;
using System;

namespace CMS.Presentation.Areas.Admin.Controllers
{
    [MvcAuthorizeAttribute]
    public class ContentsController : BaseController
    {
        private readonly IContentAppService _contentServices;
        private readonly ILanguageAppService _languageServices;
        private readonly ILanguageRepository _languageRepository;
        public ContentsController(IContentAppService contentServices, ILanguageAppService languageServices
            , ILanguageRepository languageRepository)
        {
            _contentServices = contentServices;
            _languageServices = languageServices;
            _languageRepository = languageRepository;
        }

        public ActionResult Index()
        {
            var model = new ContentModel();
            model.Languages = _languageServices.GetAllPaging().ToList();

            //get mapping proporties
            AddLocales(_languageRepository, model.Locales);

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ContentModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _contentServices.Add(model);

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
            var data = _contentServices.GetAllPaging();
            var dataTableParser = new DataTablesParser<Content>(model, data);
            var formattedList = dataTableParser.Process();
            return Json(formattedList, JsonRequestBehavior.AllowGet);
        }

    }
}