using AspXMLFIleParser.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.IO;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Data;
using System.Xml.Linq;
using System.Reflection.Metadata;

namespace AspXMLFIleParser.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public HomeController(IWebHostEnvironment webHost)
        {
            //_logger = logger;
            _hostingEnvironment = webHost;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Xml()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Xml(IFormFile xmlFile)
        {
            string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string fileName = Path.GetFileName(xmlFile.FileName);
            string fileSavePath = Path.Combine(uploadsFolder, fileName);

            using (var fileStream = new FileStream(fileSavePath, FileMode.Create))
            {
                await xmlFile.CopyToAsync(fileStream);
            }

            try
            {
                /*XDocument doc = XDocument.Load(fileSavePath);

                string ns = "{" + doc.Root.Name.NamespaceName + "}";

                var dataCollectionPlans = doc.Descendants(ns+"DataCollectionPlans")
                    .Select(dataCollectionPlans => new DataCollectionPlans
                    {
                        xmlns = (string)dataCollectionPlans.Attribute("xmlns") ?? string.Empty,
                        DataCollectionPlan = dataCollectionPlans.Elements(ns + "DataCollectionPlan")
                            .Select(dataCollectionPlan => new DataCollectionPlan
                            {
                                Id = (string)dataCollectionPlan.Attribute("id") ?? string.Empty,
                                Name = (string)dataCollectionPlan.Attribute("name") ?? string.Empty,
                                Description = (string)dataCollectionPlan.Element(ns + "Description") ?? string.Empty,
                                IntervalInMinutes = (int?)dataCollectionPlan.Attribute("intervalInMinutes") ?? 0,
                                IsPersistent = (bool?)dataCollectionPlan.Attribute("isPersistent") ?? false,
                                EventRequest = new EventRequest
                                {
                                    EventId = (string)dataCollectionPlan.Element(ns + "EventRequest")?.Attribute("eventId") ?? string.Empty,
                                    SourceId = (string)dataCollectionPlan.Element(ns + "EventRequest")?.Attribute("sourceId") ?? string.Empty,
                                    ParameterRequest = dataCollectionPlan.Element(ns + "EventRequest")?.Elements(ns + "ParameterRequest")
                                        .Select(parameterRequest => new ParameterRequest
                                        {
                                            ParameterName = (string)parameterRequest.Attribute("parameterName") ?? string.Empty,
                                            SourceId = (string)parameterRequest.Attribute("sourceId") ?? string.Empty
                                        }).ToList() ?? new List<ParameterRequest>()
                                }
                            }).ToList()
                    }).FirstOrDefault();*/

                var document = new XmlDocument();
                document.Load(fileSavePath);
                var nsManager = new XmlNamespaceManager(document.NameTable);
                var defaultNamespace = document.DocumentElement.NamespaceURI;
                nsManager.AddNamespace("ns", defaultNamespace);
                DataCollectionPlans dataCollectionPlans = new DataCollectionPlans();

                // Create an XmlSerializer with the correct type
                XmlSerializer serializer = new XmlSerializer(typeof(DataCollectionPlans));

                // Deserialize the XML into classes
                using (var reader = XmlReader.Create(fileSavePath))
                {
                    dataCollectionPlans = (DataCollectionPlans)serializer.Deserialize(reader);

                    // Access the parsed data here (e.g., dataCollectionPlans.Plans)
                    // ...
                }

                Debug.WriteLine(dataCollectionPlans);

                return View(dataCollectionPlans);
            }
            catch (Exception ex)
            {
                // Handle the exception, log the error, and return an appropriate response
                return Content("Error occurred: " + ex.Message);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
