using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class UsuarioNegocio
    {
        public bool InicioSesion(Usuarios usuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.consulta("select id, email, pass, nombre, apellido, UrlImagenPerfil, admin from USERS where email = @email AND pass = @pass ");
                datos.parametros("@email", usuario.Email);
                datos.parametros("@pass", usuario.Pass);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    usuario.Id = (int)datos.Lector["id"];
                    usuario.Admin = (bool)datos.Lector["admin"];

                    if (!(datos.Lector["nombre"] is DBNull))
                    {
                        usuario.Nombre = (string)datos.Lector["nombre"];
                    }

                    if (!(datos.Lector["apellido"] is DBNull))
                    {
                        usuario.Apellido = (string)datos.Lector["apellido"];
                    }

                    if (!(datos.Lector["UrlImagenPerfil"] is DBNull))
                    {
                        usuario.UrlImagenPerfil = (string)datos.Lector["UrlImagenPerfil"];
                    }
                    
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void actualizar(Usuarios usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.consulta("Update USERS set nombre = @nombre, apellido = @apellido, UrlImagenPerfil = @UrlImagenPerfil where Id = @Id");
                datos.parametros("@Id", usuario.Id);
                datos.parametros("@nombre", usuario.Nombre);
                datos.parametros("@apellido", usuario.Apellido);
                //datos.parametros("@imagenPerfil", usuario.ImagenPerfil != null ? usuario.ImagenPerfil : (object)DBNull.Value);
                datos.parametros("@UrlImagenPerfil", (object)usuario.UrlImagenPerfil ?? DBNull.Value);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public int agregarUsuario(Usuarios nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.consulta("insert into USERS (email, pass, admin) output inserted.id values (@Email, @Pass, @admin)");
                datos.parametros("@email", nuevo.Email);
                datos.parametros("@Pass", nuevo.Pass);
                datos.parametros("@admin", 0);
                return datos.ejecutarAccionScalar();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
