﻿using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace ASP_Net_Core_example.MiddleWares
{
    /// <summary>
    /// Наш созданный класс для логирования в RequestLog.txt
    /// </summary>
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        static IWebHostEnvironment _env;

        /// <summary>
        ///  Middleware-компонент должен иметь конструктор, принимающий RequestDelegate
        /// </summary>
        public LoggingMiddleware(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            _env = env;
        }

        /// <summary>
        ///  Необходимо реализовать метод Invoke  или InvokeAsync
        /// </summary>
        public async Task InvokeAsync(HttpContext context)
        {

            // Для логирования данных о запросе используем свойста объекта HttpContext
            string logMessage = $"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}";

            // Путь до лога (опять-таки, используем свойства IWebHostEnvironment)
            string logFilePath = Path.Combine(_env.ContentRootPath, "Logs", "RequestLog.txt");


            await File.AppendAllTextAsync(logFilePath, logMessage);
            // Передача запроса далее по конвейеру
            await _next.Invoke(context);
        }
    }
}