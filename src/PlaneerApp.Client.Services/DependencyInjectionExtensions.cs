﻿using Microsoft.Extensions.DependencyInjection;
using PlannerApp.Client.Services.Interfaces;
using PlannerApp.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneerApp.Client.Services;

public static class DependencyInjectionExtensions
{

    public static IServiceCollection AddHttpClientServices(this IServiceCollection services)
    {
        return services.AddScoped<IAuthenticationService, HttpAuthenticationService>()
                       .AddScoped<IPlansService, HttpPlansService>();
                       //.AddScoped<IToDoItemsService, HttpToDoItemsService>();
    }

}
