using Microsoft.EntityFrameworkCore;
using MvcCorePaginacionRegistros.Models;

namespace MvcCorePaginacionRegistros.Data
{
    public class BBDDContext : DbContext
    {
        public BBDDContext(DbContextOptions<BBDDContext> options)
            : base(options) { }

        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<VistaDepartamento> VistaDepartamentos { get; set; }
        public DbSet<VistaEmpleado> VistaEmpleados { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
    }
}
