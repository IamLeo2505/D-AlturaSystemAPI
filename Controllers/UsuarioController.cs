﻿﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using D_AlturaSystemAPI.Modelos;

using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace D_AlturaSystemAPI.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]

    public class UsuariosController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        private readonly string ConnectSQL, ConnectSQLTwo, ConnectSQLThree;

        public UsuariosController(IConfiguration configuration)
        {
            ConnectSQL = configuration.GetConnectionString("ConnectSQL");
            ConnectSQLTwo = configuration.GetConnectionString("ConnectSQLTwo");
            ConnectSQLThree = configuration.GetConnectionString("ConnectSQLThree");
        }


        [HttpGet]
        [Route("Lista de Usuarios")]
        public IActionResult Listado()
        {
            List<Usuario> listado = new List<Usuario>();

            try
            {

                using (var connection = new SqlConnection(ConnectSQL))
                {
                    connection.Open();
                    var cmd = new SqlCommand("pA_lista_usuario", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            listado.Add(new Usuario()
                            {
                                idusuario = Convert.ToInt32(rd["idusuario"]),
                                usuario = rd["usuario"].ToString(),
                                pass = rd["pass"].ToString(),
                                acceso = rd["acceso"].ToString(),
                                estado = rd["estado"].ToString(),
                                idempleado = Convert.ToInt32(rd["idempleado"]),
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
        [Route("Obtener/{idusuario:int}")]

        public IActionResult Obtener(int idusuario)
        {          
            List<Usuario> listado = new List<Usuario>();
            Usuario Usuarios = new Usuario();

            try
            {

                using (var connection = new SqlConnection(ConnectSQL))
                {
                    connection.Open();
                    var cmd = new SqlCommand("pA_lista_usuario", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            listado.Add(new Usuario()
                            {
                                idusuario = Convert.ToInt32(rd["idusuario"]),
                                usuario = rd["usuario"].ToString(),
                                pass = rd["pass"].ToString(),
                                acceso = rd["acceso"].ToString(),
                                estado = rd["estado"].ToString(),
                                idempleado = Convert.ToInt32(rd["idempleado"]),
                            });
                        }
                    }

                }
                Usuarios = listado.Where(item => item.idusuario == idusuario).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { message = "Correcto.", response = Usuarios });

            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = error.Message, response = Usuarios });
            }
        }

        [HttpPost]
        [Route("GuardarCambios")]

        public async Task<IActionResult> GuardarAsync([FromBody] Usuario objeto)
        {
            if (objeto == null || string.IsNullOrEmpty(objeto.usuario))
            {
                return BadRequest(new { message = "Los datos del usuario son invalidos" });
            }


            // Validar si el usuario ya existe
            var usuarioExistente = await _context.Usuarios.FirstOrDefaultAsync(u => u.usuario == objeto.usuario);
            if (usuarioExistente != null)
            {
                return Conflict(new { message = "El usuario ya existe" });
            }

            // Guardar el nuevo usuario en la base de datos
            _context.Usuarios.Add(objeto);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Usuario registrado correctamente" });

            /*try
            {

                using (var connection = new SqlConnection(ConnectSQL))
                {
                    connection.Open();
                    var cmd = new SqlCommand("pA_guardar_usuario", connection);
                    cmd.Parameters.AddWithValue("usuario", objeto.usuario);
                    cmd.Parameters.AddWithValue("pass", objeto.pass);
                    cmd.Parameters.AddWithValue("acceso", objeto.acceso);
                    cmd.Parameters.AddWithValue("estado", objeto.estado);
                    cmd.Parameters.AddWithValue("idempleado", objeto.idempleado);
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
        */
        }

        [HttpPut]
        [Route("EditarDatos")]

        public IActionResult EditarDatos([FromBody] Usuario objeto)
        {

            try
            {

                using (var connection = new SqlConnection(ConnectSQL))
                {
                    connection.Open();
                    var cmd = new SqlCommand("pA_editar_usuario", connection);
                    cmd.Parameters.AddWithValue("idusuario", objeto.idusuario == 0 ? DBNull.Value : objeto.idusuario);
                    cmd.Parameters.AddWithValue("usuario", objeto.usuario is null ? DBNull.Value : objeto.usuario);
                    cmd.Parameters.AddWithValue("pass", objeto.pass is null ? DBNull.Value : objeto.pass);
                    cmd.Parameters.AddWithValue("acceso", objeto.acceso is null ? DBNull.Value : objeto.acceso);
                    cmd.Parameters.AddWithValue("estado", objeto.estado is null ? DBNull.Value : objeto.estado);
                    cmd.Parameters.AddWithValue("idempleado", objeto.idempleado == 0 ? DBNull.Value : objeto.idempleado);
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
        [Route("EliminarDatos/{idusuario:int}")]

        public IActionResult EliminarDatos(int idusuario)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectSQL))
                {
                    connection.Open();
                    var cmd = new SqlCommand("pA_eliminar_usuario", connection);
                    cmd.Parameters.AddWithValue("idusuario", idusuario);
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
