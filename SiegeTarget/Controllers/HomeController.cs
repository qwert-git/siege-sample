using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SiegeTarget.Models;
using SiegeTarget.Services;

namespace SiegeTarget.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly RequestLogsService _requestsService;

    public HomeController(ILogger<HomeController> logger, RequestLogsService requestLogsService)
    {
        _logger = logger;
        _requestsService = requestLogsService;
    }

    public async Task<IActionResult> Index()
    {
        var model = new RequestLogModel
        {
            ContentType = Request.ContentType ?? "",
            Ip = Request.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "",
            Path = Request.Path,
            QueryString = Request.QueryString.Value,
            RequestMethod = Request.Method,
            DateTime = DateTime.UtcNow
        };


        await _requestsService.CreateAsync(model);
        var allLogs = await _requestsService.GetAsync();

        return View(allLogs);
    }

    [HttpGet("/json")]
    public async Task<IEnumerable<RequestLogModel>> GetJson()
    {
        var model = new RequestLogModel
        {
            ContentType = Request.ContentType ?? "",
            Ip = Request.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "",
            Path = Request.Path,
            QueryString = Request.QueryString.Value,
            RequestMethod = Request.Method,
            DateTime = DateTime.UtcNow
        };


        await _requestsService.CreateAsync(model);
        var allLogs = await _requestsService.GetAsync();

        return allLogs;
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
