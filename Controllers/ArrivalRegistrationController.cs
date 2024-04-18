using System.Collections.Generic;
using Login.Data;
using Login.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Login.Filters;

namespace Login.Controllers
{
    public class ArrivalRegistrationController : Controller
    {

        public static DateTime Today { get; }
        public LoginContext _loginContext;
        public ArrivalRegistrationController(LoginContext loginContext){

            _loginContext = loginContext;
        } 

        public async Task<IActionResult> Index(){
            ViewBag.Names = HttpContext.Session.GetString("Names");
            ViewBag.Entry = await _loginContext.Registers.FirstOrDefaultAsync(r => r.EmployeeId == HttpContext.Session.GetInt32("Id") && r.Exits == null);
            return View();
        }
        public async Task<IActionResult> CheckIn(){
            try
            {
                var EmployeeId = HttpContext.Session.GetInt32("Id");
    
                var register = new Register(){
                     Entries =   DateTime.Now,
                     EmployeeId = EmployeeId
                };
               _loginContext.Registers.Add(register);
               await _loginContext.SaveChangesAsync();
               TempData["MessageSuccess"] = "Se ha generado el registro de entrada correctamente"; 
               return RedirectToAction("Index");  
            }
            catch (System.Exception)
            {
                // aqui mensaje a slack mediante webhook
                throw;
            }
        }
        public async Task<IActionResult> CheckOut(){
            try
            {
               var CheckOut = await _loginContext.Registers.FirstOrDefaultAsync(r => r.EmployeeId == HttpContext.Session.GetInt32("Id") && r.Exits == null);
               CheckOut.Exits = DateTime.Now;
               await _loginContext.SaveChangesAsync();
               TempData["MessageSuccess"] = "Se ha generado el registro de tu salida correctamente"; 
               return RedirectToAction("Index");  

            }
            catch (System.Exception)
            {
                // aqui mensaje a slack mediante webhook
                throw;
            }
        }

    }
}