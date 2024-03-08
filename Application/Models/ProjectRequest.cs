using Spark.Library.Database;

namespace YoungDeveloper.Application.Models;

public class ProjectRequest : BaseModel
{

    public int UserId { get; set; }

    public int ProjectId { get; set; }

    public int Status { get; set; }

    public virtual User User { get; set; }

    public virtual Project Project { get; set; }
}

public enum RequestStatusType
{
    Pending=1,
    Approved=2,
    Denied=3
}

