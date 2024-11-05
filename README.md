Este código implementa una Máquina de Turing de dos cintas que lee cadenas de un archivo de texto y las procesa en dos sentidos: de izquierda a derecha y de derecha a izquierda. A continuación, se detalla cómo funciona el programa:

1. Entrada del Archivo de Texto
   
Al iniciar el programa, se solicita la ruta de un archivo de texto que contiene las cadenas a procesar.
Si el archivo no existe, el programa termina con un mensaje de error.

2. Definición de la Simbología de la Máquina de Turing
   
Se muestra la simbología estándar de una máquina de Turing: 
M=(Q,Σ,Γ,δ,q0,B,F), donde cada símbolo representa un componente de la máquina.

3. Lectura y Procesamiento de Cadenas
   
Se lee cada línea (cadena) del archivo y se procesa en dos direcciones:
Izquierda a derecha
Derecha a izquierda

4. Método ProcesarCadena
   
Parámetros: recibe la cadena y un indicador de dirección de lectura.
Variables Internas:
estadoActual: estado actual en el procesamiento de la cadena.
stackCaracteresValidos, stackEstadosValidos, stackEpsilon: pilas para almacenar caracteres y estados válidos.
root: nodo raíz para el árbol de derivación.
Proceso:
Se define un bucle que recorre cada caracter de la cadena en la dirección especificada.
Según el estado actual y el carácter leído, la máquina cambia de estado usando una tabla de transición.
Si la cadena tiene un error en el formato, se marca el estado ERROR.
Salida:
Si el formato de la cadena es correcto, se muestra el árbol de derivación y se acepta la cadena.
Si hay un error, el árbol mostrará el estado de error y se rechazará la cadena.

5. Método CrearTabla
   
Muestra una fila en la tabla de transición con el estado actual, el carácter leído y el siguiente estado.

6. Impresión del Árbol de Derivación
    
Método ImprimirArbol: Recursivamente imprime el árbol de derivación para visualizar las transiciones de la cadena procesada.

7. Clase TreeNode
    
Representa un nodo en el árbol de derivación.
Almacena el estado actual y el carácter de transición, así como una lista de nodos hijos para construir la estructura del árbol.
