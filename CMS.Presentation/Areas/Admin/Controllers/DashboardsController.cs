using CMS.Application.Application.ICMS;
using CMS.Domain.Entities;
using CMS.Presentation.Filters;
using DataTablesDotNet;
using DataTablesDotNet.Models;
using System.Web.Mvc;

namespace CMS.Presentation.Areas.Admin.Controllers
{
    [MvcAuthorize]
    public class DashboardsController : BaseController
    {

        public DashboardsController()
        {
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}