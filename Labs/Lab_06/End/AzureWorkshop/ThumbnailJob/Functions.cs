using Microsoft.Azure.WebJobs;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.IO;

namespace ThumbnailJob
{
	public class Functions
	{
		public static void ConvertImage(
			[QueueTrigger("imgprocessing")] string blobId,
			[Blob("images/{queueTrigger}", FileAccess.Read)] Stream inputStream,
			[Blob("thumbnails/{queueTrigger}", FileAccess.Write)] Stream outputStream)
		{
			using (Image<Rgba32> image = Image.Load(inputStream))
			{
				image.Mutate(i =>
					i.Resize(100, 100)
				);
				image.Save(outputStream, new PngEncoder());
			}
		}
	}
}