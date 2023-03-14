using Microsoft.AspNetCore.Mvc;
using MvcCorePaginacionRegistros.Models;
using MvcCorePaginacionRegistros.Repositories;

namespace MvcCorePaginacionRegistros.Controllers
{
    public class PaginacionController : Controller
    {
        private RepositoryHospital repo;

        public PaginacionController(RepositoryHospital repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> PaginarRegistroVistaDepartamento(int? posicion)
        {
            if (posicion == null)
            {
                posicion = 1;
            }

            int numregistros = this.repo.GetNumeroRegistrosVistaDepartamentos();
            int siguiente = posicion.Value + 1;
            int anterior = posicion.Value - 1;

            if (anterior == 0)
            {
                anterior = 1;
            }

            if (siguiente == numregistros + 1)
            {
                siguiente = numregistros;
            }

            VistaDepartamento vista = await this.repo.GetVistaDepartamento(posicion.Value);
            ViewData["ULTIMO"] = numregistros;
            ViewData["ANTERIOR"] = anterior;
            ViewData["SIGUIENTE"] = siguiente;

            return View(vista);
        }

        public async Task<IActionResult> PaginarGrupoVistaDepartamento(int? posicion)
        {
            if (posicion == null)
            {
                posicion = 1;
            }

            int numregistros = this.repo.GetNumeroRegistrosVistaDepartamentos();
            ViewData["REGISTROS"] = numregistros;

            int numeroPagina = 1;

            string html = "<div>";

            for (int i=1; i <= numregistros; i += 2)
            {
                html += "<a href='PaginarGrupoVistaDepartamento?posicion=" + i + "'>Página " + numeroPagina + "</a> |";
            }

            html += "</div>";
            ViewData["LINKS"] = html;
            List<VistaDepartamento> departamentos = await this.repo.GetGrupoVistaDepartamento(posicion.Value);
            return View(departamentos);
        }

        public async Task<IActionResult> PaginarGrupoDepartamento(int? posicion)
        {
            if (posicion == null)
            {
                posicion = 1;
            }

            int numregistros = this.repo.GetNumeroRegistrosVistaDepartamentos();
            ViewData["REGISTROS"] = numregistros;

            List<Departamento> departamentos = await this.repo.GetGrupoDepartamentosAsync(posicion.Value);
            return View(departamentos);
        }
    }
}
