using System;

namespace DiagnosticSourceDemo.Attributes
{
    public class DiagnosticNameAttribute : Attribute
    {
        public string Name { get; }

        public DiagnosticNameAttribute(string name)
        {
            Name = name;
        }
    }
}
