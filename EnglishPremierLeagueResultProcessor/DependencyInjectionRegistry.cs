using Autofac;

namespace EnglishPremierLeagueResultProcessor
{
    public class DependencyInjectionRegistry
    {
        public static IContainer Register()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConfigReader>().As<IConfigReader>();

            builder.RegisterType<CsvFileReader>().As<IFileReader>();
            builder.RegisterType<LeagueResultParser>().As<ILeagueResultParser>();
            builder.RegisterType<LeagueResultProcessor>().As<ILeagueResultProcessor>();
            builder.RegisterType<FileValidator>().As<IFileValidator>();
            
            var container  = builder.Build();
            return container;
        }
    }
}
