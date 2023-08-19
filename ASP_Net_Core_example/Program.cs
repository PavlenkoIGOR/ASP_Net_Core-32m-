using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP_Net_Core_example.Models;

//Основные процессы, происходящие в IHostBuilder CreateHostBuilder:
//        Устанавливается корневая папка-каталог приложения, где при сборке проекта будет осуществляться поиск файлов проекта (например, веб-страниц для отображения).
//        Подгружаются переменные среды (о них позже) и аргументы командной строки.
//        Загружается конфигурация приложения из файлов appsettings.json.
//        Подключается механизм логирования.


namespace ASP_Net_Core_example
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //здесь уже производится сборка хоста методом Build() с последующим вызовом Run().
            CreateHostBuilder(args).Build().Run();
        }
        /// <summary>
        /// Статический метод, создающий и настраивающий IHostBuilder -
        /// объект, который в свою очередь создает хост для развертывания нашего приложения
        /// </summary>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)

       //что происходит здесь:
//        В конфигурацию загружаются переменные среды.Пример из нашего appsettings.json:   "ASPNETCORE_ENVIRONMENT": "Development"
//        Запускается тот самый кроссплатформенный сервер Kestrel, на котором будет развёрнуто приложение.
//        Добавляется компонент Host Filtering, позволяющий настраивать для Kestrel веб-адреса.
//        Если приложение нужно развернуть в Windows-окружении на IIS, то здесь также выполняется интеграция с IIS.
//Дальше вызовом метода webBuilder.UseStartup<Startup>() будет определён и подключён класс Startup, в котором непосредственно запускаются настраиваемые сервисы.
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    // Переопределяем путь до статических файлов по умолчанию
                    webBuilder.UseWebRoot("Views");
                });
    }
}
