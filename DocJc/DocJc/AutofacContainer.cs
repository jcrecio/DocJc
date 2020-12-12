namespace DocJc
{
    using Autofac;
    using Autofac.Extras.CommonServiceLocator;
    using CommonServiceLocator;
    using Contracts.Services;
    using Mapper;
    using Service;
    using Settings;
    using ViewModels;

    public sealed class AutoFacContainer
    {
        public static void Initialize()
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<DiagnosisSearchViewModel>().AsSelf();
            containerBuilder.RegisterType<LoginViewModel>().AsSelf();
            containerBuilder.RegisterType<HealthService>().As<IHealthService>();
            containerBuilder.RegisterType<TokenProvider>().As<ITokenProvider>();
            containerBuilder.RegisterType<BaseEntityMapper>().AsSelf();
            containerBuilder.RegisterType<DiagnosticMapper>().AsSelf();
            containerBuilder.RegisterType<AppSettingsManager>().As<IAppSettingsManager>();

            IContainer container = containerBuilder.Build();

            AutofacServiceLocator autofacServiceLocator = new AutofacServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => autofacServiceLocator);
        }
    }
}
