using System;
using System.Collections.Generic;
using System.Text;

namespace DBEntity
{
    public class EntityImage : EntityBase
    {
        public int id_imagen { get; set; }
        public int id_producto_detalle { get; set; }
        public string nombre { get; set; }
        public string ruta { get; set; }
        public string tipo { get; set; }
    }
}
