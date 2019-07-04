using CMS.Application.ViewModels;
using CMS.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace CMS.Presentation.Controllers.Api
{
    //[EnableCors(origins: "https://CMS.vn,https://localhost:44345", headers: "*", methods: "*")]
    //[AutoInvalidateCacheOutput]
    //[CacheOutput(ServerTimeSpan = 84000, ExcludeQueryStringFromCacheKey = true)]
    public class ApiControllerBase : ApiController
    {
        protected virtual void AddLocales<TLocalizedModelLocal>(ILanguageRepository languageService, IList<TLocalizedModelLocal> locales) where TLocalizedModelLocal : ILocalizedModelLocal
        {
            AddLocales(languageService, locales, null);
        }

        protected virtual void AddLocales<TLocalizedModelLocal>(ILanguageRepository languageService, IList<TLocalizedModelLocal> locales, Action<TLocalizedModelLocal, int> configure) where TLocalizedModelLocal : ILocalizedModelLocal
        {
            foreach (var language in languageService.GetAll())
            {
                var locale = Activator.CreateInstance<TLocalizedModelLocal>();
                locale.LanguageId = language.LanguageId;
                if (configure != null)
                {
                    configure.Invoke(locale, locale.LanguageId);
                }
                locales.Add(locale);
            }
        }

    }
}
