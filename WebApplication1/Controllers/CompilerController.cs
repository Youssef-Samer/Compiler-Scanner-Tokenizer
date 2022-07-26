using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CompilerController : Controller
    {
        // GET: Home

        public ActionResult Index()
        {
            Scanner Index = new Scanner();
            return View(Index);
        }

        [HttpPost]
        public ActionResult Index(Scanner scanner)
        {
            string tokens = scanner.displayTokens();
            int numberOfErrors = scanner.retrieveErrorCounter();
            ViewData["ReturnedTokens"] = tokens;
            ViewData["NumberOfErrors"] = numberOfErrors;
            return View();
        }


























        /*public IActionResult Index()
        {
            Scanner obj = new Scanner(" \"float\" Loli Slow = ; $/ $$$+ \n 'h' 'kjnjdf' int ");
            String returnedTokens = obj.displayTokens();
            int numberOfErrors = obj.retrieveErrorCounter();

            ViewData["ReturnedTokens"] = returnedTokens;
            ViewData["NumberOfErrors"] = numberOfErrors;


            return View();

        <h4>Returned Tokens: </h4>
    <h5>@ViewData["ReturnedTokens"]</h5>
    <br>
    <h4>Number Of Errors: </h4>
    <h5>@ViewData["NumberOfErrors"]</h5>
        }*/
    }
}
