using YoungDeveloper.Application.Models;

namespace YoungDeveloper.Pages.Components;

public class PageState
{
	public User User { get; set; } = new();
	public string AppName { get; set; }
}