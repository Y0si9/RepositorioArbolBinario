using AplicacionArbol9B.DataAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionArbol9B.Business
{
    public class Servicios
    {
        public void Insertar(int numero)
        {
            Nodo nuevo = new Nodo(numero);
            Operaciones.Root = Operaciones.InsertadoRecursivo(Operaciones.Root, nuevo);
            Operaciones.Balanceo(nuevo);
        }

        public string PreOrden()
        {
            return Operaciones.Preorden(Operaciones.Root).Trim();  
        }

        public string Orden()
        {
            return Operaciones.Orden(Operaciones.Root).Trim();
        }

        public string Posorden()
        {
            return Operaciones.Posorden(Operaciones.Root).Trim();
        }

        public string PorNiveles()
        {
            return Operaciones.PorNiveles(Operaciones.Root).Trim();
        }

        //public void InsertarEnNodo(int numero, int valorPadre, bool aLaIzquierda)
        //{
        //    Nodo nuevo = new Nodo(numero);
        //    Operaciones.InsertarEnNodo(Operaciones.Root, nuevo, valorPadre, aLaIzquierda);
        //    Operaciones.Balanceo(nuevo);
        //}

        public void Borrar(int num)
        {
            Operaciones.Root=Operaciones.Borrar(Operaciones.Root, num);
        }

        public Nodo Buscar(int valor)
        {
            return Operaciones.Buscar(Operaciones.Root, valor);
        }



    }
}