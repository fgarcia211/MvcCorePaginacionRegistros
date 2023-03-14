using MvcCorePaginacionRegistros.Data;
using MvcCorePaginacionRegistros.Models;

namespace MvcCorePaginacionRegistros.Repositories
{
    public class RepositoryEmpleados
    {
        private BBDDContext context;

        public RepositoryEmpleados(BBDDContext context)
        {
            this.context = context;
        }

        public List<Empleado> GetEmpleadosDept(int deptno)
        {
            var consulta = from datos in this.context.Empleados
                           where datos.DeptNo == deptno
                           select datos;
            return consulta.ToList();
        }
    }
}
