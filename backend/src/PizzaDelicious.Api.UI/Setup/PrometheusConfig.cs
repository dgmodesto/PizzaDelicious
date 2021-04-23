using Microsoft.AspNetCore.Builder;
using Prometheus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDelicious.Api.UI.Setup
{
    public static class PrometheusConfig
    {

        public static IApplicationBuilder UsePrometheusConfiguration(this IApplicationBuilder app)
        {
            // Custom Metrics to count requests for each endpoint and the method
            var counter = Metrics.CreateCounter("pizza_delicious_service", "Counts requests to the PizzaDeliciousService API endpoints",
                new CounterConfiguration
                {
                    LabelNames = new[] { "method", "endpoint" }
                });

            app.Use((context, next) =>
            {
                counter.WithLabels(context.Request.Method, context.Request.Path).Inc();
                return next();
            });

            // Use the prometheus middleware
            app.UseMetricServer();
            app.UseHttpMetrics();

            return app;

        }
    }
}
