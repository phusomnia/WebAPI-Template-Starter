using Microsoft.EntityFrameworkCore;
using WebAPI_Template_Starter.Domain.Entities;
using WebAPI_Template_Starter.Infrastructure.Persistence;
using WebAPI_Template_Starter.SharedKernel.configuration;
using WebAPI_Template_Starter.SharedKernel.persistence.data;
using WebAPI_Template_Starter.SharedKernel.utils;

namespace WebAPI_Template_Starter.Application.AccountAPI;

[Repository]
public class AccountRepository : CrudRepository<Account, String>
{
    private readonly AppDbContext _context;
    
    public AccountRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public Task<Account?> findByUsername(string username)
    {
        return _context.Accounts.FirstOrDefaultAsync(x => x.Username == username);
    }

    public async Task<ICollection<Dictionary<String, Object>>> findAccountDetail(string username)
    {
        var query = @"
                        SELECT
                          acc.id AS id,
                          acc.username AS username,
                          acc.password AS password,
                          r.name AS roleName,
                          JSON_ARRAYAGG(p.name) AS permissionList
                        FROM Account acc
                        LEFT JOIN Role r ON r.id = acc.roleId
                        LEFT JOIN RolePermission rp ON rp.roleId = r.id
                        LEFT JOIN Permission p ON p.id = rp.permissionId
                        WHERE acc.username = ?
                        GROUP BY acc.id, acc.username, r.name;";
        var result = await _context.executeSqlRawAsync(query, username);
        Console.WriteLine(CustomJson.json(result, CustomJsonOptions.WriteIndented));
        return result;
    }
}