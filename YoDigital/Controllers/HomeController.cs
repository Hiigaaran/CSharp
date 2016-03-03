using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YoDigital.Models;

namespace YoDigital.Controllers
{
    [RoutePrefix("Home")]
    public class HomeController : Controller
    {
        // GET: Home
        [Route("~/")]
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detalle()
        {
            return View();
        }

        public ActionResult Trabajador()
        {
            DataSet dsNull = new DataSet();
            DataTable dt = new DataTable("Trabajador");
            dt.Columns.Add(new DataColumn("Esperando La Consulta de la Informacion", typeof(string)));
            dsNull.Tables.Add(dt);
            return View(dsNull);
        }

        /*public ActionResult ReportesGenerales()
        {
            return View();
        }*/
        [Authorize]
        public ActionResult HomeReportes()
        {
            return View();
        }

        public ActionResult ReporteSemanal()
        {
            DataSet dsNull = new DataSet();
            DataTable dt = new DataTable("ReporteSemanal");
            dt.Columns.Add(new DataColumn("Esperando La Consulta de la Informacion", typeof(string)));
            dsNull.Tables.Add(dt);
            return View(dsNull);
        }

        public ActionResult ReportePersonalizado()
        {
            DataSet dsNull = new DataSet();
            DataTable dt = new DataTable("ReportePersonalizado");
            dt.Columns.Add(new DataColumn("Esperando La Consulta de la Informacion", typeof(string)));
            dsNull.Tables.Add(dt);
            return View(dsNull);
        }

        [HttpPost]
        public ActionResult Login(string txtUsuario, string txtPassword, string start, string end)
        {
            LoginTrabajador request = new LoginTrabajador();

            if (request.Login(txtUsuario, txtPassword) && start != "" && end != "")
            {
                //desde este ActionResult redirigo contenido a la vista Trabajador. 
                ViewBag.Saludo = "<h2 style = \"text-align:center\">Bienvenido " + request.QueryUsuario(txtUsuario) + "</h2><div style =\"text-align:center\" class = \"container\"><h3>El detalle de tus horas trabajadas es el siguiente:</h3></div>";
                return View("Trabajador", QueryHorasTrabajadas(start, end, txtUsuario));
            }
            else
            {
                DataSet dsNull = new DataSet();
                DataTable dt = new DataTable("Trabajador");
                dt.Columns.Add(new DataColumn("Esperando La Consulta de la Informacion", typeof(string)));
                dsNull.Tables.Add(dt);
                ViewBag.Resultado = "<div class = \"alert alert-danger\" style = \"text-align:center\" role = \"alert\">Los Datos de Usuario Ingresados o las Fechas son Invalidos, Intentelo nuevamente</div>";
                return View("Trabajador", dsNull);
            }
        }

        [HttpPost]
        public ActionResult ConsultaReporteSemanal(string start)
        {
            if (start != null)
            {
                return View("ReporteSemanal", QueryReporte(start, start, "NULL"));
            }
            else
            {
                ViewBag.Error = "<div class = \"alert alert-danger\" style = \"text-align:center\" role = \"alert\">Se tiene que ingresar una fecha para efectuar la consulta</div>";
                DataSet dsNull = new DataSet();
                DataTable dt = new DataTable("ReporteSemanal");
                dt.Columns.Add(new DataColumn("Esperando La Consulta de la Informacion", typeof(string)));
                dsNull.Tables.Add(dt);
                return View("ReporteSemanal", dsNull);
            }

        }

        [HttpPost]
        public ActionResult ConsultaReportePersonalizado(string start, string end)
        {
            if (start != "" && end != "")
            {
                return View("ReportePersonalizado", QueryReporte(start, end, "NULL"));
            }
            else
            {
                ViewBag.Error = "<div class = \"alert alert-danger\" style = \"text-align:center\" role = \"alert\">Se tiene que ingresar una fecha para efectuar la consulta</div>";
                DataSet dsNull = new DataSet();
                DataTable dt = new DataTable("ReportePersonalizado");
                dt.Columns.Add(new DataColumn("Esperando La Consulta de la Informacion", typeof(string)));
                dsNull.Tables.Add(dt);
                return View("ReportePersonalizado", dsNull);
            }
        }

        #region Querys
        public DataSet QueryHorasTrabajadas(string fechaInicio, string fechaFinal, string rut)
        {
            DataSet lista = new DataSet();
            Cloud cliente = new Cloud();

            lista = cliente.Ws.Data_SQL_Query("EXEC	[dbo].[SP_HORAS_TRABAJADAS] @fecha_inicio = N'" + Convert.ToDateTime(fechaInicio).ToString("yyyy-MM-dd").Replace("/", "-") + "', @fecha_termino = N'" + Convert.ToDateTime(fechaFinal).ToString("yyyy-MM-dd").Replace("/", "-") + "', @rut = " + rut, "YODIGITAL_NEW");

            return lista;
        }

        public DataSet QueryReporte(string fechaInicio, string fechaFinal, string rut)
        {
            DataSet lista = new DataSet();
            Cloud cliente = new Cloud();

            lista = cliente.Ws.Data_SQL_Query("EXEC	[dbo].[SP_RESUMEN_SEMANAL] @fecha_inicio = N'" + Convert.ToDateTime(fechaInicio).ToString("yyyy-MM-dd").Replace("/", "-") + "', @fecha_termino = N'" + Convert.ToDateTime(fechaFinal).ToString("yyyy-MM-dd").Replace("/", "-") + "', @fecha_mes = N'2016-02-01', @rut = " + rut, "YODIGITAL_NEW");

            #region Seteo Header
            lista.Tables[0].Columns[2].ColumnName = "Lunes";
            lista.Tables[0].Columns[3].ColumnName = "Martes";
            lista.Tables[0].Columns[4].ColumnName = "Miercoles";
            lista.Tables[0].Columns[5].ColumnName = "Jueves";
            lista.Tables[0].Columns[6].ColumnName = "Viernes";
            lista.Tables[0].Columns[7].ColumnName = "Sabado";
            lista.Tables[0].Columns[8].ColumnName = "Domingo";
            lista.Tables[0].Columns[9].ColumnName = "Prom Semanal";
            lista.Tables[0].Columns[10].ColumnName = "Total Semanal";
            lista.Tables[0].Columns[11].ColumnName = "Total Mensual";
            #endregion

            return lista;
        }
        #endregion
    }
}