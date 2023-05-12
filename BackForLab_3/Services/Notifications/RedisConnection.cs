using StackExchange.Redis;

namespace BackForLab_3.Services.Notifications
{
    public class RedisConnection
    {
        private static RedisConnection? _instance = null;
        public static RedisConnection Get()
        {
            if (_instance == null)
                _instance = new RedisConnection();
            return _instance;
        }
        public ISubscriber Subscriber { get; private set; }
        private RedisConnection()
        {
            ConnectionMultiplexer redis =
ConnectionMultiplexer.Connect("localhost");
            Subscriber = redis.GetSubscriber();
        }
    }
}
