using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using OpenId3as.DivulgacaoONGs.Domain.Entities;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using System;

namespace OpenId3as.DivulgacaoONGs.Infra.Data.Context.Mongo
{
    public class NOGContext : IDisposable
    {
        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _database;

        public NOGContext(string connectionString, string database)
        {
            _mongoClient = new MongoClient(connectionString);
            _database = _mongoClient.GetDatabase(database);
            Map();
        }

        public IMongoDatabase Db
        {
            get
            {
                return _database;
            }
        }

        public IMongoCollection<Collaborator> Collaborator
        {
            get
            {
                return _database.GetCollection<Collaborator>("Collaborator");
            }
        }

        public IMongoCollection<Contact> Contact
        {
            get
            {
                return _database.GetCollection<Contact>("Contact");
            }
        }

        public IMongoCollection<Home> Home
        {
            get
            {
                return _database.GetCollection<Home>("Home");
            }
        }

        public IMongoCollection<HowToHelp> HowToHelp
        {
            get
            {
                return _database.GetCollection<HowToHelp>("HowToHelp");
            }
        }

        public IMongoCollection<Language> Language
        {
            get
            {
                return _database.GetCollection<Language>("Language");
            }
        }

        public IMongoCollection<Logo> Logo
        {
            get
            {
                return _database.GetCollection<Logo>("Logo");
            }
        }

        public IMongoCollection<Volunteer> Volunteer
        {
            get
            {
                return _database.GetCollection<Volunteer>("Volunteer");
            }
        }

        public IMongoCollection<WhoAreWe> WhoAreWe
        {
            get
            {
                return _database.GetCollection<WhoAreWe>("WhoAreWe");
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        private void Map()
        {
            BsonClassMap.RegisterClassMap<MongoEntity>(cm =>
            {
                cm.AutoMap();
                cm.MapIdProperty(c => c.Id);
            });
        }
    }
}
