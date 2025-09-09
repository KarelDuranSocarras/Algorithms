namespace Algorithms;

    /// <summary>
    /// Represents an individual in the population with a generic set of genes.
    /// </summary>
    /// <typeparam name="T">Type of the gene (e.g., double, int, bool)</typeparam>
    public class Individual<T>
    {
        /// <summary>
        /// The individual's genes.
        /// </summary>
        public List<T> Genes { get; private set; }

        /// <summary>
        /// Fitness value of the individual.
        /// </summary>
        public double Fitness { get; set; }

        /// <summary>
        /// Creates an individual with the given list of genes.
        /// </summary>
        /// <param name="genes">Initial list of genes.</param>
        public Individual(List<T> genes)
        {
            Genes = new List<T>(genes);
        }

        /// <summary>
        /// Creates an empty individual with a specified gene count.
        /// </summary>
        /// <param name="geneCount">Number of genes.</param>
        public Individual(int geneCount)
        {
            Genes = new List<T>(new T[geneCount]);
        }

        /// <summary>
        /// Creates a deep copy of the individual.
        /// </summary>
        /// <returns>New Individual object with copied genes.</returns>
        public Individual<T> Clone()
        {
            var clone = new Individual<T>(new List<T>(Genes));
            clone.Fitness = this.Fitness;
            return clone;
        }
    }