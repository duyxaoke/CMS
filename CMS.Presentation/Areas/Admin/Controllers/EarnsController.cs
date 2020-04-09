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
using CMS.Application.Application.ICMS;

namespace CMS.Presentation.Areas.Admin.Controllers
{
    [MvcAuthorizeAttribute]
    public class EarnsController : BaseController
    {
        private readonly IPartnerAppService _PartnerServices;

        public EarnsController(IPartnerAppService PartnerServices)
        {
            _PartnerServices = PartnerServices;
        }

        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Data(DataTablesRequest model)
        {
            var data = _PartnerServices.GetAllPaging();
            var dataTableParser = new DataTablesParser<Partner>(model, data);
            var formattedList = dataTableParser.Process();
            return Json(formattedList, JsonRequestBehavior.AllowGet);
        }

    }
}