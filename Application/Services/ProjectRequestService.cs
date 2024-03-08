using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Spark.Library.Extensions;
using YoungDeveloper.Application.Database;
using YoungDeveloper.Application.Models;

namespace YoungDeveloper.Application.Services;

public class ProjectRequestService(DatabaseContext db, IHttpContextAccessor context)
{

    private readonly DatabaseContext _db = db;
    private readonly HttpContext _context = context.HttpContext;

    public ProjectRequest? CreateProjectRequest(int projectId)
    {

        var userId = _context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId)) return null;

        ProjectRequest request = new()
        {
            ProjectId = projectId,
            UserId = Convert.ToInt32(userId)
        };

        _db.ProjectRequests.Save(request);

        return request;
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

    public async Task<List<Project>?> GetCurrentUsersProjects()
    {
        var userId = _context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId)) return null;

        return await _db.Projects.Include(x => x.User).Where(x => x.CreatedById == Convert.ToInt32(userId)).ToListAsync();
    }
}