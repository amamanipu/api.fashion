using System;
using System.Collections.Generic;
using System.Text;

namespace DBEntity
{
    public class EntityLoginResponse
    {
        public int id_usuario { get; set; }
        public int id_perfil { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string documentoidentidad { get; set; }
        public string token { get; set; }
    }
}
