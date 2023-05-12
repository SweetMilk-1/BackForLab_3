using StackExchange.Redis;

namespace BackForLab_3.Services.Notifications
{
    public class NotificationService : INotificationService
    {
        public void Notify(string message, string channel)
        {
            RedisConnection.Get().Subscriber.Publish(channel, message);
        }
    }
}
