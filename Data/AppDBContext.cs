using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dagagino.Models;
using MongoDB.Driver;

namespace Dagagino.Data
{

    public class AppDBContext
    {
        private readonly IMongoDatabase _db;
        public AppDBContext(IDagaginoDBSettings settings)
        {      
            var client = new MongoClient(settings.DefaultConnection);
            _db = client.GetDatabase(settings.DBName);

        }

        /* -------------------------------------------------------------------------- */
        /*                              Users Collection                              */
        /* -------------------------------------------------------------------------- */
        public IMongoCollection<User> Users => _db.GetCollection<User>("Users");
        
        /* -------------------------------------------------------------------------- */
        /*                             Products Collection                            */
        /* -------------------------------------------------------------------------- */
        public IMongoCollection<Product> Products => _db.GetCollection<Product>("Products");

        /* -------------------------------------------------------------------------- */
        /*                                Governorates                                */
        /* -------------------------------------------------------------------------- */
        public IMongoCollection<Governorate> Governorates => _db.GetCollection<Governorate>("Governorates");
        public IMongoCollection<GovernorateState> GovernorateStates => _db.GetCollection<GovernorateState>("GovernorateStates");


    }
}