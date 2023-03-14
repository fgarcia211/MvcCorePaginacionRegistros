using Microsoft.AspNetCore.Mvc;
using MvcCorePaginacionRegistros.Repositories;

namespace MvcCorePaginacionRegistros.ViewComponents
{
    public class DepartamentosViewComponent : ViewComponent
    {
        private RepositoryDepartamentos repo;

        public DepartamentosViewComponent(RepositoryDepartamentos repo)
        {
            this.repo = repo;
        }

        //AQUI PODRIAMOS TENER CUALQUIER METODO DE UNA CLASE
        //LA PETICION SE REALIZA MEDIANTE EL METODO InvokeAsync
        //ES OBLIGATORIO TENERLO

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(this.repo.GetDepartamentos());
        }
    }

}
