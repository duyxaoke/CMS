using CMS.Application.Application.ICMS;
using CMS.Domain.Entities;
using CMS.Presentation.Filters;
using DataTablesDotNet;
using DataTablesDotNet.Models;
using System.Web.Mvc;

namespace CMS.Presentation.Areas.Admin.Controllers
{
    [MvcAuthorize]
    //[ClaimsGroup(ClaimResources.Configs)]
    public class CategoriesController : BaseController
    {
        private readonly ICategoryAppService _services;

        public CategoriesController(ICategoryAppService services)
        {
            _services = services;
        }

        //[ClaimsAction(ClaimsActions.Index)]
        public ActionResult Index()
        {
            return View();
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