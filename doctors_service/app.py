from fastapi import FastAPI
from dbConnection import DbConnection
from Doctors import Doctor

apiDoctors = FastAPI()

@apiDoctors.get("/doctors", response_model=list[Doctor])
async def GetAllDoctors():
    return DbConnection.GetDoctors()

@apiDoctors.post("/postDoctor")
async def PostDoctor(_doctor: Doctor):
    return DbConnection.InsertDoctor(_doctor)

@apiDoctors.get("/doctor")
async def GetDoctor(name: str, lastName:str, speciality:str):
    return DbConnection.GetOneDoctor(name, lastName, speciality)

@apiDoctors.put("/updateDoctor")
async def UpdateDoctor(name: str, lastName:str, speciality:str, _doctor:Doctor):
    return DbConnection.UpdateDoctor(name, lastName, speciality, _doctor)