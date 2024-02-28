using System.Security.Claims;
using Spark.Library.Extensions;
using YoungDeveloper.Application.Database;
using YoungDeveloper.Application.Models;

namespace YoungDeveloper.Application.Services;

public class ProjectService(DatabaseContext db, IHttpContextAccessor context)
{

    private readonly DatabaseContext _db = db;
    private readonly HttpContext _context = context.HttpContext;

    public async Task<Project> CreateProjectAsync(string title, string description)
    {

        var userID = _context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!String.IsNullOrEmpty(userID))
        {
            Project project = new()
            {
                Title = title,
                Description = description,
                CreatedById = Convert.ToInt32(userID)
            };       
            
            _db.Projects.Save(project);

            return project;
        }

        return null;
    }

}