using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Services.Client;
using Microsoft.Data.OData;
using OData1c_console.OData1c;

namespace OData1c_console
{
    class Program
    {
        static void Main(string[] args)
        {
           
            Uri uri = new Uri("http://localhost/OData/odata/standard.odata/");
            var container = new OData1c.EnterpriseV8(uri);

            /* Catalog_Контрагенты contr = new Catalog_Контрагенты();
             contr.Description = "Иванов Иван Иванович";
             contr.Вид = "ЮрЛицо";
             container.AddToCatalog_Контрагенты(contr);

             DataServiceResponse response = container.SaveChanges();

             // Enumerate the returned responses.
             foreach (ChangeOperationResponse change in response)
             {
                 // Get the descriptor for the entity.
                 EntityDescriptor descriptor = change.Descriptor as EntityDescriptor;

                 if (descriptor != null)
                 {
                     Catalog_Контрагенты addedProduct = descriptor.Entity as Catalog_Контрагенты;

                     if (addedProduct != null)
                     {
                         Console.WriteLine("New контрагент added with ID {0}.",
                             addedProduct.Ref_Key);
                     }
                 }
             }*/

            //var query = container.CreateQuery<Catalog_Контрагенты>("Catalog_Контрагенты");

            /*container.SendingRequest2 += (s, e) =>
            {
                Console.WriteLine("{0} {1}", e.RequestMessage.Method, e.RequestMessage.Url);
            };*/

            //var Catalog_Контрагенты = query.Execute();//.Where(p => p.Вид == "ФизЛицо");

            //Console.WriteLine(Catalog_Контрагенты.Description);

            // Запись данных документа АктВыполненныхРабот
            Document_АктВыполненныхРабот doc = new Document_АктВыполненныхРабот();
            doc.Контрагент = container.Catalog_Контрагенты.Where(p => p.Description == "Контрагент 2").First();
            doc.Работы.Add(new Document_АктВыполненныхРабот_Работы_RowType{Описание="привет из .NET", Сумма=150000, LineNumber=1});
            doc.Date = DateTime.Now;
            doc.Posted = true;
            doc.Контрагент_Key = doc.Контрагент.Ref_Key;
            container.AddToDocument_АктВыполненныхРабот(doc);
            container.SaveChanges();

            // Чтение данных документа АктВыполненныхРабот
            var query = container.CreateQuery<Document_АктВыполненныхРабот>("Document_АктВыполненныхРабот");

            IEnumerable<Document_АктВыполненныхРабот> docs;
            try
            {
                docs = query.Execute();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.Read();
                return;
            }

            //Document_АктВыполненныхРабот doc = null;

            // Вывод данных документа
            foreach (var e in docs)
            {
                /*  if (doc == null)
                  {
                      container.LoadProperty(e, "");
                  }*/
                container.LoadProperty(e, "Контрагент");
                Console.WriteLine($"документ № {e.Number} от {e.Date}, контрагент {e.Контрагент.Description}");
                Console.WriteLine("работы: ");
                foreach (var line in e.Работы)
                {
                    Console.WriteLine($"Описание: {line.Описание}, сумма: {line.Сумма}");
                }

            }


            Console.Read();

        }
    }
}
