using CRUD_DEMO.Data;
using CRUD_DEMO.Models;
using CRUD_DEMO.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace CRUD_DEMO.Controllers
{
    public class ProductsController : Controller
    {
        private readonly MVCDemoDbContext mvcDemoDbContext;

        public ProductsController(MVCDemoDbContext mvcDemoDbContext)
        {
            this.mvcDemoDbContext = mvcDemoDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await mvcDemoDbContext.Products.ToListAsync();
            return View(products);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddProductViewModel addProductRequest)
        {
            try
            {
                var isnameexists = await mvcDemoDbContext.Products.FirstOrDefaultAsync(x => x.Name == addProductRequest.Name);
                Console.Write(isnameexists);
                if (isnameexists != null)
                {
                    ViewBag.Message = "Exist";
                }
                var product = new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = addProductRequest.Name,
                    Price = addProductRequest.Price
                };
                await mvcDemoDbContext.Products.AddAsync(product);
                await mvcDemoDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
           
            catch (Exception ex)
            {
                Console.WriteLine("Error occured!", ex.Message);
            }

        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var product = await mvcDemoDbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            if(product != null)
            {
                var viewModel = new UpdateProductViewModel()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price
                };
                return await Task.Run(() => View("View", viewModel));
            }
            
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateProductViewModel model)
        {
            var product = await mvcDemoDbContext.Products.FindAsync(model.Id);

            if (product != null)
            {
                product.Name = model.Name;
                product.Price = model.Price;
                await mvcDemoDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateProductViewModel model)
        {
            var product = await mvcDemoDbContext.Products.FindAsync(model.Id);
            if(product != null)
            {
                mvcDemoDbContext.Products.Remove(product);
                await mvcDemoDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
