using LibraryMngSys.Wrappers;
using Microsoft.IdentityModel.Tokens;

namespace LibraryMngSys.Models.Category
{
    public class CategoryUtility: Utility<Category>
    {
        public Dictionary<string, Func<Category, string>> CategoryUtils;

        public Dictionary<string, string> header;

        Func<Category, string> Id = x => x.Id.ToString();
        Func<Category, string> Name = x => x.Name;
        Func<Category, string> Description = x => x.Description;


        public CategoryUtility()
        {
            CategoryUtils = new Dictionary<string, Func<Category, string>>();
            CategoryUtils.Add("Id", Id);
            CategoryUtils.Add("Name", Name);
            CategoryUtils.Add("Description", Description);
            header = new Dictionary<string, string>() {
                {"Name","Name"},
                {"Description","Description"},
            };
        }


        public Dictionary<string, string> Attrs(Category category)
        {
            return new Dictionary<string, string>()
            {
                {"Name",category.Name },
                {"Description",category.Description },

            };
        }
    }



}
