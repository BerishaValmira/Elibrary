using LibraryMngSys.Data;
using LibraryMngSys.Models;
using LibraryMngSys.Wrappers;

namespace LibraryMngSys.Models.Book
{
    public class BookUtility: Utility<Book>
    {
        public Dictionary<string, Func<Book, string>> BookUtils;

        public Dictionary<string, string> header;

        Func<Book, string> Title = x => x.Title;
        Func<Book, string> Author = x => x.Author;
        Func<Book, string> Category = x => x.Category;
        Func<Book, string> Id = x => x.Id.ToString();
        Func<Book, string> CreatedAt = x => x.createdAt.ToString();
        Func<Book, string> UpdatedAt = x => x.updatedAt.ToString();
       

        public BookUtility()
        {
            BookUtils = new Dictionary<string, Func<Book, string>>();
            BookUtils.Add("Title", Title);
            BookUtils.Add("Id", Id);
            BookUtils.Add("Author", Author);
            BookUtils.Add("Category", Category);
            BookUtils.Add("createdAt", CreatedAt);
            BookUtils.Add("updatedAt", UpdatedAt);
            header = new Dictionary<string, string>() {
                {"Title","Title"},
                {"Author","Author"},
                {"Category","Category"},
            };
        }

        

        public Dictionary<string, string> Attrs(Book book)
        {
            return new Dictionary<string, string>()
            {
                { "Title", book.Title },
                {"Author",book.Author },
                {"Category",book.Category },

            };
        }
    }
}

