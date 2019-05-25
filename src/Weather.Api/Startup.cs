using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Weather.Core;
using Weather.Providers;
using Weather.Providers.OpenWeather;

namespace Weather.Api
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
			services.Configure<OpenWeatherOptions>(Configuration.GetSection("OpenWeather"));

			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

			services
				.AddHttpClient<OpenWeatherProvider>()
				.AddHttpMessageHandler<AppIdHeaderHandler>();

			services
				.AddTransient<AppIdHeaderHandler>()
				.AddTransient<IWeatherProvider, OpenWeatherAdapter>();

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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