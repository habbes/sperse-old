using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Batch;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OData.Edm;

namespace SampleApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddControllers()
            services.AddOData();
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            });
            services.AddSingleton <TestMethods>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseMvc(routeBuilder =>
            {
                var odataBatchHandler = new DefaultODataBatchHandler();
                routeBuilder.Select().Filter().Expand().MaxTop(100).OrderBy().Count();
                routeBuilder.MapODataServiceRoute("ODataRoute", "odata", GetEdmModel(), odataBatchHandler);
            });
        }

        public static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();
            var entitySet = builder.EntitySet<Dummy>("Dynamic");
            builder.EntityType<Dummy>().Collection.Function("Call").Returns<int>()
                .Parameter<int>("x");

            builder.Action("Define")
                .Parameter<string>("source");

            return builder.GetEdmModel();
        }
    }

    public class Dummy
    {
        [Key]
        public int Key { get; set; }
    }
}