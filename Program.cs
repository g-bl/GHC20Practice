using GeneticSharp.Domain;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Terminations;
using System;
using System.Collections.Generic;
using System.IO;

namespace MutantPizza
{
    class Program
    {
        /// <summary>
        /// GeneticSharp Console Application template.
        /// <see href="https://github.com/giacomelli/GeneticSharp"/>
        /// </summary>

        static void Main(string[] args)
        {
            int M = 0;
            List<int> pizzaParts = new List<int>();

            /**************************************************************************************
             *  Input loading
             **************************************************************************************/

            //string inputFileName = "a_example.in";
            //string inputFileName = "b_small.in";
            //string inputFileName = "c_medium.in";
            //string inputFileName = "d_quite_big.in";
            string inputFileName = "e_also_big.in";

            Console.WriteLine("Input loading... " + inputFileName);

            string inputFilePath = Path.Combine(Directory.GetCurrentDirectory(), inputFileName);
            string[] lines = File.ReadAllLines(inputFilePath);

            string[] splittedLine = lines[0].Split(' ');

            M = int.Parse(splittedLine[0]);

            int N = int.Parse(splittedLine[1]);
            MyProblemChromosome.N = N;

            string[] splittedLine2 = lines[1].Split(' ');

            for (int i = 0; i < N; i++)
            {
                pizzaParts.Add(int.Parse(splittedLine2[i]));
            }

            /**************************************************************************************
             *  Solver
             **************************************************************************************/

            // TODO: use the best genetic algorithm operators to your optimization problem.
            var selection = new EliteSelection();
            var crossover = new UniformCrossover();
            var mutation = new UniformMutation(true);

            var fitness = new MyProblemFitness();
            fitness.M = M;
            fitness.PizzaParts = pizzaParts;

            var chromosome = new MyProblemChromosome();

            var population = new Population(50, 70, chromosome);

            var ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);
            ga.Termination = new FitnessStagnationTermination(100);
            ga.GenerationRan += (s, e) => Console.WriteLine($"Generation {ga.GenerationsNumber}. Best fitness: {ga.BestChromosome.Fitness.Value}");

            Console.WriteLine("GA running...");
            ga.Start();

            Console.WriteLine();
            Console.WriteLine($"Best solution found has fitness: {ga.BestChromosome.Fitness}");
            Console.WriteLine($"Elapsed time: {ga.TimeEvolving}");
            Console.ReadKey();

            /**************************************************************************************
             *  Output
             **************************************************************************************/

            Console.WriteLine("Output to file...");

            string outputFileName = Path.Combine(Directory.GetCurrentDirectory(), inputFileName.Split('.')[0] + ".out");

            using (StreamWriter outputFile = new StreamWriter(outputFileName))
            {
                List<int> indexes = new List<int>();
                for (int i = 0; i < ga.BestChromosome.Length; i++)
                {
                    if ((int)ga.BestChromosome.GetGene(i).Value == 1)
                        indexes.Add(i);
                }

                outputFile.WriteLine(indexes.Count);
                outputFile.WriteLine(String.Join(' ', indexes));
            }

            Console.WriteLine("Done.");
            Console.WriteLine(Path.Combine(Directory.GetCurrentDirectory(), outputFileName));
        }
    }
}
