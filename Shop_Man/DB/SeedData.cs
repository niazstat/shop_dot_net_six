using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.DB
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {

            OrderManagementDBContext context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<OrderManagementDBContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }
    }
}
