using LibraryMngSys.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryMngSys.ViewModels
{
    public class EditUserViewModel
    {
        public LibraryMngSysUser User { get; set; }

        public IList<SelectListItem> Roles { get; set; }
    }
}
