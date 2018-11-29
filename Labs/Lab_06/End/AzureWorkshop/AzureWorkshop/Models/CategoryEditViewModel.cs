using Microsoft.AspNetCore.Mvc.Rendering;

namespace AzureWorkshop.Models
{
	public class CategoryEditViewModel
	{
		public Category Category { get; set; }
		public SelectList Pictures { get; set; }
	}
}
