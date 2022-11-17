using Doctorla.Core.InternalDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Doctorla.Core
{
    /// <summary>
    /// Use this ONLY when dotnet core dependency injection mechanism can not be used.
    /// </summary>
    public static class ServiceLocator
    {
        private static IServiceProvider _serviceProvider;
        public static bool IsProductionEnvironment { get; private set; }
        public static AppSettings AppSettings { get; private set; }

        public static void Initialize(IServiceProvider serviceProvider, AppSettings appSettings)
        {
            _serviceProvider = serviceProvider;
            AppSettings = appSettings;
        }

        //public static Context GetContext()
        //{
        //    var builder = new DbContextOptionsBuilder<Context>();
        //    builder.UseSqlServer(_connectionString);
        //    return new Context(builder.Options);
        //}

        public static void SetProductionEnvironment()
        {
            IsProductionEnvironment = true;
        }

        public static object ResolveService(Type type)
        {
            // During migration, httpContext is null but it is not needed
            try
            {
                return _serviceProvider.GetService(type);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static T ResolveService<T>()
        {
            return _serviceProvider.GetService<T>();
        }
    }
}
