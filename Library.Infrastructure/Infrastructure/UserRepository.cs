using Library.Application.Interfaces;
using Library.Domain.IRepositories;
using Library.Domain.Permissions;
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
    private readonly IJwtProvider  _jwtProvider;

    public UserRepository(AppDbContext dbContext, UserManager<UserEntity> userManager,
        RoleManager<RoleEntity> roleManager, IJwtProvider jwtProvider)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtProvider = jwtProvider;
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

    public async Task<string> Login(string email, string password)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(x => x.Email == email);
        
        if (user == null)
            throw new Exception("There is no user");
        
        var result = await _userManager.CheckPasswordAsync(user, password);
        
        if(result == false)
            throw new  Exception("something went wrong");

        var roles = user.RoleEntity;
        var permisssions = user.RoleEntity.Permissions;

        var permissionsDomain = user.RoleEntity.Permissions.Select(x => new Permission(x.Name)
        {
            Name = x.Name
        });

        var rolesDomain = new Role()
        {
            Name = roles.Name,
            Permission = permissionsDomain.ToList()
        };

        var userDomain = new User()
        {
            Email = user.Email,
            Role = rolesDomain,
            Username = user.UserName
        };

        var token = _jwtProvider.GenerateToken(userDomain);

        return token;
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