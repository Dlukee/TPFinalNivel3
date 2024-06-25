using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using System.IO;
using System.Configuration;
using System.Data;

namespace Negocio
{
    public class ArticulosNegocio
    {
        public List<Articulos> listar()
        {
            List<Articulos> lista = new List<Articulos>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.consulta("select A.Id, A.Codigo, A.Nombre, A.Descripcion, M.Descripcion Marca, C.Descripcion Categoria, A.IdMarca, A.IdCategoria, A.ImagenUrl, A.Precio from ARTICULOS A, CATEGORIAS C, MARCAS M where A.IdMarca = M.Id and A.IdCategoria = C.Id");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulos aux = new Articulos();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    aux.Marcas = new Marcas();
                    aux.Marcas.Id = (int)datos.Lector["IdMarca"];
                    aux.Marcas.Descripcion = (string)datos.Lector["Marca"];

                    aux.Categorias = new Categorias();
                    aux.Categorias.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categorias.Descripcion = (string)datos.Lector["Categoria"];

                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                    aux.ImagenUrl = (string)datos.Lector["ImagenUrl"]; 

                    aux.Precio = Convert.ToInt32(datos.Lector["Precio"]);

                    lista.Add(aux);
                }
                return lista;
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
        public void agregar(Articulos nuevo)
        {
            {
                AccesoDatos datos = new AccesoDatos();

                try
                {
                    datos.consulta("INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio) VALUES (@Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @ImagenUrl, @Precio)");
                    datos.parametros("@Codigo", nuevo.Codigo);
                    datos.parametros("@Nombre", nuevo.Nombre);
                    datos.parametros("@Descripcion", nuevo.Descripcion);
                    datos.parametros("@IdMarca", nuevo.Marcas.Id);
                    datos.parametros("@IdCategoria", nuevo.Categorias.Id);
                    datos.parametros("@ImagenUrl", nuevo.ImagenUrl);
                    datos.parametros("@Precio", nuevo.Precio);
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
        }
        public void eliminar(int Id)
        {
            Articulos articulos = new Articulos();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.consulta("delete from ARTICULOS where Id = @Id");
                datos.parametros("@Id", Id);
                datos.ejecutarAccion();

            }

            catch (IOException ex)
            {
                throw ex;
            }

            finally
            {
                datos.cerrarConexion();
            }
        }
        public void eliminarLogica(int Id)
        {
            AccesoDatos datos = new AccesoDatos();
            datos.consulta("update ARTICULOS set Activo = 0 where id = @Id");
            datos.parametros("@Id", Id);
            datos.ejecutarAccion();
        }
        public void modificar(Articulos articulos)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.consulta("update ARTICULOS set Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, IdMarca = @IdMarca, IdCategoria = @IdCategoria, ImagenUrl = @ImagenUrl, Precio = @Precio where Id = @Id");
                datos.parametros("@Codigo", articulos.Codigo);
                datos.parametros("@Nombre", articulos.Nombre);
                datos.parametros("@Descripcion", articulos.Descripcion);
                datos.parametros("@IdMarca", articulos.Marcas.Id);
                datos.parametros("@IdCategoria", articulos.Categorias.Id);
                datos.parametros("@ImagenUrl", articulos.ImagenUrl);
                datos.parametros("@Precio", articulos.Precio);
                datos.parametros("@Id", articulos.Id);
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
        public void informacion(Articulos articulos)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.consulta("update ARTICULOS set Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcon, IdMarca = @IdMarca, IdCategoria = IdCategoria, ImagenUrl = @ImagenUrl, Precio = @Precio where Id = @Id");
                datos.parametros("@Codigo", articulos.Codigo);
                datos.parametros("@Nombre", articulos.Nombre);
                datos.parametros("@Descripcon", articulos.Descripcion);
                datos.parametros("@IdMarca", articulos.Marcas.Id);
                datos.parametros("@IdCategoria", articulos.Categorias.Id);
                datos.parametros("@ImagenUrl", articulos.ImagenUrl);
                datos.parametros("@Precio", articulos.Precio);
                datos.parametros("@Id", articulos.Id);
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
        public List<Articulos> filtrar(string campo, string criterio, string filtroAvanzado)
        {
            List<Articulos> lista = new List<Articulos>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = ("select A.Id, A.Codigo, A.Nombre, A.Descripcion, M.Descripcion Marca, C.Descripcion Categoria, A.IdMarca, A.IdCategoria, A.ImagenUrl, A.Precio from ARTICULOS A, CATEGORIAS C, MARCAS M where A.IdMarca = M.Id and A.IdCategoria = C.Id And ");


                if (campo == "Precio")
                {
                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += "Precio > " + filtroAvanzado;
                            break;
                        case "Menor a":
                            consulta += "Precio < " + filtroAvanzado;
                            break;
                        case "Igual a":
                            consulta += "Precio = " + filtroAvanzado;
                            break;
                    }
                }
                else if (campo == "Categoria")
                {
                    switch (criterio)
                    {
                        case "Empieza con":
                            consulta += "C.Descripcion like '" + filtroAvanzado + "%' ";
                            break;
                        case "Termina con":
                            consulta += "C.Descripcion like '%" + filtroAvanzado + "' ";
                            break;
                        case "Contiene":
                            consulta += "C.Descripcion like '%" + filtroAvanzado + "%' ";
                            break;
                    }
                }
                else if (campo == "Nombre")
                {
                    switch (criterio)
                    {
                        case "Empieza con":
                            consulta += "A.Nombre like '" + filtroAvanzado + "%'";
                            break;
                        case "Termina con":
                            consulta += "A.Nombre like '%" + filtroAvanzado + "' ";

                            break;
                        case "Contiene":
                            consulta += "A.Nombre like '%" + filtroAvanzado + "%' ";

                            break;
                    }
                }
                else
                {
                    switch (criterio)
                    {
                        case "Empieza con":
                            consulta += "M.Descripcion like '" + filtroAvanzado + "%' ";
                            break;
                        case "Termina con":
                            consulta += "M.Descripcion like '%" + filtroAvanzado + "' ";
                            break;
                        case "Contiene":
                            consulta += "M.Descripcion like '%" + filtroAvanzado + "%' ";
                            break;
                    }
                }


                datos.consulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulos aux = new Articulos();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    aux.Marcas = new Marcas();
                    aux.Marcas.Id = (int)datos.Lector["IdMarca"];
                    aux.Marcas.Descripcion = (string)datos.Lector["Marca"];

                    aux.Categorias = new Categorias();
                    aux.Categorias.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categorias.Descripcion = (string)datos.Lector["Categoria"];

                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];

                    aux.Precio = Convert.ToInt32(datos.Lector["Precio"]);

                    lista.Add(aux);
                }

                return lista;
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

        public bool UrlValida(string url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult))
            {
                return uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps;
            }
            return false;
        }
    }
}


