using LibraryMngSys.Wrappers;

namespace LibraryMngSys.Models.Shop
{
    public class ShopUtility: Utility<Shop>
    {
        public Dictionary<string, Func<Shop, string>> ShopUtils;

        public Dictionary<string, string> header;

        Func<Shop, string> Name = x => x.Name;
        Func<Shop, string> Id = x => x.Id.ToString();
        Func<Shop, string> Address = x => x.Address;
        Func<Shop, string> OpeningDate = x => x.OpeningDate.ToString();
        Func<Shop, string> Location = x => x.Location;

        public ShopUtility()
        {
            ShopUtils = new Dictionary<string, Func<Shop, string>>();
            ShopUtils.Add("Name", Name);
            ShopUtils.Add("Id", Id);
            ShopUtils.Add("Address", Address);
            ShopUtils.Add("OpeningDate", OpeningDate);
            ShopUtils.Add("Location", Location);
            header = new Dictionary<string, string>() {
                {"Name","Name"},
                {"Address","Address"},
                {"OpeningDate","OpeningDate"},
                {"Location","Location"},

            };
        }

        public Dictionary<string, string> Attrs(Shop shop)
        {
            return new Dictionary<string, string>()
            {
                {"Name",shop.Name },
                {"Address",shop.Address },
                {"Opening Date",shop.OpeningDate.ToString() },
                {"Location",shop.Location },

            };
        }
    }
}
