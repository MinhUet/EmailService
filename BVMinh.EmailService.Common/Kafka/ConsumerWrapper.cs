using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Text;

namespace BVMinh.EmailService.Common.Kafka
{
    public class ConsumerWrapper<TKey, TValue> : IDisposable where TValue : class
    {
        private readonly ConsumerConfig _config;
        private readonly IConsumer<TKey, TValue> _consumer;
        private readonly string _topic;

        public ConsumerWrapper(ConsumerConfig config, string topic)
        {
            _config = config;
            _topic = topic;
            _consumer = new ConsumerBuilder<TKey, TValue>(_config).Build();
            _consumer.Subscribe(_topic);
        }

        public TValue ReadMessage()
        {
            var message = _consumer.Consume();
            return message.Message?.Value;
        }

        public void Dispose()
        {
            _consumer.Dispose();
        }
    }
}
