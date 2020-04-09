using CMS.Application.Application.ICMS;
using CMS.Application.ViewModels.CMS;
using CMS.Presentation.Areas.Admin.Controllers;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CMS.Presentation.Controllers
{
    public class CategoriesController : BaseController
    {
        Logger logger = LogManager.GetLogger("databaseLogger");
        private readonly ICategoryAppService _services;
        private readonly ILanguageAppService _languageServices;

        public CategoriesController(ICategoryAppService services, ILanguageAppService languageServices)
        {
            _services = services;
            _languageServices = languageServices;

        }

        public ActionResult Index()
        {
            return View();
        }

    }
}