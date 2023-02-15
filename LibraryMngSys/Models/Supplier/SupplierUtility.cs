using LibraryMngSys.Wrappers;

namespace LibraryMngSys.Models.Supplier
{
    public class SupplierUtility: Utility<Supplier>
    {
        public Dictionary<string, Func<Supplier, string>> SupplierUtils;

        public Dictionary<string, string> header;

        Func<Supplier, string> Name = x => x.Name;
        Func<Supplier, string> Id = x => x.Id.ToString();
        Func<Supplier, string> Address = x => x.Address;
        Func<Supplier, string> Email = x => x.Email;
        Func<Supplier, string> ContactNumber = x => x.ContactNumber;
        Func<Supplier, string> CreatedAt = x => x.createdAt.ToString();
        Func<Supplier, string> UpdatedAt = x => x.updatedAt.ToString();

        public SupplierUtility()
        {
            SupplierUtils = new Dictionary<string, Func<Supplier, string>>();
            SupplierUtils.Add("Name", Name);
            SupplierUtils.Add("Id", Id);
            SupplierUtils.Add("Address", Address);
            SupplierUtils.Add("Email", Email);
            SupplierUtils.Add("ContactNumber", ContactNumber);
            SupplierUtils.Add("createdAt", CreatedAt);
            SupplierUtils.Add("updatedAt", UpdatedAt);
            header = new Dictionary<string, string>() {
                {"Name","Name"},
                {"Address","Address"},
                {"Email","Email"},
                {"ContactNumber","ContactNumber"},

            };
        }

        public Dictionary<string, string> Attrs(Supplier supplier)
        {
            return new Dictionary<string, string>()
            {
                {"Name",supplier.Name },
                {"Address",supplier.Address },
                {"Email",supplier.Email },
                {"ContactNumber",supplier.ContactNumber }

            };
        }
    }
}
