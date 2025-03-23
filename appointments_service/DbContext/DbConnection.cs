using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using System.Text.Json.Serialization;
using MongoDB.Driver;

public class DbConnection{
    public static IMongoDatabase? mongoDatabase;

    private class Doctor{
        public string? name { get; set; }
        public string? lastName { get; set; }
        public string? speciality { get; set; }
    }
    public DbConnection(){
        var connection = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json").Build().GetConnectionString("connection");
        MongoClient client= new MongoClient(connection);
        mongoDatabase = client.GetDatabase("appointments");
    }

    public static Task<Appointment> AddAppointment(string userName, string insurance, string doctor, string speciality, string description, int month, int day, int hour){
        var appointment = new Appointment();
        appointment.userName = userName;
        appointment.insurance = insurance;
        appointment.doctorName = doctor;
        appointment.speciality = speciality;
        appointment.date = new DateTime(DateTime.Now.Year, month, day, hour, 0, 0);
        appointment.description = description;
        var collection = mongoDatabase!.GetCollection<Appointment>("appointments");
        collection.InsertOne(appointment);
        return Task.FromResult(appointment);
    }

    public static async Task<Appointment> GetAppointment(string userName, string doctorName, DateTime date){
        var collection = mongoDatabase!.GetCollection<Appointment>("appointments");
        var filter1 = Builders<Appointment>.Filter.Eq("userName", userName);
        var filter2 = Builders<Appointment>.Filter.Eq("doctorName", doctorName);
        var filter3 = Builders<Appointment>.Filter.Eq("date", date);
        var filters = Builders<Appointment>.Filter.And(filter1, filter2, filter3);
        var data = collection.Find(filters).FirstOrDefaultAsync();
        return await data;
    }


}