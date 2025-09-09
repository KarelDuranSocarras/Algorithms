using Algorithms;

namespace Algorithms.Tests;

[TestFixture]
public class GeneticAlgorithmUnitTests
{
    private GeneticAlgorithm<int> ga;

    private int GeneGenerator() => new Random().Next(2);

    private double FitnessFunction(Individual<int> individual)
    {
        int sum = 0;
        foreach (var gene in individual.Genes)
            sum += gene;
        return sum; // fitness is sum of genes (higher is better)
    }

    private int MutateFunc(int gene)
    {
        return gene == 0 ? 1 : 0; // flip 0 to 1 or 1 to 0
    }

    [SetUp]
    public void Setup()
    {
        ga = new GeneticAlgorithm<int>(populationSize: 10,
                                        generateRandomGene: GeneGenerator,
                                        fitnessFunction: FitnessFunction,
                                        mutateGeneFunction: MutateFunc);
        ga.MutationProbability = 0.5; // high prob for testing mutation
        ga.InitializePopulation(geneCount: 5);
    }

    [Test]
    public void PopulationInitialization_CreatesCorrectSize()
    {
        Assert.AreEqual(10, ga.Population.Count);
        foreach (var individual in ga.Population)
        {
            Assert.AreEqual(5, individual.Genes.Count);
        }
    }

    [Test]
    public void FitnessCalculation_IsConsistent()
    {
        foreach (var individual in ga.Population)
        {
            double expectedFitness = 0;
            foreach (var gene in individual.Genes)
                expectedFitness += gene;
            Assert.AreEqual(expectedFitness, individual.Fitness);
        }
    }

    [Test]
    public void TournamentSelection_ReturnsAnIndividualFromPopulation()
    {
        var selected = ga.GetType()
                            .GetMethod("TournamentSelection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                            .Invoke(ga, new object[] { 3 }) as Individual<int>;
        Assert.IsNotNull(selected);
        Assert.AreEqual(5, selected.Genes.Count);
    }

    [Test]
    public void Crossover_CreatesTwoChildrenWithProperGeneLength()
    {
        var parent1 = ga.Population[0];
        var parent2 = ga.Population[1];

        var children = ga.GetType()
                            .GetMethod("Crossover", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                            .Invoke(ga, new object[] { parent1, parent2 }) as List<Individual<int>>;

        Assert.IsNotNull(children);
        Assert.AreEqual(2, children.Count);
        Assert.AreEqual(5, children[0].Genes.Count);
        Assert.AreEqual(5, children[1].Genes.Count);
    }

    [Test]
    public void Mutation_AltersGenesBasedOnProbability()
    {
        var testInd = ga.Population[0].Clone();
        ga.GetType()
            .GetMethod("Mutate", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .Invoke(ga, new object[] { testInd });

        // Since mutation probability is 0.5, expect some changes
        bool changed = false;
        for (int i = 0; i < testInd.Genes.Count; i++)
        {
            if (testInd.Genes[i] != ga.Population[0].Genes[i])
            {
                changed = true;
                break;
            }
        }
        Assert.IsTrue(changed);
    }

    [Test]
    public void RunGeneration_EvolvesPopulation()
    {
        // Store best fitness before generation
        double bestFitnessBefore = ga.GetBestIndividual().Fitness;
        System.Console.WriteLine(bestFitnessBefore);

        ga.RunGeneration();

        double bestFitnessAfter = ga.GetBestIndividual().Fitness;
        System.Console.WriteLine(bestFitnessAfter);

        // Best fitness should be equal or better after one generation
        Assert.GreaterOrEqual(bestFitnessAfter, bestFitnessBefore);
        Assert.AreEqual(10, ga.Population.Count);
    }
}