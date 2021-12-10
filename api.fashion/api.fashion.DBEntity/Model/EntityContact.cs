using System;
using System.Collections.Generic;
using System.Text;

namespace DBEntity
{
    public class EntityContact : EntityBase
    {
        public int id_contacto { get; set; }
        public string nombres { get; set; }
        public string correo { get; set; }
        public string comentarios { get; set; }
        

    }
}
