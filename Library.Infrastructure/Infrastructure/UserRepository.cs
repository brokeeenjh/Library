using Library.Domain.IRepositories;
using Library.Infrastructure.DbContext;
using Library.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Infrastructure;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;
    private readonly UserManager<UserEntity>  _userManager;
    private readonly RoleManager<RoleEntity>  _roleManager;

    public UserRepository(AppDbContext dbContext, UserManager<UserEntity> userManager,
        RoleManager<RoleEntity> roleManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _roleManager = roleManager;
    }


    public async Task Register(string username, string password, string email)
    {
        if (await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email) != null)
        {
            throw new Exception("Email is already registered");
        }

        var user = new UserEntity()
        {
            UserName = username,
            Email = email
        };
        
       var result =  await _userManager.CreateAsync(user, password);

       if (!result.Succeeded)
       {
           throw new Exception("something went wrong");
       }
       
       var resultAddRole = await _userManager.AddToRoleAsync(user, "User");

       if (!resultAddRole.Succeeded)
       {
           throw new Exception("something went wrong");
       }
        
    }

    public Task<string> Login(string email, string password)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<string>> GetAllPermissions(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());

        if (user == null)
            throw new Exception("There is no user");

        var roles = await _userManager.GetRolesAsync(user);

        var permissions = await _dbContext.Roles
            .Where(x => roles.Contains(x.Name))
            .SelectMany(x => x.Permissions)
            .Select(x => x.Name)
            .Distinct()
            .ToListAsync();

        return permissions;
    }
}