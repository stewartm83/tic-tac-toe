using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using tictactoe_server.Models;

namespace tictactoe_server
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
			var connectionString = Configuration.GetConnectionString("DefaultConnection");
			services.AddDbContext<TicTacToeContext>(options => options.UseSqlite(connectionString));
		
			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy",
					builder =>
					{
						builder
							.AllowAnyHeader()
							.AllowAnyMethod()
							.AllowCredentials()
							.WithOrigins("http://localhost:8080");
					});
			});

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
				.AddJsonOptions(options =>
				{
					options.SerializerSettings.ReferenceLoopHandling =
											   Newtonsoft.Json.ReferenceLoopHandling.Ignore;
				}); ;
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseCors("CorsPolicy");

			app.UseMvc();
		}
	}
}
