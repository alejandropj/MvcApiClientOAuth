using Microsoft.AspNetCore.Mvc;
using MvcApiClientOAuth.Models;
using MvcApiClientOAuth.Services;
using System.Security.Claims;

namespace MvcApiClientOAuth.Controllers
{
    public class EmpleadosController : Controller
    {
        private ServiceApiEmpleados service;
        public EmpleadosController (ServiceApiEmpleados service)
        {
            this.service = service;
        }
        [AuthorizeEmpleados]
        public async Task<IActionResult> Index()
        {
            List<Empleado> empleados = await this.service.GetEmpleadosAsync();
            return View(empleados);
        }
        [AuthorizeEmpleados]
        public async Task<IActionResult> Details(int id)
        {
            //HttpContext.User.FindFirst(x => x.Type == "TOKEN");
            //string token = HttpContext.Session.GetString("TOKEN");
/*            if (token == null)
            {
                ViewData["MENSAJE"]= "Debe validarse en Login";
                return View();
            }
            else
            {*/
                Empleado empleado = await this.service.FindEmpleadoAsync(id);
                return View(empleado);
            //}
            
        }        
/*        [AuthorizeEmpleados]
        public async Task<IActionResult> Perfil()
        {
            var data = HttpContext.User.FindFirst
                (x => x.Type == ClaimTypes.NameIdentifier).Value;
            int id = int.Parse(data);
            Empleado empleado = await this.service.FindEmpleadoAsync(id);
            return View(empleado);
        }   */     
        [AuthorizeEmpleados]
        public async Task<IActionResult> Perfil()
        {
            Empleado empleado = await this.service.GetPerfilEmpleadoAsync();
            return View(empleado);
        }        
        [AuthorizeEmpleados]
        public async Task<IActionResult> Compis()
        {
            List<Empleado> empleados = await this.service.GetCompisTrabajoAsync();
            return View(empleados);
        }
        public async Task<IActionResult> EmpleadosOficios()
        {
            List<string> oficios = await this.service.GetOficiosAsync();
            ViewData["OFICIOS"] = oficios;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EmpleadosOficios(int? incremento, List<string> oficio, string accion)
        {
            if (accion.ToLower() == "update")
            {
                await this.service.UpdateEmpleadosOficios(incremento.Value, oficio);
            }
            List<Empleado> empleados =
                await this.service.GetEmpleadosOficiosAsync
                (oficio);

            List<string> oficios = await this.service.GetOficiosAsync();
            ViewData["OFICIOS"] = oficios;
            return View(empleados);
        }
    }
}
