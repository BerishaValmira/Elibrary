using LibraryMngSys.Areas.Identity.Data;
using LibraryMngSys.Models.Book;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryMngSys.ViewModels
{
    public class AddBookCategory
    {
        public Book Book { get; set; }

        public IList<SelectListItem> Categories { get; set; }

        public string SelectedCategory { get; set; }
    }
}