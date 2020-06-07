using System;
using Jane.Todo.Web.Pages.ViewModels;
using Jane.Todo.Web.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Jane.Todo.Web
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie();

			services.AddDataProtection()
				.SetApplicationName("todo-jane");

			services.AddRazorPages();
			services.AddServerSideBlazor();
			services.AddHttpContextAccessor();

			services.AddTransient<IAuthService, AuthService>();
			services.AddTransient<ITodoService, TodoService>();

			services.AddTransient<IndexViewModel>();
			services.AddTransient<TaskEditViewModel>();
			services.AddTransient<TaskViewViewModel>();

			services.AddHttpClient<IHttpClientWrapper, HttpClientWrapper>(client =>
			{
				client.BaseAddress = new Uri("http://localhost:63558/");
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
			}

			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapBlazorHub();
				endpoints.MapFallbackToPage("/_Host");
			});
		}
	}
}
