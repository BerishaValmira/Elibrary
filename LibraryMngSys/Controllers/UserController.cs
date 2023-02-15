using LibraryMngSys.Areas.Identity.Data;
using LibraryMngSys.Models.Role;
using LibraryMngSys.Models.User;
using LibraryMngSys.ViewModels;
using LibraryMngSys.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryMngSys.Controllers
{
    public class UserController : Controller
    {
        private readonly UserServices userServices;

        private readonly RoleServices roleServices;

        private readonly SignInManager<LibraryMngSysUser> signInManager;

        public UserController(UserServices _us, RoleServices _roleServices, SignInManager<LibraryMngSysUser> _signInManager)
        {
            userServices = _us;
            roleServices = _roleServices;
            signInManager = _signInManager;
        }

        [Authorize(Policy = "RequireAdmin")]
        public async Task<IActionResult> Index(
            [FromQuery]
            PageRequest<LibraryMngSysUser> request
            )
        {
            PageResponse<LibraryMngSysUser> response = await userServices.List(request);
            return View("_Table", response);
        }

        //GET
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) return NotFound();
            
            var user = await userServices.GetById(id);

            if (user == null)return NotFound();

            var roles = roleServices.List().Result;

            var userRoles = await signInManager.UserManager.GetRolesAsync(user);

            var roleItemsSelect = roles.Select(role => 
                new SelectListItem(role.Name, role.Id, 
                    userRoles.Any(ur => ur.Contains(role.Name))
                    )
                ).ToList();

            var vm = new EditUserViewModel
            {
                User = user ,
                Roles = roleItemsSelect
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel vm) {
            var user1 = await userServices.GetByEmail(vm.User.Email);

            var user =await userServices.GetById(vm.User.Id);

            if (user == null) return NotFound();

            if (user1 != null && user1.Id != user.Id)
            {
                ModelState.AddModelError("Email", "Email taken");
            }

            var userRoles = await signInManager.UserManager.GetRolesAsync(user);

            var rolesToAdd = new List<string>();
            var rolesToRemove = new List<string>();

            foreach(var role in vm.Roles)
            {
                var assignedInDb = userRoles.FirstOrDefault(u => u == role.Text);
                if (role.Selected)
                {
                    if(assignedInDb == null)
                    {
                        rolesToAdd.Add(role.Text);
                    }
                }
                else
                {
                    if (assignedInDb != null)
                    {
                        rolesToRemove.Add(role.Text);
                    }
                }
            }

            if (rolesToAdd.Any())
            {
                await signInManager.UserManager.AddToRolesAsync(user, rolesToAdd);
            }
            if (rolesToRemove.Any())
            {
                await signInManager.UserManager.RemoveFromRolesAsync(user, rolesToRemove);
            }

            user.Name = vm.User.Name;
            user.Surname = vm.User.Surname;
            user.Email = vm.User.Email;
            await userServices.Update(user);
            return RedirectToAction("Index");
        }
    }   
}
