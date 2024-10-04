using DIDemo.Interfaces;
using DIDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Json;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ISingletonService singletonService;
    private readonly ISingletonService singletonService2;
    private readonly IScopedService scopedService;
    private readonly IScopedService scopedService2;
    private readonly ITransientService transientService;
    private readonly ITransientService transientService2;
    private readonly string dataFilePath = "../data.json"; 

    public RequestData requestData { get; set; } = new RequestData();

    public HomeController(ILogger<HomeController> logger, IScopedService scopedService, ITransientService transientService, ISingletonService singletonService,
        IScopedService scopedService2, ITransientService transientService2, ISingletonService singletonService2)
    {
        _logger = logger;
        this.scopedService = scopedService;
        this.transientService = transientService;
        this.singletonService = singletonService;
        this.scopedService2 = scopedService2;
        this.transientService2 = transientService2;
        this.singletonService2 = singletonService2;

        // Завантаження даних з файлу
        requestData = LoadDataFromFile(dataFilePath);
    }

    public IActionResult Index()
    {
        int scop1 = scopedService.ShareNumber();
        int scop2 = scopedService2.ShareNumber();

        int sin1 = singletonService.ShareNumber();
        int sin2 = singletonService2.ShareNumber();

        int tr1 = transientService.ShareNumber();
        int tr2 = transientService2.ShareNumber();

        if (requestData.req1.Count == 0 && requestData.req2.Count == 0)
        {
            requestData.req1.Add("scop1", scop1);
            requestData.req1.Add("scop2", scop2);
            requestData.req1.Add("sin1", sin1);
            requestData.req1.Add("sin2", sin2);
            requestData.req1.Add("tr1", tr1);
            requestData.req1.Add("tr2", tr2);
        }
        else if (requestData.req1.Count > 0 && requestData.req2.Count == 0)
        {
            requestData.req2.Add("scop1", scop1);
            requestData.req2.Add("scop2", scop2);
            requestData.req2.Add("sin1", sin1);
            requestData.req2.Add("sin2", sin2);
            requestData.req2.Add("tr1", tr1);
            requestData.req2.Add("tr2", tr2);
        }
        else
        {
            requestData.req1.Clear();

            foreach (var kvp in requestData.req2)
            {
                requestData.req1.Add(kvp.Key, kvp.Value);
            }

            requestData.req2.Clear();
            requestData.req2.Add("scop1", scop1);
            requestData.req2.Add("scop2", scop2);
            requestData.req2.Add("sin1", sin1);
            requestData.req2.Add("sin2", sin2);
            requestData.req2.Add("tr1", tr1);
            requestData.req2.Add("tr2", tr2);
        }

        SaveDataToFile(requestData, dataFilePath);

        ViewBag.req1 = requestData.req1;
        ViewBag.req2 = requestData.req2;
        return View();
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

    private void SaveDataToFile(RequestData data, string filePath)
    {
        var jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
        System.IO.File.WriteAllText(filePath, jsonData);
    }

    private RequestData LoadDataFromFile(string filePath)
    {
        if (System.IO.File.Exists(filePath))
        {
            var jsonData = System.IO.File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<RequestData>(jsonData);
        }
        return new RequestData();
    }
}

public class RequestData
{
    public Dictionary<string, int> req1 { get; set; } = new Dictionary<string, int>();
    public Dictionary<string, int> req2 { get; set; } = new Dictionary<string, int>();
}
