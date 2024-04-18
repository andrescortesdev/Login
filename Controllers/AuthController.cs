using System.Diagnostics.Metrics;
using Login.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Login.Controllers
{
    public class AuthController : Controller
    {
        protected  LoginContext _contextLogin;
        public AuthController(LoginContext loginContext){
            _contextLogin = loginContext;   
        }
        public IActionResult Login() {
            try
            {
                return View();
            }
            catch (System.Exception)
            {
                // aqui mensaje a slack mediante webhook
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Login(string Document, string Password){
            try{
                var user = await _contextLogin.Employees.FirstOrDefaultAsync(x => x.Document == Document);
                if(user == null){ // si no existe un usuario con ese documento en la base de datos
                    TempData["message"] = "El documento no existe en la base de datos"; //mensaje temporal
                    return RedirectToAction("Login"); // redireccion al login para reintentar la autenticacion
                }else{ // si el usuario existe en la base de datos
                    if(user.Password == Password){ // comparacion de la contraseña de la base de datos con la que escribio el usuario
                         HttpContext.Session.SetString("Names", user.Names);
                         HttpContext.Session.SetInt32("Id", user.Id);
                         return RedirectToAction("Index", "ArrivalRegistration");
                    }else{
                        TempData["Message"] = "El documento o la contraseña no son correctos";
                        return RedirectToAction("Login");
                    }
                }
            }catch (System.Exception){
                
                throw;
            }
        }
        public IActionResult LogOut(){
            try
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Login");
            }
            catch (System.Exception)
            {
                // aqui mensaje a slack mediante webhook
                throw;
            }
        }
    }
}