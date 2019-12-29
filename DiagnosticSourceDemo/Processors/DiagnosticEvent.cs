using AspectCore.Extensions.Reflection;
using DiagnosticSourceDemo.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DiagnosticSourceDemo.Processors
{
    internal class DiagnosticEvent
    {
        private readonly IDiagnosticProcessor _diagnosticProcessor;
        private readonly IParameterResolver[] _parameterResolvers;
        private readonly MethodReflector _reflector;

        public DiagnosticEvent(IDiagnosticProcessor diagnosticProcessor, MethodInfo method)
        {
            _diagnosticProcessor = diagnosticProcessor;
            _reflector = method.GetReflector();
            _parameterResolvers = GetParameterResolvers(method).ToArray();
        }

        public void Invoke(object value)
        {
            var args = new object[_parameterResolvers.Length];
            for (var i = 0; i < _parameterResolvers.Length; i++)
            {
                args[i] = _parameterResolvers[i].Resolve(value);
            }

            _reflector.Invoke(_diagnosticProcessor, args);
        }

        private static IEnumerable<IParameterResolver> GetParameterResolvers(MethodInfo methodInfo)
        {
            foreach (var parameter in methodInfo.GetParameters())
            {
                var binder = parameter.GetCustomAttribute<ParameterResolverAttribute>();
                if (binder != null)
                {
                    if (binder is ObjectAttribute objectBinder)
                    {
                        if (objectBinder.TargetType == null)
                        {
                            objectBinder.TargetType = parameter.ParameterType;
                        }
                    }
                    if (binder is PropertyAttribute propertyBinder)
                    {
                        if (propertyBinder.Name == null)
                        {
                            propertyBinder.Name = parameter.Name;
                        }
                    }
                    yield return binder;
                }
                else
                {
                    yield return new NullParameterResolver();
                }
            }
        }
    }
}
