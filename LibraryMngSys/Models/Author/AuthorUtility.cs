using LibraryMngSys.Wrappers;

namespace LibraryMngSys.Models.Author
{
    public class AuthorUtility: Utility<Author>
    {
        public Dictionary<string, Func<Author, string>> AuthorUtils;

        public Dictionary<string, string> header;

        Func<Author, string> Id = x => x.Id.ToString();
        Func<Author, string> FirstName = x => x.Id.ToString();
        Func<Author, string> LastName = x => x.Id.ToString();
        Func<Author, string> Email = x => x.Id.ToString();
        Func<Author, string> Number = x => x.Id.ToString();

        public AuthorUtility()
        {
            AuthorUtils = new Dictionary<string, Func<Author, string>>();
            AuthorUtils.Add("Id", Id);
            AuthorUtils.Add("FirstName", FirstName);
            AuthorUtils.Add("LastName", LastName);
            AuthorUtils.Add("Email", Email);
            AuthorUtils.Add("Number", Number);
            header = new Dictionary<string, string>() {
                {"FirstName","FirstName"},
                {"LastName","LastName" },
                {"Email","Email"},
                {"Number","Number"},
            };
        }

        public Dictionary<string, string> Attrs(Author author)
        {
            return new Dictionary<string, string>()
            {
                {"FirstName",author.FirstName },
                {"LastName", author.LastName },
                {"Email",author.Email },
                {"Number", author.Number }
            };
        }
    }
}
