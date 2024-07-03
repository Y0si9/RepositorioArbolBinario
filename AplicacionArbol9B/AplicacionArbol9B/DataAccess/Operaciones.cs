using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionArbol9B.DataAccess
{
    public static class Operaciones
    {
        public static Nodo Root { get; set; }

        public static Nodo InsertadoRecursivo(Nodo padre, Nodo nuevo)
        {
            if (padre == null)
                padre = nuevo;
            else if (nuevo.Numero < padre.Numero)
                padre.Izquierda = InsertadoRecursivo(padre.Izquierda, nuevo);
            else
                padre.Derecha = InsertadoRecursivo(padre.Derecha, nuevo);
            return padre;
        }

        public static string Preorden(Nodo lista)
        {
            if (lista != null)
            {
                string cad = lista.Numero.ToString() + " ";
                cad += Preorden(lista.Izquierda) + " ";
                cad += Preorden(lista.Derecha) + " ";
                return cad;
            }
            return "";
        }

        public static string Orden(Nodo lista)
        {
            if (lista != null)
            {
                string cad = Orden(lista.Izquierda) + " ";
                cad += lista.Numero.ToString() + " ";
                cad += Orden(lista.Derecha) + " ";
                return cad;
            }
            return "";
        }

        public static string Posorden(Nodo lista)
        {
            if (lista != null)
            {
                string cad = Posorden(lista.Izquierda) + " ";
                cad += Posorden(lista.Derecha) + " ";
                cad += lista.Numero.ToString() + " ";
                return cad;
            }
            return "";
        }
        public static string PorNiveles(Nodo root)
        {
            if (root == null)
                return string.Empty;

            Queue<Nodo> cola = new Queue<Nodo>();
            cola.Enqueue(root);
            List<int> resultado = new List<int>();

            while (cola.Count > 0)
            {
                Nodo actual = cola.Dequeue();
                resultado.Add(actual.Numero);

                if (actual.Izquierda != null)
                    cola.Enqueue(actual.Izquierda);
                if (actual.Derecha != null)
                    cola.Enqueue(actual.Derecha);
            }

            return string.Join(" ", resultado);
        }

        //public static bool InsertarEnNodo(Nodo padre, Nodo nuevo, int valorPadre, bool aLaIzquierda)
        //{
        //    if (padre == null)
        //        return false;

        //    if (padre.Numero == valorPadre)
        //    {
        //        if (aLaIzquierda)
        //        {
        //            nuevo.Izquierda = padre.Izquierda;
        //            padre.Izquierda = nuevo;
        //        }
        //        else
        //        {
        //            nuevo.Derecha = padre.Derecha;
        //            padre.Derecha = nuevo;
        //        }
        //        nuevo.Padre = padre;
        //        return true;
        //    }

        //    if (InsertarEnNodo(padre.Izquierda, nuevo, valorPadre, aLaIzquierda) ||
        //        InsertarEnNodo(padre.Derecha, nuevo, valorPadre, aLaIzquierda))
        //    {
        //        return true;
        //    }

        //    return false;
        //}


        public static int Min(Nodo nodo)
        {
            if (nodo == null)
            {
                return 0;
            }
            Nodo tmp = nodo;
            int mini = tmp.Numero;
            while (tmp.Izquierda != null)
            {
                tmp = tmp.Izquierda;
                mini = tmp.Numero;
            }

            return mini;
        }

        public static Nodo Borrar(Nodo nodo, int numero)
        {
            if (nodo == null)
            {
                return nodo;
            }

            if (numero < nodo.Numero)
            {
                nodo.Izquierda = Borrar(nodo.Izquierda, numero);
            }
            else if (numero > nodo.Numero)
            {
                nodo.Derecha = Borrar(nodo.Derecha, numero);
            }
            else
            {
                if (nodo.Izquierda == null)
                {
                    return nodo.Derecha;
                }
                else if (nodo.Derecha == null)
                {
                    return nodo.Izquierda;
                }

                nodo.Numero = Min(nodo.Derecha);
                nodo.Derecha = Borrar(nodo.Derecha, nodo.Numero);
            }

            return nodo;
        }


        public static int AlturaNodo(Nodo n)
        {
            if (n != null)
            {
                int hi = AlturaNodo(n.Izquierda);
                int hd = AlturaNodo(n.Derecha);

                return (hi > hd) ? hi + 1 : hd + 1;
            }
            else
            {
                return 0;
            }
        }

        public static int FactorEquilibrio(Nodo nodo)
        {
            return AlturaNodo(nodo.Derecha) - AlturaNodo(nodo.Izquierda);
        }

        public static void RotLeft(Nodo nodo)
        {
            Nodo aux = nodo.Derecha;
            Nodo q = aux.Izquierda;
            Nodo p = nodo.Padre;

            nodo.Derecha = q;
            aux.Izquierda = nodo;

            if (q != null)
            {
                q.Padre = nodo;
            }
            nodo.Padre = aux;

            if (p == null)
            {
                Root = aux;
            }
            else
            {
                aux.Padre = p;
                if (aux.Numero > p.Numero)
                {
                    p.Derecha = aux;
                }
                else
                {
                    p.Izquierda = aux;
                }
            }
            aux.FacEq = FactorEquilibrio(aux);
            nodo.FacEq = FactorEquilibrio(nodo);
        }

        public static void RotRight(Nodo nodo)
        {
            Nodo aux = nodo.Izquierda;
            Nodo q = aux.Derecha;
            Nodo p = nodo.Padre;

            nodo.Izquierda = q;
            aux.Derecha = nodo;

            if (q != null)
            {
                q.Padre = nodo;
            }
            nodo.Padre = aux;

            if (p == null)
            {
                Root = aux;
            }
            else
            {
                aux.Padre = p;
                if (aux.Numero > p.Numero)
                {
                    p.Derecha = aux;
                }
                else
                {
                    p.Izquierda = aux;
                }
            }
            aux.FacEq = FactorEquilibrio(aux);
            nodo.FacEq = FactorEquilibrio(nodo);
        }

        public static void RotDoubleLeft(Nodo n)
        {
            RotRight(n.Derecha);
            RotLeft(n);
        }

        public static void RotDoubleRight(Nodo n)
        {
            RotLeft(n.Izquierda);
            RotRight(n);
        }

        public static void Balanceo(Nodo p)
        {
            Nodo padre;
            if (p != null)
            {
                padre = p.Padre;
                p.FacEq = FactorEquilibrio(p);
                if (p.FacEq > 1)
                {
                    if (FactorEquilibrio(p.Derecha) < 0)
                    {
                        RotDoubleLeft(p);
                    }
                    else
                    {
                        RotLeft(p);
                    }
                }
                else if (p.FacEq < -1)
                {
                    if (FactorEquilibrio(p.Izquierda) > 0)
                    {
                        RotDoubleRight(p);
                    }
                    else
                    {
                        RotRight(p);
                    }
                }
                Balanceo(padre);
            }
        }

        public static int Contador = 0;

        public static void Clear()
        {
            Contador = 0;
        }

        public static Nodo Buscar(Nodo nodo, int valor)
        {
            if (nodo == null || nodo.Numero == valor)
            {
                return nodo;
            }
            if (valor < nodo.Numero)
            {
                return Buscar(nodo.Izquierda, valor);
            }
            return Buscar(nodo.Derecha, valor);
        }
        
        //public static int Bus(Nodo lista, int numbuscar)
        //{
        //    if (lista != null)
        //    {
        //        int cad = Bus(lista.Izquierda, numbuscar);
        //        cad = Bus(lista.Derecha, numbuscar);
        //        cad = lista.Numero;
        //        if (lista.Numero == numbuscar)
        //        {
        //            Contador++;
        //        }
        //        return Contador / 2;
        //    }
        //    return 0;
        //}
    }
}