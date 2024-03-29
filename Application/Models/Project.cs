﻿using System.ComponentModel.DataAnnotations.Schema;
using Spark.Library.Database;

namespace YoungDeveloper.Application.Models;

public class Project : BaseModel
{

    public string Title { get; set; } = default!;

    public string Description { get; set; } = default!;

    public int CreatedById { get; set; }

    public virtual User User { get; set; }

    public virtual ICollection<ProjectRequest> ProjectRequests {  get; set; }
}