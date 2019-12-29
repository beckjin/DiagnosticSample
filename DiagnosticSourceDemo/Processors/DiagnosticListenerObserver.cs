using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DiagnosticSourceDemo.Processors
{
    public class DiagnosticListenerObserver : IObserver<DiagnosticListener>
    {
        private readonly List<IDisposable> _subscriptions;
        private readonly IEnumerable<IDiagnosticProcessor> _diagnosticProcessors;
        public DiagnosticListenerObserver(IEnumerable<IDiagnosticProcessor> diagnosticProcessors)
        {
            _subscriptions = new List<IDisposable>();
            _diagnosticProcessors = diagnosticProcessors;
        }

        public void OnCompleted()
        {
            _subscriptions.ForEach(x => x.Dispose());
            _subscriptions.Clear();
        }

        public void OnError(Exception error)
        {
            // Method intentionally left empty.
        }

        public void OnNext(DiagnosticListener value)
        {
            if (_diagnosticProcessors == null) return;

            foreach (var diagnosticProcessor in _diagnosticProcessors)
            {
                if (value.Name == diagnosticProcessor.ListenerName)
                {
                    var subscription = value.Subscribe(new DiagnosticEventObserver(diagnosticProcessor));
                    _subscriptions.Add(subscription);
                    break;
                }
            }
        }
    }
}
