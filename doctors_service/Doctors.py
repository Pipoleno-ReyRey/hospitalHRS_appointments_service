from pydantic import BaseModel, Field

class Doctor(BaseModel):
    name: str
    lastName: str
    email: str
    phone: str
    roomConsult: str
    speciality: str
    salary: float