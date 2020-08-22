using BVMinh.EmailService.Entity.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace BVMinh.EmailService.DL.Database
{
    //Class db context cho MongoDB
    public class MongoDatabaseContext : IDatabaseContext
    {

        private readonly IMongoDatabase _mongoDatabase;
        private readonly IMongoClient _mongoClient;

        public MongoDatabaseContext(IDatabaseSettings settings)
        {
            _mongoClient = new MongoClient(settings.ConnectionString);
            _mongoDatabase = _mongoClient.GetDatabase(settings.DatabaseName);
        }

        public IMongoDatabase MongoDatabase { get => _mongoDatabase; }

    }
}
