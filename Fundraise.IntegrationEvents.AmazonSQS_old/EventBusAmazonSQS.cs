using System;
using Amazon.SQS;
using Amazon.SQS.Model;
using Newtonsoft.Json;

namespace Fundraise.IntegrationEvents.AmazonSQS
{
    public class EventBusAmazonSQS : IDisposable, IEventBus
    {
        private readonly AmazonSQSClient sqsClient;

        public EventBusAmazonSQS()
        {
            var sqsConfig = new AmazonSQSConfig()
            {
                ServiceURL = "http://sqs.us-west-2.amazonaws.com"
            };
            sqsClient = new AmazonSQSClient(sqsConfig);
        }

        public void Publish<T>(T e) where T : IntegrationEvent
        {
            var sendMessageRequest = new SendMessageRequest()
            {
                QueueUrl = "https://sqs.us-west-2.amazonaws.com/852229429830/FundraiseDonations",
                MessageBody = JsonConvert.SerializeObject(e)
            };
            var sendMessageResponse = sqsClient.SendMessageAsync(sendMessageRequest);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~EventBusAmazonSQS() {
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
