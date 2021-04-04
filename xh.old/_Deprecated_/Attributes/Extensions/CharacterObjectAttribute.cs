using System;
namespace _Deprecated_
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class CharacterObjectAttribute : Attribute
    {
        public string parameter;
        public CharacterObjectAttribute(string parameter)
        {
            this.parameter = parameter;
        }
    }
}