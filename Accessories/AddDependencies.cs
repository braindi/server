using Microsoft.Extensions.DependencyInjection;
using BL;
using DAL;
namespace Accessories
{

    public static class AddDependencies
    {
        public static void AddAllDependencies(this IServiceCollection collection)
        {
            collection.AddBLDependencies().AddDALDependencies();
        }
    }
}
