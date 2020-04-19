using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hangfire;
using HangfireExample.Library.Services;
using Microsoft.AspNetCore.Mvc;

namespace HangfireExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly SomeService service;

        public HomeController(SomeService service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            try
            {
                BackgroundJob.Enqueue(() => EnqueueMethod());
                BackgroundJob.Enqueue(() => ServiceMethod());
                BackgroundJob.Schedule(() => ScheduleMethod(), TimeSpan.FromSeconds(10));
                RecurringJob.AddOrUpdate("ID-Section", () => RecurringJobMethod(), Cron.Minutely);
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
            return View();
        }

        public IActionResult Index2()
        {
            RecurringJob.RemoveIfExists("ID-Section");
            return RedirectToAction("Index");
        }

        public void EnqueueMethod ()
        {
            Thread.Sleep(2000);
            Console.WriteLine(">>>>> EnqueueMethod finished!");
        }

        public void ScheduleMethod ()
        {
            Thread.Sleep(2000);
            Console.WriteLine(">>>>> ScheduleMethod finished!");
        }

        public void RecurringJobMethod()
        {
            Thread.Sleep(2000);
            Console.WriteLine(">>>>> RecurringJobMethod finished!");
        }

        public void ServiceMethod()
        {
            Thread.Sleep(2000);
            Console.WriteLine(service.GetInfo());
        }
    }
}