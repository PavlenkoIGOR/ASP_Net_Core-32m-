using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP_Net_Core_example.MiddleWares;
using System.Text;

namespace ASP_Net_Core_example
{
    public class Startup
    {
        static IWebHostEnvironment _env;

        public Startup(IWebHostEnvironment env)
        {
            _env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            ////добавление функционала MVC-приложения
            //services.AddMvc();

            ////функционал авторизации:
            //services.AddAuthentication();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// Метод вызывается средой ASP.NET. Используйте его для настройки конвейера запросов
        /// Здесь устанавливается порядок обработки запросов нашим приложением.
        /// </summary>
        /// IWebHostEnvironment (который передаётся вторым параметром) содержит информацию о среде запуска 
        /// приложения и позволяет с ней взаимодействовать.
        public void Configure(IApplicationBuilder app)
        {
            //здесь: 
            //Страница с детальной информацией об ошибках и
            //исключениях у нас будет доступна только в режиме разработчика.
            if (_env.IsDevelopment() || _env.IsStaging())
            {
                // 1. Добавляем компонент, отвечающий за диагностику ошибок
                app.UseDeveloperExceptionPage();

            }

            //маршрутизатор
            // 2. Добавляем компонент, отвечающий за маршрутизацию
            app.UseRouting();

            // 3. Добавляем компонент с настройкой маршрутов
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context => {
                    //это тренеровочная строчка
                    //await context.Response.WriteAsync($"Welcome to the {_env.ApplicationName}!"); 

                    string viewPath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "Index.html");
                    var html = new StringBuilder(await File.ReadAllTextAsync(viewPath));
                        //.Replace("<!--SIDEBAR-->", sidebarHTML)
                    await context.Response.WriteAsync(html.ToString());
                });
            });

            app.Map("/about", About);
            app.Map("/config", Config);
            app.UseMiddleware<LoggingMiddleware>(); //это для логгирования
            app.UseStaticFiles();//возможность использовать статические файлы (wwwroot)

            // обрабатываем ошибки HTTP
            app.UseStatusCodePages();

            Console.WriteLine($"Launching project from: {_env.ContentRootPath}");

            // Добавим в конвейер запросов обработчик самым простым способом
            //результат увидим, если после адреса стартовой страницы через слеш ввести что угодно
            app.Run(async (context) =>
            {
                int zero = 0;
                int result = 4 / zero;

                await context.Response.WriteAsync($"Page not found");

                //await context.Response.WriteAsync($"app.Run()\nWelcome to the {_env.ApplicationName}!");
            });
        }

        /// <summary>
        ///  Обработчик для страницы About
        /// </summary>
        private static void About(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync($"AboutPage\n{_env.ApplicationName} - ASP.Net Core tutorial project");
            });
        }

        /// <summary>
        ///  Обработчик для главной страницы
        /// </summary>
        private static void Config(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync($"ConfigPage\nApp name: {_env.ApplicationName}. App running configuration: {_env.EnvironmentName}");
            });
        }
    }
}
