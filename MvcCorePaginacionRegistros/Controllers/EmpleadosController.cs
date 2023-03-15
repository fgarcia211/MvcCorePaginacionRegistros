using Microsoft.AspNetCore.Mvc;
using MvcCorePaginacionRegistros.Models;
using MvcCorePaginacionRegistros.Repositories;

namespace MvcCorePaginacionRegistros.Controllers
{
    public class EmpleadosController : Controller
    {
        private RepositoryEmpleados repo;

        public EmpleadosController(RepositoryEmpleados repo)
        {
            this.repo = repo;
        }

        public IActionResult ListaEmpleados(int iddept)
        {
            return View(this.repo.GetEmpleadosDept(iddept));
        }

        public IActionResult EmpleadosXPosicion(int? posicion, int deptno)
        {
            if (posicion == null)
            {
                posicion = 1;
            }

            int numeroregistos = this.repo.GetTotalVistaEmpleados();
            ViewData["REGISTROS"] = numeroregistos;

            List<Empleado> empleados = this.repo.GetEmpleadosPosicion(posicion.Value);
            return View(empleados);
        }

        public IActionResult EmpleadosXOficio()
        {
            return View();
        }

        [HttpGet("Empleados/EmpleadosXOficio/{posicion}/{oficio}")]
        [HttpPost("Empleados/EmpleadosXOficio")]
        public IActionResult EmpleadosXOficio(int? posicion, string oficio)
        {
            if (posicion == null)
            {
                posicion = 1;
            }

            int numeroregistros = this.repo.GetTotalEmpleadoOficio(oficio);
            ViewData["REGISTROS"] = numeroregistros;
            ViewData["OFICIO"] = oficio;

            return View(this.repo.GetEmpleadosPosicionOficio(posicion.Value, oficio));
        }
    }
}
