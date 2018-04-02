namespace Fundraise.IntegrationEvents
{
    public interface IEventBus
    {
        void Publish<T>(T e) where T : IntegrationEvent;
    }
}
