using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BVMinh.EmailService.Common.Kafka
{
    public class ProducerWrapper<Tkey, TValue> : IDisposable
    {
        private readonly IProducer<Tkey, TValue> _producer;

        private readonly string _topic;

        private readonly ProducerConfig _producerConfig;

        public ProducerWrapper(ProducerConfig producerConfig, string topic)
        {
            _producerConfig = producerConfig;
            _topic = topic;
            _producer = new ProducerBuilder<Tkey, TValue>(_producerConfig).Build();
        }

        public async Task SendMessage(TValue message)
        {
            var dr = await _producer.ProduceAsync(_topic, new Message<Tkey, TValue>()
            {
                Value = message
            });
        }

        public void Dispose()
        {
            _producer.Dispose();
        }
    }
}
