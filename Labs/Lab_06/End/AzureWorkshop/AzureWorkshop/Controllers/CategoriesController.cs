using AzureWorkshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace AzureWorkshop.Controllers
{
	public class CategoriesController : Controller
	{
		private readonly NorthwindContext context;
		private readonly IConfiguration configuration;

		public CategoriesController(NorthwindContext context,
			IConfiguration configuration)
		{
			this.context = context;
			this.configuration = configuration;
		}

		public IActionResult Index()
		{
			return View(context.Categories);
		}

		public IActionResult Edit(int id)
		{
			var connectionString = configuration.GetConnectionString("ImageStore");
			var savedFiles = FileEntityHelper.GetSavedFilesInfo(connectionString)
				.Where(p => !string.IsNullOrEmpty(p.Url));

			var model = new CategoryEditViewModel
			{
				Category = context.Categories.Find(id),
				Pictures = new SelectList(savedFiles, "Url", "Name")
			};

			return View(model);
		}

		[HttpPost]
		public IActionResult Edit(Category category)
		{
			context.Update(category);
			context.SaveChanges();

			return RedirectToAction("Index");
		}

	}
}