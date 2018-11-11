using Autofac;
using System;

namespace EnglishPremierLeagueResultProcessor
{
    public class Program
    {
        private static IContainer Container { get; set; }

        public static void Main(string[] args)
        {
            Container = DependencyInjectionRegistry.Register();

            using (var scope = Container.BeginLifetimeScope())
            {
                var csvReader = scope.Resolve<IFileReader>();
                var leagueResultProcessor = scope.Resolve<ILeagueResultProcessor>();

                var csvOutput = csvReader.ReadFileData();
                var winningTeam = leagueResultProcessor.FindWinningTeam(csvOutput);

                if(winningTeam != null)
                    Console.WriteLine("Team with minimum diff :" + winningTeam.TeamName);
                else
                    Console.WriteLine("Could not find Team with minimum diff.");

            }

            Console.Read();
        }
    }
}
