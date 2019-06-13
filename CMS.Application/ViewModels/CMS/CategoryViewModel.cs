namespace CMS.Application.ViewModels.CMS
{
    public class CategoryViewModel : BaseViewModel
    {
        public int PartnerID { get; set; }
        public string PartnerName { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public decimal PercentCommission { get; set; }
        public bool IsActive { get; set; }
    }
}
