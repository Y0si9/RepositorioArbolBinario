using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionArbol9B.DataAccess
{
    public class Nodo
    {
        public int Numero { set; get; }
        public int FacEq { set; get; }
        //REFERENCIA
        public Nodo Padre { set; get; }
        [JsonIgnore]
        public Nodo Izquierda { get; set; }

        [JsonIgnore]
        public Nodo Derecha { get; set; }

        public Nodo(int num)
        {
            Numero = num;
            Izquierda = null;
            Derecha = null;
            Padre = null;
        }
    }
}