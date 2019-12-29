using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using DiagnosticSourceDemo.Processors;

namespace DiagnosticSourceDemo
{
    public class InstrumentationHostedService : IHostedService
    {
        private readonly DiagnosticListenerObserver _observer;

        public InstrumentationHostedService(DiagnosticListenerObserver observer)
        {
            _observer = observer;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            DiagnosticListener.AllListeners.Subscribe(_observer);
            await Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
        }
    }
}
