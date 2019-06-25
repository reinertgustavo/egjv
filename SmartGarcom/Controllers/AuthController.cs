using System.Linq;
using SmartGarcom.Models;
using Microsoft.AspNetCore.Mvc;
using SmartGarcom.ViewModels;
namespace SmartGarcom.Controllers
{
    public class AuthController : Controller
    {
        private Banco db;
        public AuthController(Banco _db)
        {
            this.db = _db;
        }
        public void CriaAdmin()
        {

            if (this.db.TUsers.Count() == 0)
            {
                if (this.db.Roles.Count() == 0)
                {
                    Role role = new Role();
                    role.Name = "Administrador Geral";
                    role.Description = "Administrador Geral do Sistemas";
                    db.Roles.Add(role);
                    Role role2 = new Role();
                    role2.Name = "Administrador da empresa";
                    role2.Description = "Administrador geral da empresa";
                    db.Roles.Add(role2);
                    Role role3 = new Role();
                    role3.Name = "Usuário";
                    role3.Description = "Usuário comum do sistema";
                    db.Roles.Add(role3);

                    db.SaveChanges();
                }
                if (this.db.Companies.Count() == 0)
                {
                    Company cnpj = new Company();
                    cnpj.Cnpj = "00000000000";
                    cnpj.Name = "SmartSolutions";
                    cnpj.ZipCode = "89231-740";
                    db.Companies.Add(cnpj);
                    db.SaveChanges();
                }
                //Criar usuario inicial
                TUser admin = new TUser();
                admin.Email = "admin@admin.com";
                admin.Password = TUser.GenerateHash("admin123");
                admin.Name = "Administrador";
                admin.Role = db.Roles.FirstOrDefault();
                admin.Company = db.Companies.FirstOrDefault(); ;
                db.TUsers.Add(admin);
                db.SaveChanges();
            }
        }
        [HttpGet]
        public IActionResult Login()
        {
            if(ViewBag.TUser != null && ViewBag.TUser.Role.RoleId == 1)
            {
                return RedirectToRoute(new { area = "admin", controller = "home", action = "index" });
            }
            else
            {
                CriaAdmin();
                CommonVM vm = new CommonVM();
                return View(vm);
            }
        }

        [HttpPost]
        public IActionResult Login(CommonVM vm)
        {
            if (ModelState.IsValid)
            {
                var user = db.TUsers
                         .Where(m => m.Email.ToLower().Equals(vm.Loginvm.Email.ToLower()))
                         .FirstOrDefault();

                if (user == null)
                {
                    ModelState.AddModelError("Email", "Usuario Não Encontrado");
                    return View(vm);
                }

                string vmPasswordHash = TUser.GenerateHash(vm.Loginvm.Password);

                if (user.Password.Equals(vmPasswordHash))
                {
                    user.AuthToken = TUser.GenerateAuthToken();
                    db.SaveChanges();
                    Response.Cookies.Append(TUser.COOKIE_AUTH_TOKEN_NAME, user.AuthToken);
                    return RedirectToRoute(new {area = "admin", controller = "Home", action = "Index" });
                }
                else
                {
                    ModelState.AddModelError("Password", "Senha Incorreta");
                    return View(vm);
                }
            }
            return View(vm);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            var authToken = Request.Cookies[TUser.COOKIE_AUTH_TOKEN_NAME];
            if (authToken != null)
            {
                var user = db.TUsers
                             .Where(m => m.AuthToken == authToken)
                             .FirstOrDefault();
                user.AuthToken = null;
                db.SaveChanges();
            }
            return RedirectToRoute(new {controller = "auth", action = "login" });
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            CommonVM vm = new CommonVM();
            return View(vm);
        }

        [HttpPost]
        public IActionResult SignUp(CommonVM vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.SignUpvm.Password != vm.SignUpvm.PasswordConfirm)
                {
                    ModelState.AddModelError("PasswordConfirm", "As senhas informadas não são iguais");
                    return View(vm);
                }

                Role role = db.Roles
                              .Where(m => m.Name == "Usuário")
                              .FirstOrDefault();

                TUser user = new TUser
                {
                    Name = vm.SignUpvm.Name,
                    Email = vm.SignUpvm.Email,
                    Password = TUser.GenerateHash(vm.SignUpvm.Password),
                    Role = role
                    
                };
                db.TUsers.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(vm);
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}






















