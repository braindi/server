using Microsoft.Extensions.DependencyInjection;
using IDAL;
namespace DAL
{
    public static class CommonDAL
    {
        public static IServiceCollection AddDALDependencies(this IServiceCollection collection)
        {
            collection.AddScoped(typeof(IAttributeDAL), typeof(AttributeDAL));
            collection.AddScoped(typeof(IAttributeCategoryDAL), typeof(AttributeCategoryDAL));
            collection.AddScoped(typeof(ICategoryDAL), typeof(CategoryDAL));
            collection.AddScoped(typeof(IQuestionDAL), typeof(QuestionDAL));
            return collection;
        }
    }
}
