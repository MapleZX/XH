using System;
using System.Reflection;
using System.Collections.Generic;
namespace XH.Container
{
    public class ServiceModule
    {
        public string name { get; }
        public string pattern { get; }
        public Type i_service { get; }
        public Type service { get; }
        public ServiceModule(){}
        public ServiceModule(string name, string pattern, Type i_service, Type service)
        {
            this.name = name;
            this.pattern = pattern;
            this.i_service = i_service;
            this.service = service;
        }
    }
}