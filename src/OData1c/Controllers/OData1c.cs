using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

using System.Data.Services.Client;
using Microsoft.Data.OData;

using OData1c_console.TestBase;


namespace OData1c.Controllers
{
    public class OData1c : Controller
    {
        // Получает данные справочника "Контрагенты"
        // GET: /<controller>/
        public IActionResult Customers()
        {
            Uri uri = new Uri("http://localhost/OData/odata/standard.odata/");
            var container = new EnterpriseV8(uri);
  
            return PartialView("_Customers", container.Catalog_Контрагенты.ToList());
        }

        // Получает данные документов "Акты выполненных работ"
        // GET: /<controller>/
        public IActionResult Docs()
        {
            Uri uri = new Uri("http://localhost/OData/odata/standard.odata/");
            var container = new EnterpriseV8(uri);
 
            return PartialView("_Docs", container.Document_АктВыполненныхРабот.ToList());
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        // Обновляет данные в справочинке "Контрагенты" (запись данных не реализована пока)
        [HttpPost]
        public IActionResult CustomersUpdate(string CustomerId, string CustomerName, string CustomerType)
        {
            Uri uri = new Uri("http://localhost/OData/odata/standard.odata/");
            var container = new EnterpriseV8(uri);
            Catalog_Контрагенты Contr = new Catalog_Контрагенты();
            Contr.Ref_Key = new Guid(CustomerId);
            Contr.Description = CustomerName;
            Contr.Вид = CustomerType;
            try
            {
                container.AttachTo("Catalog_Контрагенты", Contr);
                container.UpdateObject(Contr);
                //container.SaveChanges(); Почему то в MVC провекте этот метод не доступен, поэтмоу ничего не работает
            }
            catch (Exception e)
            {
                return Json(new { isOk = false,errors = e.Message});
            }
            return Json(new {isOk=true});
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
