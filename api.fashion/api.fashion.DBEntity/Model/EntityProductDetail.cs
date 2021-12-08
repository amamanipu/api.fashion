using System;
using System.Collections.Generic;
using System.Text;

namespace DBEntity
{
    public class EntityProductDetail : EntityBase
    {
        public int id_producto_detalle { get; set; }
        public int id_producto { get; set; }
        public int id_tamano { get; set; }
        public string color { get; set; }
        public int stock { get; set; }
        public List<EntityImage> imagenes { get; set; }
    }
}
