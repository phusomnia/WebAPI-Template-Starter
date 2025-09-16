using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using WebAPI_Template_Starter.Domain.Entities;
using WebAPI_Template_Starter.Infrastructure.Database;
using WebAPI_Template_Starter.Infrastructure.Utils;

namespace WebAPI_Template_Starter.Features.AccountAPI;

[Repository]
public class AccountRepository : CrudRepository<Account, String>
{
    private readonly AppDbContext _context;
    
    public AccountRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public Account? findByUsername(string username)
    {
        return _context.Accounts.FirstOrDefault(x => x.Username == username);
    }

    public ICollection<Dictionary<String, Object>> findAccountDetail(string username)
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
        var result = _context.executeSqlRaw(query, username);
        Console.WriteLine(CustomJson.json(result, CustomJsonOptions.WriteIndented));
        return result;
    }
}