using AspectCore.Extensions.Reflection;

namespace DiagnosticSourceDemo.Attributes
{
    public class PropertyAttribute : ParameterResolverAttribute
    {
        public string Name { get; set; }

        public override object Resolve(object value)
        {
            if (value == null || Name == null)
            {
                return null;
            }

            var property = value.GetType().GetProperty(Name);

            return property?.GetReflector()?.GetValue(value);
        }
    }
}
