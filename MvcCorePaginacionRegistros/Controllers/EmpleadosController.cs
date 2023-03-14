using Microsoft.AspNetCore.Mvc;
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
    }
}
