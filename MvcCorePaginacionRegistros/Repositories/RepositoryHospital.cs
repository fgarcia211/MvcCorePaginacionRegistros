using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcCorePaginacionRegistros.Data;
using MvcCorePaginacionRegistros.Models;

namespace MvcCorePaginacionRegistros.Repositories
{
    public class RepositoryHospital
    {
        private BBDDContext context;

        public RepositoryHospital(BBDDContext context)
        {
            this.context = context;
        }

        public int GetNumeroRegistrosVistaDepartamentos()
        {
            return this.context.VistaDepartamentos.Count();
        }

        public async Task<VistaDepartamento> GetVistaDepartamento(int posicion)
        {
            VistaDepartamento vista = await this.context.VistaDepartamentos.FirstOrDefaultAsync(x => x.Posicion == posicion);
            return vista;
        }

        public async Task<List<VistaDepartamento>> GetGrupoVistaDepartamento(int posicion)
        {
            var consulta = from datos in this.context.VistaDepartamentos
                           where datos.Posicion >= posicion
                           && datos.Posicion < (posicion + 2)
                           select datos;

            return await consulta.ToListAsync();
        }

        public async Task<List<Departamento>> GetGrupoDepartamentosAsync(int posicion)
            {
                string sql = "SP_GRUPO_DEPARTAMENTOS @POSICION";
                SqlParameter pamposicion =
                    new SqlParameter("@POSICION", posicion);
                var consulta =
                    this.context.Departamentos.FromSqlRaw(sql, pamposicion);
                return await consulta.ToListAsync();
            }

    }
}
