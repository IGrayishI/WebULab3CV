using MongoDB.Driver;

namespace MongoCRUD.Data
{
    public class MongoCRUD
    {
        private IMongoDatabase db;


        public MongoCRUD(string database)
        {
            var client = new MongoClient("-CONNECTION-STRING-MISSING-");
            db = client.GetDatabase(database);
        }

        public Task Create(string table, Question q)
        {
            var collection = db.GetCollection<Question>(table);
            collection.InsertOne(q);
            return Task.CompletedTask;
        }

        public List<Question> ReadQuestions(string table)
        {
            var collection = db.GetCollection<Question>(table);
            return collection.Find(_ => true).ToList();
        }

        public void UpdateQuestion(string table, Question newQ)
        {
            var collection = db.GetCollection<Question>(table);

            var filter = Builders<Question>.Filter.Eq("_id", newQ._id);

            collection.ReplaceOne(filter, newQ);
        }

        public Task DeleteQuestion(string table, Question quest)
        {
            var collection = db.GetCollection<Question>(table);
            collection.DeleteOne(x => x._id == quest._id);
            return Task.CompletedTask;
        }
    }
}
