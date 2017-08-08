using Model.DAO;
using Model.DB_Context;
using Model.ViewModel;
using System.Web.Mvc;

namespace MiPrimerMVC.Controllers
{
    [RoutePrefix("Home")]
    public class HomeController : Controller
    {
        private AlumnoDAO alumnoDAO = new AlumnoDAO();
        private CursoDAO cursoDAO = new CursoDAO();
        private AlumnoCurso alumnoCurso = new AlumnoCurso();
        private Curso curso = new Curso();

        // localhost:######/home/index
        // localhost:######/home
        public ActionResult Index()
        {
            return View(alumnoDAO.Listar());
        }

        //Home/Ver/1
        public ActionResult Ver(long id)
        {  
            return View(alumnoDAO.Obtener(id));
        }

        /// <summary>
        /// Retorno por View Bag se envia toda la ista de Cursos 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PartialViewResult Cursos(int? id = 0)
        {
            ViewBag.Cursos = cursoDAO.GetAllCourses(id);
            alumnoCurso.AlumnoId = id.Value;

            return PartialView(alumnoCurso);
        }

        [HttpGet]
        [Route("CursosTest/{id}")]
        public PartialViewResult CursosTest(int? id)
        {
            ViewBag.Cursos = cursoDAO.GetAllCourses(id);
            alumnoCurso.AlumnoId = id.Value;

            return PartialView(alumnoCurso);
        }


        //Cuando se envian parametros desde un Formulario desde la vista
        //public ActionResult Guardar(Alumno alumno, int algo, int[] array) // "Algo" o "Array" puede ser de tipo "string" en este caso como se sabe del valor sera "int"
        //{
        //    //Parametros: /"Controlador"/"Accion"
        //    return Redirect("~/Home/index");
        //}

        public ActionResult Crud(int id = 0)
        {
            return View(id == 0 ? new Alumno() 
                                : alumnoDAO.Obtener(id));
        }

        /// <summary>
        /// Metodo MVC por el Framework de .NET por el Formulario
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //public ActionResult Guardar(Alumno model)
        //{
        //    if (ModelState.IsValid) //--> IsValid es la validacion del Modelo serializado (Alumno model), y es el que actica los mensajes de validación
        //    {
        //        alumnoDAO.Upsert(model);
        //        return Redirect("~/home");
        //    }
        //    else
        //    {
        //        return View("~/views/home/crud.cshtml", model);
        //    }
        //}

        /// <summary>
        /// Mismo Metodo que el anterior pero regresando un JSON Serializado
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult Guardar(Alumno model)
        {
            var rm = new ResponseModel();

            if (ModelState.IsValid)//--> IsValid es la validacion del Modelo serializado (Alumno model), y es el que actica los mensajes de validación
            {
                rm = alumnoDAO.Upsert(model);

                if (rm.response)
                {
                    rm.message = "Registro exitoso";
                    rm.href = Url.Content("~/home");
                }
            }

            return Json(rm);
        }

        public ActionResult Eliminar(int id = 0)
        {
            alumnoDAO.Eliminar(id);
            return Redirect("~/home");
        }
    }
}