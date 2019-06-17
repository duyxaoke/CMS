using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Application.ViewModels.CMS
{
    public class CategoryViewModel : BaseViewModel
    {
        public int ModuleID { get; set; }
        public int ParentID { get; set; }
        public string ParentName { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public IEnumerable<SelectListViewModel> Parents { get; set; }
        [NotMapped]
        public IEnumerable<CategoryViewModel> Childrens { get; set; }
        public List<int> CategoryIDs { get; set; }
        public bool IsActive { get; set; }
    }
}
