using System.Diagnostics;
using ChatRoom.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChatRoom.Mvc.Controllers;

public class BaseController : Controller
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult Privacy()
    {
        return View();
    }

    protected IActionResult WithStatusCode(int statusCode, Func<IActionResult> result)
    {
        this.Response.StatusCode = statusCode;
        return result();
    }
}