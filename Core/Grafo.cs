﻿using System;

namespace Core
{
    public class Grafo<T>
    {
        private bool Dirigido = false;
        private Arista<T>[,] MatrizArcos;
        private TablaHash<String, Vertice<T>>  ArregloDVertices;

        public Grafo()
        {
            ArregloDVertices = new TablaHash<String, Vertice<T>>(40);
            MatrizArcos = new Arista<T>[TablaHash<String, Vertice<T>>.ObtenerPrimo(40), TablaHash<String, Vertice<T>>.ObtenerPrimo(40)];
        }
        public Grafo(bool pDirigido)
        {
            this.Dirigido = pDirigido;
            ArregloDVertices = new TablaHash<String, Vertice<T>>(40);
            MatrizArcos = new Arista<T>[TablaHash<String, Vertice<T>>.ObtenerPrimo(40), TablaHash<String, Vertice<T>>.ObtenerPrimo(40)];
        }
        public  bool InsertaVertice(string pNombre, T pInformacion)
        {
            return  ArregloDVertices.Insertar(pNombre, new Vertice<T>(pNombre,pInformacion));
        }
        public bool InsertarArco(string pVertice1, string pVertice2, double pPeso)
        {
            if (!(ExisteArco(pVertice1, pVertice2)))
            {
                CrearRelacion(pVertice1, pVertice2, pPeso);
                return true;
            }
            return false;
        }
        public bool GetDirigido()
        {
            return this.Dirigido;
        }
        public void SetDirigido(bool pDirigido)
        {
            this.Dirigido = pDirigido;
        }

        private void CrearRelacion(string pVerticeA, string pVerticeB, double pPeso = 0)
        {
            int indexA, indexB;
            indexA = ArregloDVertices.GetIndex(pVerticeA);
            indexB = ArregloDVertices.GetIndex(pVerticeB);
            if (indexA >= 0 && indexB >= 0)
            {
                MatrizArcos[indexA, indexB] = new Arista<T>(ArregloDVertices.BuscaHashPorClave(pVerticeA), ArregloDVertices.BuscaHashPorClave(pVerticeB), pPeso);
                if (!Dirigido)
                {
                    MatrizArcos[indexB, indexA] = new Arista<T>(ArregloDVertices.BuscaHashPorClave(pVerticeB), ArregloDVertices.BuscaHashPorClave(pVerticeA), pPeso);
                }

            }
        }
        private bool ExisteArco(string pVertice1, string pVertice2)
        {
            bool siExiste = false;
            int index1, index2;
            index1 = ArregloDVertices.GetIndex(pVertice1);
            index2 = ArregloDVertices.GetIndex(pVertice2);
            if (index1 >= 0 && index2 >= 0)
            {
                if (MatrizArcos[index1, index2] != null)
                {
                    siExiste = true;
                }
            }
            return siExiste;
        }
        private Arista<T> GetArco(string pVertice1, string pVertice2)
        {
            int index1, index2;
            index1 = ArregloDVertices.GetIndex(pVertice1);
            index2 = ArregloDVertices.GetIndex(pVertice2);
            if (index1 >= 0 && index2 >= 0)
            {
                if (MatrizArcos[index1, index2] != null)
                {
                    return MatrizArcos[index1, index2];
                }
            }
            return null;
        }
        private bool ExisteVertice(string pVertice1, string pVertice2)
        {
            bool siExiste = false;
            int index1, index2;
            index1 = ArregloDVertices.GetIndex(pVertice1);
            index2 = ArregloDVertices.GetIndex(pVertice2);
            if (index1 >= 0 && index2 >= 0)
            {
                    siExiste = true;
            }
            return siExiste;
        }
        private Vertice<T>  GetVerticePorNombre(string pNombreVertice)
        {
            int index1 = ArregloDVertices.GetIndex(pNombreVertice);
            if (index1 >= 0)
            {
                return ArregloDVertices.GetForIndex(index1);
            }
            return null;
        }
        public Lista<T> GetUbicacion(string pNombreVertice)
        {
            return null;
        }
        public Lista<T> GetCaminoMasCerca(string pNombreVerticeA,string pNombreVerticeB)
        {
            return null;
        }
        public Lista<T> GetCaminoMasLargo(string pNombreVerticeA, string pNombreVerticeB)
        {
            return null;
        }
        public Lista<T> GetUbicacionesAdyacentes(string pNombreVertice)
        {
            return null;
        }
        public override string ToString()
        {
            return MostrarArcos();
        }
        private string MostrarArcos()
        {
            string result = "";
            for (int i = 0; i < MatrizArcos.GetLength(0); i++)
            {
                for (int j = 0; j < MatrizArcos.GetLength(1); j++)
                {
                    if (MatrizArcos[i, j] != null)
                    {
                        result += MatrizArcos[i, j].ToString(this.Dirigido);
                    }
                }
                result += "\n";
            }
            Console.WriteLine(result);
            return result;
        }
        public string MostrarPredecesores(string pVertice)
        {
            string result = "";
            int index = ArregloDVertices.GetIndex(pVertice);
            if (index >= 0)
            {
                for (int i = 0; i < MatrizArcos.GetLength(0); i++)
                {
                    if (MatrizArcos[i, index] != null)
                    {
                        result += MatrizArcos[i, index].GetVertA().Nombre;
                        result += "\n";
                    }
                }
            }
            Console.WriteLine(result);
            return result;
        }
        public string MostrarSucesores(string pVertice)
        {
            string result = "";
            int index = ArregloDVertices.GetIndex(pVertice);
            if (index >= 0)
            {
                for (int i = 0; i < MatrizArcos.GetLength(1); i++)
                {
                    if (MatrizArcos[index, i] != null)
                    {
                        result += MatrizArcos[index,i].GetVertB().Nombre;
                        result += "\n";
                    }
                }
            }
            Console.WriteLine(result);
            return result;
        }
        public Lista<Vertice<T>> GetListaSucesores(string pVertice)
        {
            int index = ArregloDVertices.GetIndex(pVertice);
            if (index >= 0)
            {
                Lista<Vertice<T>> ListaVerticesSucesores = new Lista<Vertice<T>>();
                for (int i = 0; i < MatrizArcos.GetLength(1); i++)
                {
                    if (MatrizArcos[index, i] != null)
                    {
                        ListaVerticesSucesores.InsertarAlFinal( MatrizArcos[index, i].GetVertB());
                    }
                }
                return ListaVerticesSucesores;
            }
            return null;
        }
        public Lista<Vertice<T>> GetListaSucesoresNoVisitados(string pVertice)
        {
            int index = ArregloDVertices.GetIndex(pVertice);
            if (index >= 0)
            {
                Lista<Vertice<T>> ListaVerticesSucesores = new Lista<Vertice<T>>();
                for (int i = 0; i < MatrizArcos.GetLength(1); i++)
                {
                    if (MatrizArcos[index, i] != null && !MatrizArcos[index, i].GetVertB().EsVisit)
                    {
                        ListaVerticesSucesores.InsertarAlFinal(MatrizArcos[index, i].GetVertB());
                    }
                }
                return ListaVerticesSucesores;
            }
            return null;
        }
        public Lista<Vertice<T>> GetListaPredecesores(string pVertice)
        {
            int index = ArregloDVertices.GetIndex(pVertice);
            if (index >= 0)
            {
                Lista<Vertice<T>> ListaVerticesPredecesores = new Lista<Vertice<T>>();
                for (int i = 0; i < MatrizArcos.GetLength(1); i++)
                {
                    if (MatrizArcos[index, i] != null)
                    {
                        ListaVerticesPredecesores.InsertarAlFinal(MatrizArcos[i,index].GetVertA());
                    }
                }
                return ListaVerticesPredecesores;
            }
            return null;
        }
        public Vertice<T> GetVeticePorNombre(string pNombre)
        {
            Vertice<T> vertice = ArregloDVertices.BuscaHashPorClave(pNombre);
            if (vertice == null)
            {
                return null;
            }
            return vertice;
        }
        public string MostrarMatrizDAdyacencias()
        {
            string result = "", fila = "";
            for (int i = 0; i < MatrizArcos.GetLength(0); i++)
            {
                result += ArregloDVertices.GetForIndex(i).Nombre + "|";
                fila += "\t" + ArregloDVertices.GetForIndex(i).Nombre;
                for (int j = 0; j < MatrizArcos.GetLength(1); j++)
                {
                    if (MatrizArcos[i, j] == null)
                    {
                        result += "\tNULL";
                    }
                    else
                    {
                        result += "\t" + MatrizArcos[i, j].GetPeso();
                    }
                }
                result += "\n";
            }
            fila = fila + "\n" + result;
            result = fila;
            Console.WriteLine(result);
            return result;
        }
        private bool HayPesosNegativosEnArcos()
        {
            for (int i = 0; i < MatrizArcos.GetLength(0); i++)
            {
                for (int j = 0; j < MatrizArcos.GetLength(1); j++)
                {
                    if (MatrizArcos[i, j]!= null )
                    {
                        if (MatrizArcos[i, j].GetPeso() < 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        private void LimpiaVisitasVertices()
        {
            foreach (Informacion<string, Vertice<T>> Informacion in this.ArregloDVertices.GetInformacionEntrada())
            {
                if (Informacion.GetInformacion() != null)
                {
                    Informacion.GetInformacion().EsVisit = false;
                }
            }
        }
        public Lista<Vertice<T>> RutaMinimaDijkstra(string pNombreVerticeA, string pNombreVerticeB)
        {
            if (pNombreVerticeA != pNombreVerticeB)
            {
                if (this.HayPesosNegativosEnArcos())
                {
                    return null;
                }
                TablaHash<String, int> verticesFinales = GetTablasHashParaDijkstra();
                TablaHash<String, int> verticesTemporales = GetTablasHashParaDijkstra();
                string clave = null;

                this.LimpiaVisitasVertices();
                Lista<Vertice<T>> ListaAuxiliarDSucesores, ListaSucesores = this.GetListaSucesores(pNombreVerticeA);
                Vertice<T> verticeActual = this.GetVerticePorNombre(pNombreVerticeA), verticeInicio = this.GetVerticePorNombre(pNombreVerticeA);
                verticeInicio.EsVisit = true;
                Arista<T> arco = null;
                int peso = 0, costo = 0;
                Iterador<Vertice<T>> iterador = new Iterador<Vertice<T>>(ListaSucesores.GetCabeza());
                bool noSalir = true;
                verticesTemporales.ActualizarDatoPorClave(pNombreVerticeA, 0);
                verticesFinales.ActualizarDatoPorClave(pNombreVerticeA, 0);
                while (noSalir)
                {
                    for (Vertice<T> verticeAdyac = iterador.Next(); verticeAdyac != null; verticeAdyac = iterador.Next())
                    {
                        arco = GetArco(verticeActual.Nombre, verticeAdyac.Nombre);
                        if (arco != null)
                        {
                            costo = (int)arco.GetPeso() + peso;
                            if (verticesTemporales.BuscaHashPorClave(arco.GetVertB().Nombre) > costo || verticesTemporales.BuscaHashPorClave(arco.GetVertB().Nombre) == -1)
                            {
                                verticesTemporales.ActualizarDatoPorClave(arco.GetVertB().Nombre, costo);
                            }
                        }
                    }
                    int index = -1, auxiliar = 0;
                    clave = null;
                    for (int i = 0; i < verticesTemporales.GetTamanio(); i++)
                    {
                        clave = verticesTemporales.GetClave(i);
                        if ((auxiliar == 0 || auxiliar > verticesTemporales.GetForIndex(i)) && (verticesTemporales.GetForIndex(i) >= 0 && clave != null))
                        {
                            if (!ArregloDVertices.BuscaHashPorClave(clave).EsVisit)
                            {
                                auxiliar = verticesTemporales.GetForIndex(i);
                                index = i;
                            }
                        }
                    }
                    if (index != -1)
                    {
                        clave = verticesTemporales.GetClave(index);
                        verticesFinales.ActualizarDatoPorIndex(index, auxiliar);
                        verticeActual = ArregloDVertices.BuscaHashPorClave(clave);
                        verticeActual.EsVisit = true;
                    }
                    else
                    {
                        if (DetenerAnalisis())
                        {
                            noSalir = false;
                        }
                    }
                    ListaAuxiliarDSucesores = this.GetListaSucesoresNoVisitados(clave);
                    if (ListaAuxiliarDSucesores.GetCabeza() != null)
                    {
                        ListaSucesores = ListaAuxiliarDSucesores;
                    }
                    peso = auxiliar;
                    index = -1;
                    auxiliar = 0;
                    iterador = new Iterador<Vertice<T>>(ListaSucesores.GetCabeza());
                }
                Lista<Vertice<T>> ListaCaminoCorto = GetCaminoMinimo(verticesFinales, pNombreVerticeB);
                return ListaCaminoCorto;
            }
            return null;
        }
        private TablaHash<String, int> GetTablasHashParaDijkstra()
        {
            TablaHash<String, int> tablasHas = new TablaHash<string, int>(ArregloDVertices.GetTamanio());
            string clave = null;
            for (int i = 0; i < this.ArregloDVertices.GetTamanio(); i++)
            {
                if (this.ArregloDVertices.GetForIndex(i) != null)
                {
                    clave = this.ArregloDVertices.GetForIndex(i).Nombre;
                    tablasHas.Insertar(clave, -1);                }
            }
            return tablasHas;
        }
        private Lista<Vertice<T>> GetCaminoMinimo(TablaHash<string,int> pVerticesFinales,string pVerticeDestino)
        {
            Lista<Vertice<T>> ListaCaminoCorto = new Lista<Vertice<T>>();
            ListaCaminoCorto.InsertarAlInicio(this.GetVerticePorNombre(pVerticeDestino));
            Lista<Vertice<T>>  ListaSucesores = this.GetListaSucesores(pVerticeDestino);
            Iterador<Vertice<T>> iterador = new Iterador<Vertice<T>>(ListaSucesores.GetCabeza());
            Vertice<T> verticeActual = this.GetVerticePorNombre(pVerticeDestino);
            Arista<T> arco = null;
            int pesoA, pesoB, pesoC, residuo;
            bool noSalir = true;
            while (noSalir)
            {
                for (Vertice<T> verticeAdyac = iterador.Next(); verticeAdyac != null; verticeAdyac = iterador.Next())
                {
                    arco = GetArco(verticeActual.Nombre, verticeAdyac.Nombre);
                    if (arco != null)
                    {
                        pesoA = (int)arco.GetPeso();
                        pesoB = pVerticesFinales.BuscaHashPorClave(verticeActual.Nombre);
                        pesoC = pVerticesFinales.BuscaHashPorClave(verticeAdyac.Nombre);
                        residuo = pesoB - pesoA;
                        if (residuo == pesoC || residuo == 0)
                        {
                            ListaCaminoCorto.InsertarAlInicio(arco.GetVertB());
                            verticeActual = verticeAdyac;
                            verticeAdyac = null;
                            if (residuo == 0)
                            {
                                noSalir = false;
                            }
                        }
                    }
                }
                ListaSucesores = this.GetListaSucesores(verticeActual.Nombre);
                iterador = new Iterador<Vertice<T>>(ListaSucesores.GetCabeza());
            }
           
            return ListaCaminoCorto;
        }
        private bool DetenerAnalisis()
        {
            for (int i = 0; i < ArregloDVertices.GetTamanio(); i++)
            {
                if (ArregloDVertices.GetForIndex(i) != null)
                {
                    if (!ArregloDVertices.GetForIndex(i).EsVisit)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        //public Cola<Vertice<T>> RutaMinimaDijkstra(string pNombreVerticeA, string pNombreVerticeB)
        //{
        //    Cola<Vertice<T>> ColaCaminoCorto = new Cola<Vertice<T>>();
        //    if (this.HayPesosNegativosEnArcos())
        //    {
        //        return null;
        //    }
        //    this.LimpiaVisitasVertices();
        //    Lista<Vertice<T>> ListaSucesores = this.GetListaSucesores(pNombreVerticeA);
        //    Vertice<T> verticeActual = null, verticeInicio = this.GetVerticePorNombre(pNombreVerticeA);
        //    verticeInicio.EsVisit = true;
        //    ColaCaminoCorto.EnColar(verticeInicio);
        //    Arista<T> arco = null;
        //    int peso = 0, costo = 0;
        //    Iterador<Vertice<T>> iterador = new Iterador<Vertice<T>>(ListaSucesores.GetCabeza());
        //    bool noSalir = true;
        //    while (noSalir)
        //    {
        //        for (Vertice<T> verticeAdyac = iterador.Next(); verticeAdyac != null; verticeAdyac = iterador.Next())
        //        {
        //            arco = GetArco(verticeInicio.Nombre, verticeAdyac.Nombre);
        //            if (arco != null)
        //            {
        //                if (arco.GetVertA().Nombre.Equals(pNombreVerticeB))
        //                {
        //                    noSalir = false;
        //                }
        //                else
        //                {
        //                    if ((peso == 0 || peso > arco.GetPeso() + costo) && !arco.GetVertB().EsVisit)
        //                    {
        //                        peso = (int)arco.GetPeso() + costo;
        //                        verticeActual = arco.GetVertB();
        //                        //vIni.EsVisit = true;
        //                    }
        //                }

        //            }
        //        }

        //        verticeInicio = verticeActual;
        //        verticeInicio.EsVisit = true;
        //        if (peso == 0)
        //        {
        //            ListaSucesores = this.GetListaSucesores(ColaCaminoCorto.GetInicio().GetInfo().Nombre);
        //            verticeInicio = ColaCaminoCorto.GetInicio().GetInfo();
        //        }
        //        else
        //        {
        //            ListaSucesores = this.GetListaSucesores(verticeInicio.Nombre);
        //            ColaCaminoCorto.EnColar(verticeInicio);
        //            costo = peso;
        //        }
        //        iterador = new Iterador<Vertice<T>>(ListaSucesores.GetCabeza());
        //        peso = 0;
        //    }
        //    //ColaCaminoCorto.EnColar(this.GetVerticePorNombre(pNombreVerticeB));
        //    return ColaCaminoCorto;
        //}

    }
}
