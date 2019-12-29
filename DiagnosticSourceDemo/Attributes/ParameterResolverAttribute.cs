using System;

namespace DiagnosticSourceDemo.Attributes
{
    public abstract class ParameterResolverAttribute : Attribute, IParameterResolver
    {
        public abstract object Resolve(object value);
    }
}
