using AplicacionArbol9B.Business;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Web.Http.Cors;
using AplicacionArbol9B.DataAccess;

namespace AplicacionArbol9B.Presentation
{
    [EnableCors(origins:"*",headers:"*",methods:"*")]
    public class ArbolController : ApiController
    {
        private readonly Servicios arbolservicios;
        public ArbolController() 
        { arbolservicios = new Servicios(); }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/arbol/insertar")]

        public IHttpActionResult Insertar([FromBody] int numero)
        {
            try
            {
                arbolservicios.Insertar(numero);
                return Ok("Nodo insertado y balanceado.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/arbol/preorden")]

        public IHttpActionResult PreOrden()
        {
            try
            {
                string preorden = arbolservicios.PreOrden();
                string arbolJson = ConvertirAJson(preorden);
                return Ok(arbolJson);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/arbol/orden")]

        public IHttpActionResult Orden()
        {
            try
            {
                string orden = arbolservicios.Orden();
                string arbolJson = ConvertirAJson(orden);
                return Ok(arbolJson);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/arbol/posorden")]

        public IHttpActionResult Posorden()
        {
            try
            {
                string posorden = arbolservicios.Posorden();
                string arbolJson = ConvertirAJson(posorden);
                return Ok(arbolJson);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/arbol/porniveles")]
        public IHttpActionResult PorNiveles()
        {
            try
            {
                string porNiveles = arbolservicios.PorNiveles();
                string arbolJson = ConvertirAJson(porNiveles);
                return Ok(arbolJson);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("api/arbol/eliminar/{valor}")]
        public IHttpActionResult Borrar( int valor)
        {
            try
            {
                Operaciones.Root = Operaciones.Borrar(Operaciones.Root, valor);
                return Ok("Se ha eliminado el Nodo.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/arbol/buscar/{valor}")]
        public IHttpActionResult Buscar( int valor)
        {
            try
            {
                var nodoEncontrado = arbolservicios.Buscar(valor);
                if (nodoEncontrado != null)
                {
                    return Ok(nodoEncontrado);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/arbol/busmini")]
        public IHttpActionResult BuscarMini()
        {
            Servicios servicios = new Servicios();
            string mensaje = servicios.BuscarMini();

            return Ok(mensaje);
        }


        private string ConvertirAJson(string recorrido)
        {
            if (string.IsNullOrEmpty(recorrido))
            {
                return "{}"; // En caso de que el recorrido esté vacío o sea nulo, retornamos un objeto JSON vacío
            }

            string[] nodos = recorrido.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<int, Dictionary<string, object>> arbol = new Dictionary<int, Dictionary<string, object>>();
            Dictionary<int, int[]> relaciones = new Dictionary<int, int[]>();

            for (int i = 0; i < nodos.Length; i++)
            {
                int valor = int.Parse(nodos[i]);
                arbol[valor] = new Dictionary<string, object>
        {
            { "valor", valor },
            { "izquierda", null },
            { "derecha", null }
        };

                int posicion = i + 1;
                int izquierda = 2 * posicion;
                int derecha = 2 * posicion + 1;

                relaciones[valor] = new int[] { izquierda - 1, derecha - 1 };
            }

            foreach (var kvp in relaciones)
            {
                int valor = kvp.Key;
                int[] hijos = kvp.Value;

                if (hijos[0] < nodos.Length)
                {
                    arbol[valor]["izquierda"] = arbol[int.Parse(nodos[hijos[0]])];
                }

                if (hijos[1] < nodos.Length)
                {
                    arbol[valor]["derecha"] = arbol[int.Parse(nodos[hijos[1]])];
                }
            }

            // Convertimos el árbol completo (arbol) a JSON utilizando Newtonsoft.Json
            string json = JsonConvert.SerializeObject(arbol[int.Parse(nodos[0])]);
            return json;
        }

        public class InsertarEnNodoRequest
        {
            public int Numero { get; set; }
            public int ValorPadre { get; set; }
            public bool ALlaIzquierda { get; set; }
        }

    }
}
