using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Spark.Library.Extensions;
using YoungDeveloper.Application.Database;
using YoungDeveloper.Application.Models;

namespace YoungDeveloper.Application.Services;

public class ProjectService(DatabaseContext db, IHttpContextAccessor context)
{

    private readonly DatabaseContext _db = db;
    private readonly HttpContext _context = context.HttpContext;

    public Project? CreateProject(string title, string description)
    {

        var userId = _context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId)) return null;
        
        Project project = new()
        {
            Title = title,
            Description = description,
            CreatedById = Convert.ToInt32(userId)
        };       
            
        _db.Projects.Save(project);

        return project;

    }

    public async Task<List<Project>?> GetProjectsBySearch(string searchString)
    {
        if (string.IsNullOrEmpty(searchString))
        {
            return await _db.Projects.Include(x => x.User).ToListAsync(); 
        }
        
        return await _db.Projects.Include(x => x.User).Where(x => x.Title.Contains(searchString) || x.Description.Contains(searchString))
            .ToListAsync();
    }

}