using Microsoft.Extensions.DependencyInjection;
using IBL;

namespace BL
{
    public static class CommonBL
    {
        public static IServiceCollection AddBLDependencies(this IServiceCollection collection)
        {
            collection.AddScoped(typeof(IAttributeBL), typeof(AttributeBL));
            collection.AddScoped(typeof(IAttributeCategoryBL), typeof(AttributeCategoryBL));
            collection.AddScoped(typeof(ICategoryBL), typeof(CategoryBL));
            collection.AddScoped(typeof(IQuestionBL), typeof(QuestionBL));
            return collection;
        }
    }
}
