using Model.DB_Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.DAO
{
    /// <summary>
    /// Clase de Acceso a datos para los cursos
    /// </summary>
    public class CursoDAO
    {
        /// <summary>
        /// Consulta de cursos almacenados
        /// </summary>
        /// <returns>Lista de cursos</returns>
        public List<Curso> GetAllCourses(int? idAlumnoId = 0)
        {
            var cursos = new List<Curso>();
            try
            {
                using (var context = new TestContext())
                {
                    cursos = context.Curso.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return cursos;
        }
    }
}
