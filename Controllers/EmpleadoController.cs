﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using D_AlturaSystemAPI.Modelos;

using System.Data;
using System.Data.SqlClient;

namespace D_AlturaSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class EmpleadoController : Controller
    {

        private readonly string ConnectSQL, ConnectSQLTwo, ConnectSQLThree;

        public EmpleadoController(IConfiguration configuration)
        {
            ConnectSQL = configuration.GetConnectionString("ConnectSQL");
            ConnectSQLTwo = configuration.GetConnectionString("ConnectSQLTwo");
            ConnectSQLThree = configuration.GetConnectionString("ConnectSQLThree");
        }


        [HttpGet]
        [Route("Lista de Empleados")]
        public IActionResult Listado()
        {
            List<Empleado> listado = new List<Empleado>();

            try
            {

                using (var connection = new SqlConnection(ConnectSQL))
                {
                    connection.Open();
                    var cmd = new SqlCommand("pA_lista_empleados", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            listado.Add(new Empleado()
                            {
                                idempleado = Convert.ToInt32(rd["idempleado"]),
                                nombre = rd["nombre"].ToString(),
                                apellidos = rd["apellidos"].ToString(),
                                dni = rd["dni"].ToString(),
                                telefono = rd["telefono"].ToString(),
                                direccion = rd["direccion"].ToString(),
                                estado = rd["estado"].ToString()

                            });
                        }
                    }

                }
                return StatusCode(StatusCodes.Status200OK, new { message = "Correcto.", response = listado });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = error.Message, response = listado });
            }
        }


        [HttpGet]
        [Route("Obtener/{idempleado:int}")]

        public IActionResult Obtener(int idempleado)
        {
            List<Empleado> listado = new List<Empleado>();
            Empleado empleado = new Empleado();

            try
            {

                using (var connection = new SqlConnection(ConnectSQL))
                {
                    connection.Open();
                    var cmd = new SqlCommand("pA_lista_empleados", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            listado.Add(new Empleado()
                            {
                                idempleado = Convert.ToInt32(rd["idempleado"]),
                                nombre = rd["nombre"].ToString(),
                                apellidos = rd["apellidos"].ToString(),
                                dni = rd["dni"].ToString(),
                                telefono = rd["telefono"].ToString(),
                                direccion = rd["direccion"].ToString(),
                                estado = rd["estado"].ToString()

                            });
                        }
                    }

                }
                empleado = listado.Where(item => item.idempleado == idempleado).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { message = "Correcto.", response = empleado });

            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = error.Message, response = empleado });
            }
        }

        [HttpPost]
        [Route("Guardar Cambios")]

        public IActionResult Guardar([FromBody] Empleado objeto)
        {

            try
            {

                using (var connection = new SqlConnection(ConnectSQL))
                {
                    connection.Open();
                    var cmd = new SqlCommand("pA_guardar_empleado", connection);
                    cmd.Parameters.AddWithValue("nombre", objeto.nombre);
                    cmd.Parameters.AddWithValue("apellidos", objeto.apellidos);
                    cmd.Parameters.AddWithValue("dni", objeto.dni);
                    cmd.Parameters.AddWithValue("telefono", objeto.telefono);
                    cmd.Parameters.AddWithValue("direccion", objeto.direccion);
                    cmd.Parameters.AddWithValue("estado", objeto.estado);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();

                }

                return StatusCode(StatusCodes.Status200OK, new { message = "Correcto." });

            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = error.Message });
            }
        }

        [HttpPut]
        [Route("EditarDatos")]

        public IActionResult EditarDatos([FromBody] Empleado objeto)
        {

            try
            {

                using (var connection = new SqlConnection(ConnectSQL))
                {
                    connection.Open();
                    var cmd = new SqlCommand("pA_editar_empleado", connection);
                    cmd.Parameters.AddWithValue("idempleado", objeto.idempleado == 0 ? DBNull.Value : objeto.idempleado);
                    cmd.Parameters.AddWithValue("nombre", objeto.nombre is null ? DBNull.Value : objeto.nombre);
                    cmd.Parameters.AddWithValue("apellidos", objeto.apellidos is null ? DBNull.Value : objeto.apellidos);
                    cmd.Parameters.AddWithValue("dni", objeto.dni is null ? DBNull.Value : objeto.dni);
                    cmd.Parameters.AddWithValue("telefono", objeto.telefono is null ? DBNull.Value : objeto.telefono);
                    cmd.Parameters.AddWithValue("direccion", objeto.direccion is null ? DBNull.Value : objeto.direccion);
                    cmd.Parameters.AddWithValue("estado", objeto.estado is null ? DBNull.Value : objeto.estado);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();

                }

                return StatusCode(StatusCodes.Status200OK, new { message = "Datos editados." });

            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = error.Message });
            }
        }

        [HttpDelete]
        [Route("EliminarDatos/{idempleado:int}")]

        public IActionResult EliminarDatos(int idempleado)
        {

            try
            {

                using (var connection = new SqlConnection(ConnectSQL))
                {
                    connection.Open();
                    var cmd = new SqlCommand("pA_eliminar_empleado", connection);
                    cmd.Parameters.AddWithValue("idempleado", idempleado);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();

                }

                return StatusCode(StatusCodes.Status200OK, new { message = "Dato eliminado." });

            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = error.Message });
            }
        }
    }
}

