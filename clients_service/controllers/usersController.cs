using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("")]
public class UsersController : ControllerBase{
    private readonly UsersDB usersDB;

    public UsersController(UsersDB usersDB){
        this.usersDB = usersDB;
    }

    [HttpGet("getUser/{name}+{lastName}+{insurance_num}")]
    public async Task<string> GetUser(string name, string lastName, string insurance_num) {
        if(insurance_num == null){
            var user = (await usersDB.users.SingleOrDefaultAsync(db => db.name == name && db.lastName == lastName))!;
            return Token.TokenCreation(user.id, user.name!, user.lastName!, "no insurance", user.role!);
        } else{
            var user = (await usersDB.users.SingleOrDefaultAsync(db => db.name == name && db.lastName == lastName && db.insurance_num == insurance_num))!;
            return Token.TokenCreation(user.id, user.name!, user.lastName!, user.insurance_num!, user.role!);
        }
    }

    [HttpPost("postUser")]
    public async Task<Users> PostUser([FromBody] Users user) {
        usersDB.Add(user);
        await usersDB.SaveChangesAsync();
        return user;
    }

    [HttpDelete("deleteUser/{name}+{lastName}+{email}+{insurance_num}")]
    public async Task<string> DeleteUser(string name, string lastName, string email, string insurance_num) {
        var user = await usersDB.users.FirstOrDefaultAsync(u => u.name == name && u.lastName == lastName && u.email == email && u.insurance_num == insurance_num);
        if (user == null) {
            return "user not found";
        } else{
            usersDB.users.Remove(user!);
            return "eliminado";
        }
    }
}