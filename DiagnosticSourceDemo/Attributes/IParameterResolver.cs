namespace DiagnosticSourceDemo.Attributes
{
    public interface IParameterResolver
    {
        object Resolve(object value);
    }
}
