using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheckoutCom.ShoppingList.DataAccess;
using CheckoutCom.ShoppingList.DataAccess.Base;
using CheckoutCom.ShoppingList.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CheckoutCom.ShoppingList
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
            services.AddDbContext<ShoppingListContext>(options => options.UseInMemoryDatabase("ShoppingList"));
            services.AddMvc();
            services.AddTransient<IRepository<ShoppingListEntity>, ShoppingListRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
