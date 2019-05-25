using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Weather.Api.Misc;
using Weather.Core;
using Weather.Core.Services;
using Weather.Core.Services.Impl;
using Weather.Core.Workers;
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

			services
				.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

			services
				.AddHttpClient<OpenWeatherProvider>()
				.AddHttpMessageHandler<AppIdQueryStringHandler>()
				.AddHttpMessageHandler<HttpClientErrorHandler>();

			services
				.AddTransient<AppIdQueryStringHandler>()
				.AddTransient<HttpClientErrorHandler>()
				.AddTransient<WeatherWorker>()
				.AddTransient<IWeatherProvider, OpenWeatherAdapter>()
				.AddTransient<IWeatherService, WeatherService>();

			services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info {Title = "Weather API", Version = "v1"}); });

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.UseDeveloperExceptionPage();

			app.UseSwagger();

			app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Weather API V1"); });

			app.UseMiddleware<ExceptionHandlerMiddleware>();

			app.UseMvc();
		}
	}
}