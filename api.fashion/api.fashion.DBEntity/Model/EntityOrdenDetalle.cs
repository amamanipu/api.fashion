using System;
using System.Collections.Generic;
using System.Text;

namespace api.fashion.DBEntity.Model
{
    public class EntityOrdenDetalle
    {
        public Int64 ID_ORDEN { get; set; }
        public int ID_PRODUCTO { get; set; }
        public int cantidad { get; set; }
        public float precio { get; set; }
    }
}
