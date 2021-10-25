using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Web;
using System.Web.Mvc;
using MVCApplication.Models;

namespace MVCApplication.Controllers
{
    public class ProvinciaController : Controller
    {
        IList<ProvinciaModel> provincias = new List<ProvinciaModel>()
        {
            new ProvinciaModel() { Id = 1, Descripcion = "Buenos Aires" },
            new ProvinciaModel() { Id = 2, Descripcion = "Córdoba" },
            new ProvinciaModel() { Id = 3, Descripcion = "Entre Ríos"}
        };

        public string CargarPopup()
        {
            DatosPopup datos = new DatosPopup();
            datos.InSitu = true;
            datos.Tercerizado = false;
            datos.TiposTrabajo = new List<TipoTrabajo>();
            datos.TiposTrabajo.Add(new TipoTrabajo(){Id=1, Descripcion = "Tipo1"});
            datos.TiposTrabajo.Add(new TipoTrabajo(){Id=2, Descripcion = "Tipo2"});
            datos.TiposTrabajo.Add(new TipoTrabajo(){Id=3, Descripcion = "Tipo3"});

            System.IO.StringWriter sw = new System.IO.StringWriter();
            ViewEngineResult ver = ViewEngines.Engines.FindPartialView(this.ControllerContext, "DatosPopup");
            this.ViewData.Model = datos;
            ViewContext vc = new ViewContext(this.ControllerContext, ver.View, this.ViewData, this.TempData, sw);

            ver.View.Render(vc, sw);
            return sw.GetStringBuilder().ToString();
        }

        public string GuardarDatosPopup(DatosPopup datosPopup)
        {
            string respuesta = "";

            try
            {
                if (ModelState.IsValid)
                {
                    respuesta = "TODO OK!"; // TODO: realizar las operaciones necesarias de la ViewModel
                }
            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }

            return respuesta;
        }
        
        // GET: Provincia
        public ActionResult Index()
        {
            IList<ProvinciaModel> provincias = this.provincias;
            
            return View(provincias);
        }

        [HttpGet]
        public ActionResult Edit(int idProvincia)
        {
            IList<ProvinciaModel> provincias = this.provincias;

            //!!!! Esto deberia resolverse con la consulta al negocio, es solo de ejemplo
            ProvinciaModel provincia = (
                    from prov in provincias
                    where prov.Id == idProvincia
                    select prov).First();

            return View("Edit", provincia);
        }
        [HttpPost]
        public ActionResult Edit(ProvinciaModel provincia)
        {
            ProvinciaModel aReemplazar = this.provincias.FirstOrDefault(e => e.Id == provincia.Id);
            if (ModelState.IsValid || aReemplazar == null)
            {
                // Aqui iria el codigo para almacenar los cambios
                // simplemente voy a modificar la lista del metodo
                int index = this.provincias.IndexOf(aReemplazar);
                this.provincias[index] = provincia;
                return RedirectToAction("Index");
            }

            return View("Edit", provincia);
        }
        [HttpGet]
        public ActionResult Delete(int idProvincia)
        {
            IList<ProvinciaModel> provincias = this.provincias;

            //!!!! Esto deberia resolverse con la consulta al negocio, es solo de ejemplo
            ProvinciaModel provincia = (
                from prov in provincias
                where prov.Id == idProvincia
                select prov).First();
            
            return View("Delete", provincia);

        }
        [HttpPost]
        public ActionResult Delete(ProvinciaModel provincia)
        {
            IList<ProvinciaModel> provincias = this.provincias;
            ProvinciaModel aBorrar = this.provincias.FirstOrDefault(e => e.Id == provincia.Id);

            this.provincias.RemoveAt(this.provincias.IndexOf(aBorrar));

            return RedirectToAction("Index");

        }
    }
}