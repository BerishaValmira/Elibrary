using LibraryMngSys.Areas.Identity.Data;
using LibraryMngSys.Wrappers;

namespace LibraryMngSys.Models.User
{
    public class UserUtility : Utility<LibraryMngSysUser>
    {
        public Dictionary<string, Func<LibraryMngSysUser, string>> userUtils;

        public Dictionary<string, string> header;

        Func<LibraryMngSysUser, string> Name = x => x.Name;
        Func<LibraryMngSysUser, string> Id = x => x.Id.ToString();
        Func<LibraryMngSysUser, string> Surname = x => x.Surname;
        Func<LibraryMngSysUser, string> Email = x => x.Email;

        public UserUtility()
        {
            userUtils = new Dictionary<string, Func<LibraryMngSysUser, string>>();
            userUtils.Add("Name", Name);
            userUtils.Add("Id", Id);
            userUtils.Add("Surname", Surname);
            userUtils.Add("Email", Email);
            header = new Dictionary<string, string>() {
                {"Name","Name"},
                {"Surname","Surname"},
                {"Email","Email"},
            };
        }

        public Dictionary<string, string> Attrs(LibraryMngSysUser LibraryMngSysUser)
        {
            return new Dictionary<string, string>()
            {
                { "Name", LibraryMngSysUser.Name },
                {"Surname",LibraryMngSysUser.Surname },
                { "Email",LibraryMngSysUser.Email}
            };
        }

        
    }
}
