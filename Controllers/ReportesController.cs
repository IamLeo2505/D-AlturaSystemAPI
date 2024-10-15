using AspNetCore.Reporting;
using D_AlturaSystemAPI.Servicio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace D_AlturaSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly ServiciosBD serviciosBD;

        public ReportesController(ServiciosBD servicioBD)
        {
            serviciosBD = servicioBD;
        }

        [HttpGet("ReporteProveedores")]
        public IActionResult ObtenerDatosProveedores()
        {
            var dt = serviciosBD.ObtenerDatosProveedor();

            string path = Path.Combine(Directory.GetCurrentDirectory(), "Reportes", "Proveedores.rdlc");

            LocalReport report = new LocalReport(path);
            report.AddDataSource("dtProveedor", dt);

            var result = report.Execute(RenderType.Pdf);

            return File(result.MainStream, "application/pdf", "ReporteProveedores.pdf");
        }
    }
}