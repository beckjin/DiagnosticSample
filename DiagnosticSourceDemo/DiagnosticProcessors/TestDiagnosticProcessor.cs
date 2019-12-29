using System;
using DiagnosticSourceDemo.Attributes;
using DiagnosticSourceDemo.Processors;

namespace DiagnosticSourceDemo.DiagnosticProcessors
{
    public class TestDiagnosticProcessor : IDiagnosticProcessor
    {
        public string ListenerName { get; } = "TestDiagnosticListener";

        [DiagnosticName("RequestStart")]
        public void RequestStart([Object]string name)
        {
            Console.WriteLine(name);
        }
    }
}
