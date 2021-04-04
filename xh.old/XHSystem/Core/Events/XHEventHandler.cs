using System;

namespace XHSystem
{
    public class XHEventHandler : IXHEventHandler
    {
        public event EventHandler handler;

        public void Run(object sender)
        {
            if (handler != null)
                handler(sender, EventArgs.Empty);
        }

        public void Run(object sender, EventArgs e)
        {
            if (handler != null)
                handler(sender, EventArgs.Empty);
        }
    }

    public class XHEventHandler<TArgs> : IXHEventHandler<TArgs> where TArgs : EventArgs
    {
        public event EventHandler<TArgs> handler;
        public void Run(object sender, TArgs e)
        {
            if (handler != null)
                handler(sender, e);
        }
    }
}