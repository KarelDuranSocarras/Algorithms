# Algorithms.Client: Ejemplo de Aplicación de Algoritmo Genético

Este subproyecto es una aplicación de consola de ejemplo que demuestra cómo utilizar la biblioteca `Algorithms` (el algoritmo genético genérico) para resolver un problema concreto. En este caso, el objetivo es **encontrar una palabra o frase objetivo** mediante la evolución genética.

## Problema Resuelto

El algoritmo genético intenta recrear la frase `"The fittest survive"`.

## Configuración y Parámetros Clave

La aplicación está configurada con los siguientes parámetros:

*   **`targetWord`**: La palabra o frase que el algoritmo intenta generar ("The fittest survive").
*   **`alfabeto`**: El conjunto de caracteres posibles que pueden formar los genes (letras, espacios, números y símbolos como '@').
*   **`geneCount`**: El número de genes por individuo, que es igual a la longitud de la palabra objetivo.
*   **`populationSize`**: El número de individuos en cada generación (establecido en 100 en el ejemplo).
*   **`mutationProbability`**: La probabilidad de que un gen cambie aleatoriamente (establecida en 0.1 en el ejemplo).
*   **`generateRandomGene`**: Una función que devuelve un carácter aleatorio del `alfabeto`.
*   **`mutateGene`**: Una función que muta un gen dado, cambiándolo por otro carácter aleatorio del `alfabeto` que no sea el original.
*   **`fitnessFunction`**: Evalúa la aptitud de un individuo contando cuántos de sus genes coinciden con los caracteres correspondientes en la `targetWord`.

## Cómo Ejecutar

Para ejecutar el cliente, compile y ejecute el archivo `Program.cs` en este subproyecto.

La aplicación simulará generaciones, mostrando en la consola el progreso:
*   El número de generación actual.
*   La mejor solución encontrada hasta el momento.
*   El valor de aptitud (fitness) de esa mejor solución.

El algoritmo se detendrá cuando la mejor solución coincida perfectamente con la palabra objetivo (es decir, el `fitness` sea igual a `geneCount`) o después de 1000 generaciones.
