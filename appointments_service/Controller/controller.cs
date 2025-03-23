using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("")]
public class AppointmentController: ControllerBase{

    [HttpGet("getAppointment/{userName}+{doctorName}+{year}+{month 00}+{day 00}")]
    public async Task<Appointment> GetAppointment(string userName, string doctorName, int year, int month, int day) {
        var date = new DateTime(year, month, day, 0, 0, 0);
        return await DbConnection.GetAppointment(userName, doctorName, date);
    }

    [HttpPost("postAppointment/{userName}+{insurance}+{doctor}+{speciality}+{month 00}+{day 00}+{hour 00--24}")]
    public async void PostAppointment(string userName, string insurance, string doctor, string speciality, int month, int day, int hour, string description) {
        await DbConnection.AddAppointment(userName, insurance, doctor, speciality, description, month, day, hour);
    }
}