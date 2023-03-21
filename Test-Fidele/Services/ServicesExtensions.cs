namespace Test_Fidele.Services
{
    public static class ServicesExtensions
    {
        public static void AddMyPersonnalServices(this IServiceCollection services)
        {
            services.AddSingleton<IMetier, Metier>();
        }
    }
}
