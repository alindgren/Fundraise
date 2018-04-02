using System;
using EasyNetQ;

namespace Fundraise.IntegrationEvents.RabbitMQ
{
    public class EventBusRabbitMQ : IDisposable, IEventBus
    {
        private readonly IBus bus;

        public EventBusRabbitMQ()
        {
            //IEasyNetQLogger logger = new DiagnosticsLogger();
            bus = RabbitHutch.CreateBus("host=localhost;username=guest;password=guest");//,
            //    serviceRegister => serviceRegister.Register(serviceProvider => logger));
        }

        public void Publish<T>(T e) where T : IntegrationEvent
        {
            if (bus != null)
                bus.Publish(e); // use Polly for retry?
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    bus.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~EventBusRabbitMQ() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
