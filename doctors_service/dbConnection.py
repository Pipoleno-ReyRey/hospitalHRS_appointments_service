from dataclasses import asdict
import pymongo
import pymongo.collection
from Doctors import Doctor

class DbConnection:

    @staticmethod
    def GetDoctors():
        urlConnection = "mongodb+srv://reynaldo:reynaldo066512@cluster0.g786f.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0"
        mongoClient = pymongo.MongoClient(urlConnection)
        database = mongoClient["doctorsDb"]
        collection = database["doctors"]
        data = collection.find()
        doctors = []
        for doctor in data:
            _doctor = Doctor( 
                    name=doctor["name"],
                    lastName=doctor["lastName"],
                    email=doctor["email"],
                    phone=doctor["phone"],
                    roomConsult=doctor["roomConsult"],
                    speciality=doctor["speciality"],
                    salary=doctor["salary"])
            
            doctors.append(_doctor)        
        return doctors

    @staticmethod
    def InsertDoctor(doctor: Doctor):
        urlConnection = "mongodb+srv://reynaldo:reynaldo066512@cluster0.g786f.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0"
        mongoClient = pymongo.MongoClient(urlConnection)
        database = mongoClient["doctorsDb"]
        collection = database["doctors"]
        collection.insert_one(vars(doctor))
        return doctor

    @staticmethod
    def GetOneDoctor(name: str, lastName: str, speciality: str):
        _doctor = None
        urlConnection = "mongodb+srv://reynaldo:reynaldo066512@cluster0.g786f.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0"
        mongoClient = pymongo.MongoClient(urlConnection)
        database = mongoClient["doctorsDb"]
        collection = database["doctors"]
        filters = {"name":{"$regex":name, "$options":"i"}, "lastName": {"$regex":lastName, "$options":"i"}, "speciality": {"$regex":speciality, "$options":"i"}}
        data = collection.find(filters)

        for doctor in data:
            _doctor = Doctor( 
                    name=doctor["name"],
                    lastName=doctor["lastName"],
                    email=doctor["email"],
                    phone=doctor["phone"],
                    roomConsult=doctor["roomConsult"],
                    speciality=doctor["speciality"],
                    salary=doctor["salary"])
        
        return _doctor 