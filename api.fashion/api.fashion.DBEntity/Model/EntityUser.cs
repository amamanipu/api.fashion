using System;
using System.Collections.Generic;
using System.Text;

namespace DBEntity
{
    public class EntityUser : EntityBase
    {
        public int id_usuario { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int id_perfil { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string documentoidentidad { get; set; }
        public string direccion { get; set; }
        public string celular { get; set; }

    }
}
