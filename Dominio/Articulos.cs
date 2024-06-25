using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Articulos
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string ImagenUrl { get; set; }
        public Categorias Categorias { get; set; }
        public Marcas Marcas { get; set; }
        public int Precio { get; set; }
        public bool Activo { get; set; }
    }
}
