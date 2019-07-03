using System.Collections.Generic;

namespace CMS.Application.ViewModels
{
    public interface ILocalizedModel
    {
    }

    public interface ILocalizedModel<T> : ILocalizedModel where T : ILocalizedModelLocal
    {
        IList<T> Locales { get; set; }
    }

    public interface ILocalizedModelLocal
    {
        int LanguageId { get; set; }
    }

}