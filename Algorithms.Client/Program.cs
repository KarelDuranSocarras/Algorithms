using System;
using System.Collections.Generic;
using Algorithms; 

class Program
{
    static void Main()
    {
        string targetWord = "The fittest survive";
        string alfabeto = " .AB@CDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        int geneCount = targetWord.Length;
        int populationSize = 100;
        double mutationProbability = 0.1;

        Random rnd = new Random();

        Func<char> generateRandomGene = () =>
        {
            return alfabeto[rnd.Next(alfabeto.Length)];
        };

        Func<char, char> mutateGene = currentGene =>
        {
            char newGene;
            do
            {
                newGene = alfabeto[rnd.Next(alfabeto.Length)];
            } while (newGene == currentGene);
            return newGene;
        };

        // Función de fitness: cuenta cuántos caracteres coinciden con la palabra objetivo
        Func<Individual<char>, double> fitnessFunction = individual =>
        {
            int score = 0;
            for (int i = 0; i < individual.Genes.Count; i++)
            {
                if (individual.Genes[i] == targetWord[i])
                    score++;
            }
            return score;
        };

        // Crear instancia del algoritmo genético
        var ga = new GeneticAlgorithm<char>(populationSize, generateRandomGene, fitnessFunction, mutateGene);
        ga.MutationProbability = mutationProbability;

        // Inicializar población
        ga.InitializePopulation(geneCount);

        int generation = 0;
        Individual<char> best;

        // Ejecutar el algoritmo hasta encontrar la palabra o alcanzar 1000 generaciones
        do
        {
            best = ga.GetBestIndividual();
            string bestString = new string(best.Genes.ToArray());
            Console.WriteLine($"Generación {generation}, Mejor solución: {bestString} Fitness: {best.Fitness}");
            //Console.ReadLine();
            if (best.Fitness == geneCount)
                break;

            ga.RunGeneration();
            generation++;

        } while (generation < 1000);

        Console.WriteLine($"Resultado final en generación {generation}: {new string(best.Genes.ToArray())}");
    }
}
