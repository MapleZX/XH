using System;

namespace XHSystem
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class NodeAttribute : Attribute
    {
        public string nodePath;
        public NodePropertyHint hint;

        public NodeAttribute(string nodePath, NodePropertyHint hint = NodePropertyHint.None)
        {
            this.nodePath = nodePath;
            this.hint = hint;
        }
    }
}