using Model.DB_Context;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Model.DAO
{
    public class AlumnoDAO
    {

        public List<Alumno> Listar()
        {
            var result = new List<Alumno>();

            try
            {
                using (TestContext context = new TestContext())
                {
                    result = context.Alumno.ToList();
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            return result;
        }

        public Alumno Obtener(long id)
        {
            var result = new Alumno();
            try
            {
                using (TestContext context = new TestContext())
                {
                    //1)
                    //result = (from alum in context.Alumno
                    //          where alum.IdAlumno == id
                    //          select alum).FirstOrDefault();
                    //2)
                    //result = context.Alumno.Where(p => p.IdAlumno == id).FirstOrDefault();

                    //3)Lazzy Preload Data --> ".Include("AlumnoCurso").Include("AlumnoCurso.Curso")"  , -> "AlumnoCurso" / "AlumnoCurso.Curso" son os nombres de los POCOS (Objetos Inizializados)
                    result = context.Alumno.Include("AlumnoCurso")
                        .Include("AlumnoCurso.Curso")
                        .Where(p => p.IdAlumno == id)
                        .SingleOrDefault();

                }      
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        /// <summary>
        /// Insert / update Method
        /// </summary>
        /// <param name="alumno"></param>
        /// <returns></returns>
        public ResponseModel Upsert(Alumno alumno)
        {
            var rm = new ResponseModel();

            try
            {
                using (TestContext context = new TestContext())
                {
                    if (alumno.IdAlumno != 0)
                    {
                        context.Entry(alumno).State = EntityState.Modified;
                    } else
                    {
                        context.Entry(alumno).State = EntityState.Added;
                    }

                    rm.SetResponse(true);
                    context.SaveChanges();
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return rm;
        }

        /// <summary>
        /// Eliminar DAO
        /// </summary>
        /// <param name="id"></param>
        public void Eliminar(int id = 0)
        {
            try
            {
                var alumno = this.Obtener(id);


                using (TestContext context = new TestContext())
                {
                    context.Entry(alumno).State = EntityState.Deleted;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
