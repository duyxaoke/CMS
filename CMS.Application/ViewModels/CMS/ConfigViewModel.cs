namespace CMS.Application.ViewModels.CMS
{
    public class ConfigViewModel : BaseViewModel
    {
        public int ConfigId { get; set; }
        public string Title { get; set; }
        public string Keyword { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
    }
}
