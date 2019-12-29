using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using DiagnosticSourceDemo.Attributes;

namespace DiagnosticSourceDemo.Processors
{
    internal class DiagnosticEventCollection : IEnumerable<DiagnosticEvent>
    {
        private readonly List<DiagnosticEvent> _events;

        public DiagnosticEventCollection(IDiagnosticProcessor diagnosticProcessor)
        {
            _events = new List<DiagnosticEvent>();
            foreach (var method in diagnosticProcessor.GetType().GetMethods())
            {
                var diagnosticName = method.GetCustomAttribute<DiagnosticNameAttribute>();
                if (diagnosticName == null)
                    continue;
                _events.Add(new DiagnosticEvent(diagnosticProcessor, method, diagnosticName.Name));
            }
        }

        public IEnumerator<DiagnosticEvent> GetEnumerator()
        {
            return _events.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _events.GetEnumerator();
        }
    }
}
