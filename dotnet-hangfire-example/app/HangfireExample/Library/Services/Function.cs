using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangfireExample.Library.Services
{
    public class Function
    {
        private readonly SomeService service;

        public Function(SomeService service)
        {
            this.service = service;
        }

        public void PrintServiceInfo()
        {
            Console.WriteLine(service.GetInfo());
        }
    }
}
