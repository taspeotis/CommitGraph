using System;

namespace CommitGraph.Attributes
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public sealed class EagerInstantiationAttribute : Attribute
    {
        public EagerInstantiationAttribute(Type type)
        {
            Type = type;
        }

        public Type Type { get; }
    }
}