using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace TodoApi.Services
{
    public class MongoDbService
    {
        private readonly IMongoCollection<TodoItem> _todoItems;
        

        public MongoDbService()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("TodoAppDb");
            _todoItems = database.GetCollection<TodoItem>("todos");
        }

      public async Task<List<TodoItem>> GetTodosAsync()
{
    return await _todoItems.Find(new BsonDocument()).ToListAsync();
}
      public async Task CreateTodoAsync(TodoItem todoItem)
{
    todoItem.Id = ObjectId.GenerateNewId();
    await _todoItems.InsertOneAsync(todoItem);
}

     public async Task<bool> DeleteTodoAsync(ObjectId id)
{
    var result = await _todoItems.DeleteOneAsync(todo => todo.Id == id);
    return result.DeletedCount > 0;
}


 
}}
