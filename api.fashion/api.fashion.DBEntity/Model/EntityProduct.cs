using System;
using System.Collections.Generic;
using System.Text;

namespace DBEntity
{
    public class EntityProduct : EntityBase
    {
        public int id_producto { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int id_categoria { get; set; }
        public string categoria { get; set; }
        public int id_marca { get; set; }
        public string marca { get; set; }
        public decimal precio { get; set; }
        public List<EntityProductDetail> productoDetalles { get; set; }
    }
}
