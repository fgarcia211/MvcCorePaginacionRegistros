using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcCorePaginacionRegistros.Data;
using MvcCorePaginacionRegistros.Models;

#region SQL

/*CREATE OR ALTER VIEW V_GRUPO_EMPLEADOS
AS
    SELECT CAST(
    ROW_NUMBER() OVER(ORDER BY EMP_NO) AS INT) AS POSICION,
    ISNULL(EMP_NO, 0) AS EMP_NO, APELLIDO, OFICIO, SALARIO, DEPT_NO 
    FROM EMP
GO

CREATE PROCEDURE SP_GRUPO_EMPLEADOS
(@POSICION INT)
AS
    SELECT *
    FROM V_GRUPO_EMPLEADOS
    WHERE POSICION >= @POSICION AND POSICION < (@POSICION + 3)
GO*/



/*CREATE OR ALTER PROCEDURE SP_GRUPO_EMPLEADOS_OFICIO(@POSICION INT, @OFICIO NVARCHAR(50))
AS
	SELECT * FROM (SELECT CAST(
		ROW_NUMBER() OVER(ORDER BY EMP_NO) AS INT) AS POSICION,
        ISNULL(EMP_NO, 0) AS EMP_NO, APELLIDO, OFICIO, SALARIO, DEPT_NO 
		FROM EMP WHERE OFICIO = @OFICIO) 
	AS QUERY
	WHERE QUERY.POSICION >= @POSICION AND QUERY.POSICION < (@POSICION+3)
GO*/

#endregion
namespace MvcCorePaginacionRegistros.Repositories
{
    public class RepositoryEmpleados
    {
        private BBDDContext context;

        public RepositoryEmpleados(BBDDContext context)
        {
            this.context = context;
        }

        public int GetTotalVistaEmpleados()
        {
            return this.context.VistaEmpleados.Count();
        }
        public List<Empleado> GetEmpleadosDept(int deptno)
        {
            var consulta = from datos in this.context.Empleados
                           where datos.DeptNo == deptno
                           select datos;
            return consulta.ToList();
        }

        public List<Empleado> GetEmpleadosPosicion(int posicion)
        {
            string sql = "SP_GRUPO_EMPLEADOS @POSICION";
            SqlParameter pamPos = new SqlParameter("@POSICION", posicion);
            var consulta = this.context.Empleados.FromSqlRaw(sql, pamPos);
            List<Empleado> empleados = consulta.AsEnumerable().ToList();
            return empleados;
        }

        public List<Empleado> GetEmpleadosPosicionOficio(int posicion, string oficio)
        {
            string sql = "SP_GRUPO_EMPLEADOS_OFICIO @POSICION, @OFICIO";
            SqlParameter pamPos = new SqlParameter("@POSICION", posicion);
            SqlParameter pamOfi = new SqlParameter("@OFICIO", oficio);
            var consulta = this.context.Empleados.FromSqlRaw(sql, pamPos, pamOfi);
            List<Empleado> empleados = consulta.AsEnumerable().ToList();
            return empleados;
        }

        public int GetTotalEmpleadoOficio(string oficio)
        {
            return this.context.VistaEmpleados.Count(x => x.Oficio == oficio);
        }

    }
}
