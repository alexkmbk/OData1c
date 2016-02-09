using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

using System.Data.Services.Client;
using Microsoft.Data.OData;

using OData1c_console.TestBase;
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace OData1c.Controllers
{
    public class OData1c : Controller
    {

        // GET: /<controller>/
        public IActionResult Customers()
        {
            Uri uri = new Uri("http://localhost/OData/odata/standard.odata/");
            var container = new EnterpriseV8(uri);
  
            return View("_Customers", container.Catalog_Контрагенты.ToList());
        }

        // GET: /<controller>/
        public IActionResult Docs()
        {
            Uri uri = new Uri("http://localhost/OData/odata/standard.odata/");
            var container = new EnterpriseV8(uri);
 
            return View("_Docs", container.Document_АктВыполненныхРабот.ToList());
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            Uri uri = new Uri("http://localhost/OData/odata/standard.odata/");
            var container = new EnterpriseV8(uri);

            ViewData["Docs"] = container.Document_АктВыполненныхРабот.ToList();
            return View();
        }
    }
}


// Чтение данных документа АктВыполненныхРабот
/*var query = container.CreateQuery<Document_АктВыполненныхРабот>("Document_АктВыполненныхРабот");

IEnumerable<Document_АктВыполненныхРабот> docs;
docs = query.Execute();

List<Document_АктВыполненныхРабот> docslst = new List<Document_АктВыполненныхРабот>();
foreach (var e in docs)
{
    container.LoadProperty(e, "Контрагент");
    docslst.Add(e);
}*/
