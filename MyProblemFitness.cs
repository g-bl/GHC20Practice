using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;
using System;
using System.Collections.Generic;

namespace MutantPizza
{
    class MyProblemFitness : IFitness
    {
        public int M { get; set; }
        public List<int> PizzaParts { get; set; }

        public double Evaluate(IChromosome chromosome)
        {
            double score = 0;
            for (int i =0; i<chromosome.Length; i++)
            {
                if ((int)chromosome.GetGene (i).Value == 1)
                {
                    score += PizzaParts[i];
                    if (score > M)
                        return 0f;
                }
            }
            return score;
        }
    }
}
