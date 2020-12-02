using System;

namespace Advent
{
    public static class ServiceProviderExtensions
    {
        public static object GetThrowingService(this IServiceProvider serviceProvider, Type type) =>
            serviceProvider.GetService(type) ?? throw new InvalidOperationException($"Did not register type {type.FullName}");
    }
}
