
using Microsoft.AspNetCore.Mvc;
using MyApp.Models;
using ChoETL;
using OfficeOpenXml;
 

public class HomeController : Controller
{
    private const string xmlFile = "countries.xml";
    
    public ActionResult Index()
    {
        IEnumerable<Root> countries = null;
        countries = GetCountries("all");
        
        return View(countries);
        
    }
    // Show flag
    [HttpGet("flag/{capital}")]
    public ActionResult Flag(string capital){
        
        ViewData["flag_path"] = capital.Replace('@','/');
        return View();
    }
    
 
    public IEnumerable<Root>  GetCountries(string path){
        IEnumerable<Root> countries = null;

        using (var client = new HttpClient())
        {
            // Base URL
            client.BaseAddress = new Uri("https://restcountries.com/v3.1/");
            
            var responseTask = client.GetAsync(path);
          
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Root>>();
                readTask.Wait();
                countries = readTask.Result;
            }
            else 
            {
                //log response status here..

                countries = Enumerable.Empty<Root>();

                ModelState.AddModelError(string.Empty, "Erro do servidor. Contacte o administrador");
            }
        }

        return countries;
    }
   
    // Gerar ficheiro XLS
    [HttpGet("xml")]
     public void GenerateXML(){
       
        IEnumerable<Root> countries = GetCountries("all");


        using (var parser = new ChoXmlWriter<Root>("files/countries.xml").WithXPath("Countries/Countrie"))
        {
            parser.Write(countries);
        }
        return;
    }


    // Gerar ficheiro CSV
    [HttpGet("csv")]
      public void GenerateCSV(string fileName="countries"){
         IEnumerable<Root> countries = GetCountries("all");
        fileName = fileName+".csv";
        using (var parser = new ChoCSVWriter<Root>("files/"+fileName))
        {
            parser.Write(countries);
        }
        return;

     }
     // Gerar ficheiro XLSX
     [HttpGet("xls")]
     public void GenerateXLS(){
        string csvFileNameTemp = "csvFileNameTemp";
        GenerateCSV(csvFileNameTemp);
         csvFileNameTemp = csvFileNameTemp+".csv";
        string xlsFileName = @"countries.xls";

        string worksheetsName = "COUNTRIES";

        bool firstRowIsHeader = false;

        var format = new ExcelTextFormat();
        format.Delimiter = ',';
        format.EOL = "\r";

        using (ExcelPackage package = new ExcelPackage(new FileInfo("files/"+xlsFileName)))
        {
           
            try {
                 ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(worksheetsName);
                 
                 worksheet.Cells["A1"].LoadFromText(new FileInfo("files/"+csvFileNameTemp), format, OfficeOpenXml.Table.TableStyles.Medium27, firstRowIsHeader);
                 package.Save();
                } catch (Exception e) {
                Console.Write("ola");
                } 
                finally {
                Console.Write("ola");
                }

        }
        System.IO.File.Delete("files/"+csvFileNameTemp);
        return;
     }

    

}


