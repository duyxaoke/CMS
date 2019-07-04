using CMS.Application.Application.ICMS;
using CMS.Application.ViewModels.CMS;
using CMS.Domain.Interfaces.Repositories;
using CMS.Presentation.Filters;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiThrottle;

namespace CMS.Presentation.Controllers.Api
{
    [ApiAuthorizeAttribute]
    [RoutePrefix("api/Categories")]
    public class CategoriesController : ApiControllerBase
    {
        private readonly ICategoryAppService _service;
        private readonly ILanguageAppService _languageServices;
        private readonly ILanguageRepository _languageRepository;

        public CategoriesController(ICategoryAppService service, ILanguageAppService languageServices
            , ILanguageRepository languageRepository)
        {
            _service = service;
            _languageServices = languageServices;
            _languageRepository = languageRepository;
        }

        [HttpGet]
        [ResponseType(typeof(CategoryModel))]
        [Route("Init")]
        public IHttpActionResult Init()
        {
            try
            {
                var result = new CategoryModel();
                result.Languages = _languageServices.GetAllPaging().ToList();

                //get mapping proporties
                AddLocales(_languageRepository, result.Locales);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [HttpGet]
        [ResponseType(typeof(IEnumerable<CategoryModel>))]
        public async Task<IHttpActionResult> GetAllAsync()
        {
            try
            {
                var result = await _service.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        [ResponseType(typeof(CategoryModel))]
        public async Task<IHttpActionResult> GetByIdAsync(int id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);
                if (result == null)
                {
                    return NotFound();
                }
                var mappingModel = new List<CategoryMappingModel>();
                AddLocales(_languageRepository, mappingModel, (locale, languageId) =>
                {
                    foreach (var item in result.Locales.Where(l => l.LanguageId == languageId))
                    {
                        locale.Title = item.Title;
                        locale.Description = item.Description;
                    }
                });
                result.Languages = _languageServices.GetAllPaging().ToList();


                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        [HttpPost]
        [ResponseType(typeof(void))]
        [EnableThrottling(PerSecond = 1)]
        public IHttpActionResult Post([FromBody]CategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = new List<string>();
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                return Content(HttpStatusCode.NotAcceptable, errors);
            }
            try
            {
                _service.Add(model);
                return Content(HttpStatusCode.Created, model.CategoryId);
            }
            catch (DbUpdateException ex)
            {
                if (Exists(model.CategoryId))
                {
                    return Conflict();
                }
                else
                {
                    return InternalServerError();
                }
            }
        }

        [HttpPut]
        [ResponseType(typeof(void))]
        [EnableThrottling(PerSecond = 1)]
        public IHttpActionResult Put([FromBody]CategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = new List<string>();
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                return Content(HttpStatusCode.NotAcceptable, errors);
            }
            try
            {
                _service.Update(model);
                return Ok();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!Exists(model.CategoryId))
                {
                    return NotFound();
                }
                else
                {
                    return InternalServerError();
                }
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        [EnableThrottling(PerSecond = 1)]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _service.Remove(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        #region Helpers
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _service.Dispose();
            }
            base.Dispose(disposing);
        }
        private bool Exists(int id)
        {
            return _service.GetByIdAsync(id) != null;
        }
        #endregion
    }
}
