using GeneticSharp.Domain.Chromosomes;
using System;

namespace MutantPizza
{
    class MyProblemChromosome : BinaryChromosomeBase
    {
        // TODO: Change the argument value passed to base construtor to change the length 
        // of your chromosome.
        Random random;

       public static int N;

        public MyProblemChromosome() 
            : base(N)
        {
            random = new Random();
            CreateGenes();
        }

        public override Gene GenerateGene(int geneIndex)
        {
            int randomBit = random.Next(0, 2);
            return new Gene(randomBit);
        }

        public override IChromosome CreateNew()
        {
            return new MyProblemChromosome();
        }
    }
}
