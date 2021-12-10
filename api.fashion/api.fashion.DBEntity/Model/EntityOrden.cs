using api.fashion.DBEntity.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBEntity
{
    public class EntityOrden
    {
        public Int64 ID_ORDEN { get; set; }
        public int ID_CLIENTE { get; set; }
        public DateTime FEC_REGISTRO { get; set; }
        public DateTime FEC_ENTREGA { get; set; }
        public string METODO_PAGO { get; set; }
        public string DIRECCION_ENTREGA { get; set; }
        public string REFERENCIA_ENTREGA { get; set; }
        public float IMP_TOTAL { get; set; }
        public string ESTADO_ORDEN { get; set; }

        public List<EntityOrdenDetalle> detalle { get; set; }
    }
}
