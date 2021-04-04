using System;

namespace XHSystem
{
    public interface IXHEventHandler
    {
        event EventHandler handler;
        void Run(object sender);
        void Run(object sender, EventArgs e);
    }
    public interface IXHEventHandler<TArgs> where TArgs : EventArgs
    {
        event EventHandler<TArgs> handler;
        void Run(object sender, TArgs e);
    }
}