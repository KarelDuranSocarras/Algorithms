# Algorithms.Tests: Pruebas Unitarias del Algoritmo Genético

Este subproyecto contiene las pruebas unitarias para la implementación del algoritmo genético que se encuentra en el subproyecto `Algorithms`. Las pruebas utilizan el marco NUnit [GlobalUsings.cs].

## Funcionalidades Probadas

Las pruebas cubren aspectos clave del `GeneticAlgorithm`, incluyendo:

*   **Inicialización de la Población**: Verifica que la población se crea con el tamaño correcto y que cada individuo tiene el número esperado de genes.
*   **Cálculo de Aptitud**: Asegura que el valor de aptitud de los individuos se calcula de manera consistente con la función de fitness definida.
*   **Selección por Torneo**: Confirma que el método de selección por torneo devuelve un individuo válido de la población.
*   **Cruce (Crossover)**: Prueba que el cruce de un punto genera dos individuos hijos con la longitud de genes adecuada.
*   **Mutación**: Comprueba que la función de mutación altera los genes de un individuo según la probabilidad de mutación establecida.
*   **Ejecución de Generación**: Valida que el método `RunGeneration` evoluciona la población, asegurando que el mejor fitness de la nueva población es igual o superior al de la generación anterior y que el tamaño de la población se mantiene constante.

## Configuración de Pruebas (`SetUp`)

Antes de cada prueba, se inicializa una instancia de `GeneticAlgorithm` con los siguientes parámetros específicos para las pruebas:
*   `populationSize`: 10
*   `generateRandomGene`: Genera genes binarios (0 o 1).
*   `fitnessFunction`: Calcula la suma de los genes (a mayor suma, mejor aptitud).
*   `mutateGeneFunction`: Invierte el valor del gen (0 a 1, o 1 a 0).
*   `MutationProbability`: 0.5 (una alta probabilidad para facilitar la observación de mutaciones en las pruebas).
*   La población se inicializa con 5 genes por individuo.

## Cómo Ejecutar las Pruebas

Para ejecutar las pruebas, se puede utilizar un corredor de pruebas compatible con NUnit (por ejemplo, el Test Explorer en Visual Studio o el comando `dotnet test` desde la línea de comandos en el directorio del subproyecto `Algorithms.Tests`).
