using System;
using System.Collections.Generic;
using System.IO;

namespace ProyectoFinalLenguajes
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ingrese la ruta del archivo de texto: ");
            string archivo = Console.ReadLine();
            archivo = archivo.Replace("\"", "");

            if (!File.Exists(archivo))
            {
                Console.WriteLine("El archivo no existe. Verifique la ruta e inténtelo de nuevo.");
                return;
            }

            // Definición de simbología de la máquina de Turing
            Console.WriteLine("Simbología de la Máquina de Turing:");
            Console.WriteLine("M = (Q, Σ, Γ, δ, q0, B, F)");

            // Procesar el archivo con las cadenas en ambos sentidos
            using (var reader = new StreamReader(archivo))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine($"\nProcesando la cadena: {line}");
                    // Procesar la cadena en ambos sentidos
                    ProcesarCadena(line, true);  // Izquierda a derecha
                    ProcesarCadena(line, false); // Derecha a izquierda
                }
            }
        }

        // Método para procesar una cadena en un sentido específico
        static void ProcesarCadena(string cadena, bool izquierdaADerecha)
        {
            // Estructuras para almacenar estados y caracteres válidos
            var stackCaracteresValidos = new Stack<char>();
            var stackEstadosValidos = new Stack<string>();
            var stackEpsilon = new Stack<char>();

            string sentido = izquierdaADerecha ? "Izquierda a Derecha" : "Derecha a Izquierda";
            Console.WriteLine($"\nLectura en sentido: {sentido}");
            string estadoActual = "q0";
            bool formatoCorrecto = true;

            // Definir el árbol de derivación
            var root = new TreeNode(estadoActual);

            // Proceso de validación de la cadena en el grafo especificado
            for (int i = (izquierdaADerecha ? 0 : cadena.Length - 1);
                 (izquierdaADerecha ? i < cadena.Length : i >= 0);
                 i += (izquierdaADerecha ? 1 : -1))
            {
                char letra = cadena[i];
                string estadoSiguiente = estadoActual;

                if (!formatoCorrecto)
                    break;

                switch (estadoActual)
                {
                    case "q0":
                        if (letra == 'a')
                            estadoSiguiente = "q1";
                        else
                            estadoSiguiente = "q0";
                        break;

                    case "q1":
                        if (letra == 'b')
                            estadoSiguiente = "q2";
                        else
                            estadoSiguiente = "q1";
                        break;

                    case "q2":
                        if (letra == 'a')
                            estadoSiguiente = "q3";
                        else
                        {
                            estadoSiguiente = "ERROR";
                            formatoCorrecto = false;
                        }
                        break;

                    case "q3":
                        if (letra == 'a')
                            estadoSiguiente = "q3";
                        else if (letra == '*')
                            estadoSiguiente = "q4";
                        else if (letra == 'b')
                            estadoSiguiente = "q1";
                        else
                        {
                            estadoSiguiente = "ERROR";
                            formatoCorrecto = false;
                        }
                        break;

                    case "q4":
                        if (letra == '#')
                            estadoSiguiente = "q4";
                        else if (letra == 'a')
                            estadoSiguiente = "q3";
                        else if (letra == 'b')
                            estadoSiguiente = "q1";
                        else
                        {
                            estadoSiguiente = "ERROR";
                            formatoCorrecto = false;
                        }
                        break;
                }

                // Crear la tabla de transición
                if (estadoSiguiente == "ERROR")
                {
                    CrearTabla(estadoActual, letra, "ERROR");
                }
                else
                {
                    CrearTabla(estadoActual, letra, estadoSiguiente);
                    var nodoActual = new TreeNode(estadoSiguiente, letra.ToString());
                    root.Children.Add(nodoActual);

                    // Guardar los caracteres y estados válidos en los stacks
                    stackCaracteresValidos.Push(letra);
                    stackEstadosValidos.Push(estadoSiguiente);

                    if (letra == '*' || letra == '#')
                        stackEpsilon.Push(letra);
                }

                if (estadoSiguiente == "ERROR")
                {
                    formatoCorrecto = false;
                    break;
                }

                estadoActual = estadoSiguiente;
            }

            // Añadir nodo de "Error" en el árbol si el formato no es correcto
            if (!formatoCorrecto)
            {
                root.Children.Add(new TreeNode("Error", "Error"));
            }

            Console.WriteLine("\nÁrbol de Transiciones:");
            ImprimirArbol(root, "", true);

            if (!formatoCorrecto)
            {
                Console.WriteLine("Cadena no aceptada.");
            }
            else
            {
                Console.WriteLine("Cadena aceptada.");
            }
        }

        // Método para crear la tabla de transición
        static void CrearTabla(string estadoActual, char letra, string estadoSiguiente)
        {
            Console.WriteLine($"| {estadoActual} | {letra} | {estadoSiguiente} |");
        }

        // Método para imprimir el árbol de derivación
        static void ImprimirArbol(TreeNode nodo, string indent, bool last)
        {
            Console.Write(indent);
            Console.Write(last ? "└── " : "├── ");
            Console.WriteLine($"{nodo.State} ({nodo.Label})");

            indent += last ? "    " : "│   ";
            for (int i = 0; i < nodo.Children.Count; i++)
            {
                ImprimirArbol(nodo.Children[i], indent, i == nodo.Children.Count - 1);
            }
        }
    }

    // Clase de Nodo para el árbol de derivación
    public class TreeNode
    {
        public string State { get; set; }
        public string Label { get; set; }
        public List<TreeNode> Children { get; set; } = new List<TreeNode>();

        public TreeNode(string state, string label = "")
        {
            State = state;
            Label = label;
        }
    }
}
