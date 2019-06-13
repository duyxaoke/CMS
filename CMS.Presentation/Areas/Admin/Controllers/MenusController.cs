using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DataTablesDotNet;
using DataTablesDotNet.Models;
using CMS.Application;
using CMS.Domain.Entities;
using CMS.Presentation.Filters;
using CMS.Presentation.Models;
using CMS.Application.Application;

namespace CMS.Presentation.Areas.Admin.Controllers
{
    [MvcAuthorizeAttribute]
    [ClaimsGroup(ClaimResources.Menus)]
    public class MenusController : BaseController
    {
        private readonly IMenuAppService _menuServices;

        public MenusController(IMenuAppService menuServices)
        {
            _menuServices = menuServices;
        }

        [ClaimsAction(ClaimsActions.Index)]
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Data(DataTablesRequest model)
        {
            var data = _menuServices.GetAllPaging();
            var dataTableParser = new DataTablesParser<Menu>(model, data);
            var formattedList = dataTableParser.Process();
            return Json(formattedList, JsonRequestBehavior.AllowGet);
        }

    }
}