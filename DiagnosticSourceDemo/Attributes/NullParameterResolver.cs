namespace DiagnosticSourceDemo.Attributes
{
    public class NullParameterResolver : IParameterResolver
    {
        public object Resolve(object value)
        {
            return null;
        }
    }
}
