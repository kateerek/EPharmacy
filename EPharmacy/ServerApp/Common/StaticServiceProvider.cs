using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System;

namespace EPharmacy.ServerApp.Common
{
    public static class StaticServiceProvider
    {
        private static IServiceProvider _instance;

        public static IServiceProvider Instance
        {
            get => _instance ?? throw new Exception("StaticServiceProvider not initialized.");
            private set => _instance = value;
        }

        public static void Initialize(IServiceProvider serviceProvider)
        {
            if (_instance != null)
            {
                throw new Exception("StaticServiceProvider has already been initialized");
            }
            Instance = serviceProvider;
        }
        public static IStringLocalizer<T> GetLocalizer<T>()
        {
            return Instance.GetService<IStringLocalizer<T>>();
        }
    }
}
