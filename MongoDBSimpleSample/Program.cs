using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB;
using MongoDB.Driver;
using MongoDB.Bson;

namespace MongoDBSimpleSample
{
    class Program
    {

        static void Main(string[] args)
        {
            MongoDBOpr mng = new MongoDBOpr();

            //connect to mongodb server
            mng.connectToMongoDB("mongodb://192.168.99.100:32769", "euldb");

            //get document list from collection
            mng.readAllDocumentsFromCollection("eulcol");
            //get document by a given key
            mng.findADocumentFromCollection("eulcol", "name", "Engin");
            //insert new document
            mng.addNewDocumentSample("eulcol");
            //read document after insertion
            mng.findADocumentFromCollection("eulcol", "name", "enginunal");

        }

    }

    public class MongoDBOpr
    {        
        private MongoClient _client;
        private IMongoDatabase _database;
        
        public void connectToMongoDB(string mongoConnString, string mongoDBName)
        {
            _client = new MongoDB.Driver.MongoClient(mongoConnString);
            _database = _client.GetDatabase(mongoDBName);
        }

        public IEnumerable<BsonDocument> readAllDocumentsFromCollection(string collName)
        {
            var coll = _database.GetCollection<BsonDocument>(collName);
            var docs = coll.Find(Builders<BsonDocument>.Filter.Empty).ToList();

            foreach (BsonDocument doc in docs)
            {
                Console.WriteLine("Doc is => " + doc.ToJson() + '\n');
            }

            return docs.ToList();
        }

        public IEnumerable<BsonDocument> findADocumentFromCollection(string collName, string filterField, string filterValue)
        {
            var coll = _database.GetCollection<BsonDocument>(collName);
            var docs = coll.Find(Builders<BsonDocument>.Filter.Eq(filterField,filterValue)).ToList();

            foreach (BsonDocument doc in docs)
            {
                Console.WriteLine(filterField + "=" + filterValue + " filtered result doc is => " + doc.ToJson() + '\n');
            }

            return docs.ToList();
        }

        public void addNewDocumentSample(string collName)
        {
            var coll = _database.GetCollection<BsonDocument>(collName);
            var docs = coll.Find(Builders<BsonDocument>.Filter.Empty).ToList();

            BsonDocument doc = new BsonDocument();
            doc.Add("name", "enginunal");
            doc.Add("surname", "unalengin");

            coll.InsertOne(doc);


        }


    }
}
