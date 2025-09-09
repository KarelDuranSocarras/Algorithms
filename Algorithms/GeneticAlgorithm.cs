namespace Algorithms;

    /// <summary>
    /// Generic genetic algorithm implementation.
    /// </summary>
    /// <typeparam name="T">Type of gene.</typeparam>
    public class GeneticAlgorithm<T>
    {
        private readonly Func<T> generateRandomGene;
        private readonly Func<Individual<T>, double> fitnessFunction;
        private readonly Func<T, T> mutateGeneFunction;

        /// <summary>
        /// Population size.
        /// </summary>
        public int PopulationSize { get; private set; }

        /// <summary>
        /// Probability of mutation per gene.
        /// </summary>
        public double MutationProbability { get; set; }

        /// <summary>
        /// Current population of individuals.
        /// </summary>
        public List<Individual<T>> Population { get; private set; }

        private Random random;

        /// <summary>
        /// Genetic algorithm constructor.
        /// </summary>
        /// <param name="populationSize">Number of individuals in the population.</param>
        /// <param name="generateRandomGene">Function to generate a random gene.</param>
        /// <param name="fitnessFunction">Function to evaluate individual's fitness.</param>
        /// <param name="mutateGeneFunction">Function to mutate a gene.</param>
        public GeneticAlgorithm(int populationSize,
                                Func<T> generateRandomGene,
                                Func<Individual<T>, double> fitnessFunction,
                                Func<T, T> mutateGeneFunction)
        {
            PopulationSize = populationSize;
            this.generateRandomGene = generateRandomGene;
            this.fitnessFunction = fitnessFunction;
            this.mutateGeneFunction = mutateGeneFunction;
            MutationProbability = 0.01;
            random = new Random();
            Population = new List<Individual<T>>(populationSize);
        }

        /// <summary>
        /// Initializes the population with random individuals.
        /// </summary>
        /// <param name="geneCount">Number of genes per individual.</param>
        public void InitializePopulation(int geneCount)
        {
            Population.Clear();
            for (int i = 0; i < PopulationSize; i++)
            {
                var genes = new List<T>();
                for (int g = 0; g < geneCount; g++)
                {
                    genes.Add(generateRandomGene());
                }
                var individual = new Individual<T>(genes);
                individual.Fitness = fitnessFunction(individual);
                Population.Add(individual);
            }
        }

        /// <summary>
        /// Performs tournament selection.
        /// </summary>
        /// <param name="tournamentSize">Number of individuals in the tournament.</param>
        /// <returns>The winning individual of the tournament.</returns>
        private Individual<T> TournamentSelection(int tournamentSize = 3)
        {
            var candidates = new List<Individual<T>>();
            for (int i = 0; i < tournamentSize; i++)
            {
                candidates.Add(Population[random.Next(PopulationSize)]);
            }
            return candidates.OrderByDescending(ind => ind.Fitness).First().Clone();
        }

        /// <summary>
        /// Performs one-point crossover between two parents to generate two children.
        /// </summary>
        /// <param name="parent1">First parent.</param>
        /// <param name="parent2">Second parent.</param>
        /// <returns>List containing two child individuals.</returns>
        private List<Individual<T>> Crossover(Individual<T> parent1, Individual<T> parent2)
        {
            int geneCount = parent1.Genes.Count;
            int crossoverPoint = random.Next(1, geneCount - 1);

            var child1 = new Individual<T>(geneCount);
            var child2 = new Individual<T>(geneCount);

            for (int i = 0; i < geneCount; i++)
            {
                if (i < crossoverPoint)
                {
                    child1.Genes[i] = parent1.Genes[i];
                    child2.Genes[i] = parent2.Genes[i];
                }
                else
                {
                    child1.Genes[i] = parent2.Genes[i];
                    child2.Genes[i] = parent1.Genes[i];
                }
            }

            return new List<Individual<T>> { child1, child2 };
        }

        /// <summary>
        /// Applies mutation to an individual based on mutation probability.
        /// </summary>
        /// <param name="individual">Individual to mutate.</param>
        private void Mutate(Individual<T> individual)
        {
            for (int i = 0; i < individual.Genes.Count; i++)
            {
                if (random.NextDouble() < MutationProbability)
                {
                    individual.Genes[i] = mutateGeneFunction(individual.Genes[i]);
                }
            }
        }

        /// <summary>
        /// Executes one full generation of the genetic algorithm.
        /// </summary>
        public void RunGeneration()
        {
            var newPopulation = new List<Individual<T>>();

            // Elitism: retain the best individual
            var bestIndividual = Population.OrderByDescending(ind => ind.Fitness).First().Clone();
            newPopulation.Add(bestIndividual);

            while (newPopulation.Count < PopulationSize)
            {
                // Selection
                var parent1 = TournamentSelection();
                var parent2 = TournamentSelection();

                // Crossover
                var children = Crossover(parent1, parent2);

                // Mutation
                Mutate(children[0]);
                Mutate(children[1]);

                // Calculate fitness for children
                children[0].Fitness = fitnessFunction(children[0]);
                if (newPopulation.Count < PopulationSize)
                    newPopulation.Add(children[0]);

                children[1].Fitness = fitnessFunction(children[1]);
                if (newPopulation.Count < PopulationSize)
                    newPopulation.Add(children[1]);
            }

            Population = newPopulation;
        }

        /// <summary>
        /// Gets the best individual in the current population.
        /// </summary>
        /// <returns>The individual with best fitness.</returns>
        public Individual<T> GetBestIndividual()
        {
            return Population.OrderByDescending(ind => ind.Fitness).First().Clone();
        }
    }