using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;

namespace AzureWorkshop.Models
{
	public static class FileEntityHelper
	{
		public static IEnumerable<FileEntity> GetSavedFilesInfo(
			string connectionString)
		{
			var result = new List<FileEntity>();

			CloudStorageAccount account = null;
			CloudStorageAccount.TryParse(connectionString, out account);

			var tableClient = account.CreateCloudTableClient();
			var imageTable = tableClient.GetTableReference("images");

			var blobClient = account.CreateCloudBlobClient();
			var thumbnailsContainer = blobClient.GetContainerReference("thumbnails");

			var query = new TableQuery<FileEntity>()
				.Where("ClientAddress ne '::1'");

			TableContinuationToken token = null;
			do
			{
				var t = imageTable.ExecuteQuerySegmentedAsync(query, token).Result;

				t.Results.ForEach(i =>
				{
					var blob = thumbnailsContainer.GetBlobReference(i.RowKey);
					if (blob.ExistsAsync().Result)
					{
						i.Url = blob.Uri.ToString();
					}
				});

				result.AddRange(t.Results);
				token = t.ContinuationToken;

			} while (token != null);


			return result;
		}
	}
}
