using System;
using System.Collections.Generic;

namespace DiagnosticSourceDemo.Processors
{
    public class DiagnosticEventObserver : IObserver<KeyValuePair<string, object>>
    {
        private readonly DiagnosticEventCollection _eventCollection;

        public DiagnosticEventObserver(IDiagnosticProcessor diagnosticProcessor)
        {
            _eventCollection = new DiagnosticEventCollection(diagnosticProcessor);
        }

        public void OnCompleted()
        {
            // Method intentionally left empty.
        }

        public void OnError(Exception error)
        {
            // Method intentionally left empty.
        }

        public void OnNext(KeyValuePair<string, object> value)
        {
            var diagnosticEvent = _eventCollection.GetDiagnosticEvent(value.Key);
            if (diagnosticEvent == null) return;

            try
            {
                diagnosticEvent.Invoke(value.Value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
