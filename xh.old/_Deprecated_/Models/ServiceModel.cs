using System;
namespace _Deprecated_
{
    internal class ServiceModel
    {
        public enum PatternStatus { TRANSIENTS, SCOPEDS, SINGLETONS}
        public string name { get; private set; }
        public PatternStatus pattern { get; private set; }
        public Type service{ get; private set; }
        public Type client { get; private set; }
        public ServiceModel(){}
        public ServiceModel(string name, PatternStatus pattern, Type service, Type client)
        {
            this.name = name;
            this.pattern = pattern;
            this.service = service;
            this.client = client;
        }
        public void SetModule(string name, PatternStatus pattern, Type service, Type client)
        {
            this.name = name;
            this.pattern = pattern;
            this.service = service;
            this.client = client;
        }
    }
}