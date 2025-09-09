# Algorithms: Implementación del Algoritmo Genético

Este subproyecto encapsula la implementación central y genérica de un algoritmo genético. Está diseñado para ser una biblioteca reutilizable que otros proyectos pueden consumir para aplicar la optimización evolutiva a una variedad de problemas.

## Componentes Principales

*   **`GeneticAlgorithm<T>`**: La clase principal que implementa la lógica del algoritmo genético. Es genérica, lo que permite trabajar con diferentes tipos de genes (`T`).
    *   **Constructor**: Inicializa el algoritmo con el tamaño de la población, funciones para generar genes aleatorios, evaluar la aptitud (fitness) y mutar genes.
    *   **Propiedades Clave**:
        *   `PopulationSize`: Define el número de individuos en cada generación.
        *   `MutationProbability`: La probabilidad de que un gen individual mute.
        *   `Population`: La lista actual de individuos en la población.
    *   **Métodos Clave**:
        *   `InitializePopulation(int geneCount)`: Crea una población inicial de individuos con genes aleatorios y calcula su aptitud.
        *   `TournamentSelection(int tournamentSize = 3)`: Selecciona un individuo de la población mediante un torneo, donde los candidatos compiten por su aptitud.
        *   `Crossover(Individual parent1, Individual parent2)`: Realiza un cruce de un punto entre dos padres para generar dos individuos hijos.
        *   `Mutate(Individual individual)`: Aplica mutaciones a los genes de un individuo basándose en la probabilidad de mutación.
        *   `RunGeneration()`: Ejecuta una generación completa del algoritmo genético, incluyendo selección, cruce, mutación y evaluación de aptitud. Implementa elitismo, reteniendo al mejor individuo de la generación anterior.
        *   `GetBestIndividual()`: Devuelve el individuo con la mayor aptitud de la población actual.

*   **`Individual<T>`**: Representa a un individuo dentro de la población del algoritmo genético.
    *   **Propiedades Clave**:
        *   `Genes`: Una lista de genes que componen al individuo.
        *   `Fitness`: El valor de aptitud (fitness) del individuo, que indica qué tan buena es su solución.
    *   **Constructor**: Puede crearse con una lista de genes preexistente o con un número especificado de genes vacíos.
    *   **Método `Clone()`**: Crea una copia profunda del individuo, incluyendo sus genes y su valor de aptitud.

## Uso

Este subproyecto es una biblioteca y no es directamente ejecutable. Debe ser referenciado por otros proyectos (como `Algorithms.Client` o `Algorithms.Tests`) para utilizar sus funcionalidades.
