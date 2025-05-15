using Microsoft.Practices.Unity;
using BellonaAPI.Filters;
using BellonaAPI.DataAccess.Interface;
using BellonaAPI.DataAccess.Class;
using System.Web.Http;

namespace BellonaAPI
{
    public static class WebApiConfig
    {
        public static string _WebApiExecutionPath = "api";
        public static void Register(HttpConfiguration config)
        {
            ResolveDependency(config);

            // Web API configuration and services
            config.Filters.Add(new CustomExceptionFilter());
            config.Filters.Add(new GZipCompressionAttribute());

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        private static void ResolveDependency(HttpConfiguration config)
        {
            // Web API configuration and services
            var container = new UnityContainer();
            container.RegisterType<IUserMenuRightsRepository, UserMenuRightsRepository>();
            container.RegisterType<IUserProfilRepository, UserProfileReposiotory>();
            container.RegisterType<ICountryRepository, CountryRepository>();
            container.RegisterType<ICurrencyRepository, CurrencyRepository>();
            container.RegisterType<IBrandRepository, BrandRepository>();
            container.RegisterType<ICityRepository, CityRepository>();
            container.RegisterType<IClusterRepository, ClusterRepository>();
            container.RegisterType<IRegionRepository, RegionRepository>();
            container.RegisterType<IOutletRepository, OutletRepository>();
            container.RegisterType<ITransactionRepository, TransactionRepository>();
            container.RegisterType<IDashboardRepository, DashboardRepository>();

            container.RegisterType<IMastersRepository, MastersRepository>();
            container.RegisterType<IHistoryDashboardRepository, HistoryDashboardRepository>();

            container.RegisterType<IPresentDashboardRepository, PresentDashboardRepository>();

            container.RegisterType<IDeliver_GuestPartners, Deliver_GuestPartnersRepository>();
            container.RegisterType<IGuestRepository, GuestRepository>();
            container.RegisterType<ICashModuleRepository, CashModuleRepository>();
            container.RegisterType<IProspectRepository, ProspectRepository>();
            container.RegisterType<IProspectDashboardRepository, ProspectDashboardRepository>();
            container.RegisterType<ISiteMasterRepository, SiteMasterRepository>();

            container.RegisterType<IBillingRepository, BillingRepository>();
            container.RegisterType<IEInvoiceUploadDownloadRepository, EInvoiceUploadDownloadRepository>();
            container.RegisterType<IBillingDashboardRepository, BillingDashboardRepository>();
            container.RegisterType<IItemMasterRepository, ItemMasterRepository>();
            container.RegisterType<IOutletItemMappingRepository, OutletItemMappingRepository>();
            container.RegisterType<IScheduleStockCountRepository, ScheduleStockCountRepository>();
            container.RegisterType<IInventoryEntryRepository, InventoryEntryRepository>();
            container.RegisterType<IStockTransferRepository, StockTransferRepository>();

            container.RegisterType<ICommonRepository, CommonRepository>();
            container.RegisterType<IHelpFileRepository, HelpFileRepository>();

            container.RegisterType<IManPowerRepository, ManPowerRepository>();


            config.DependencyResolver = new UnityResolver(container);
        }

    }
}
