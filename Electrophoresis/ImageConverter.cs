using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace Electrophoresis
{
	public static class ImageConverter
	{
		public static System.Windows.Media.ImageSource ToSource(this Image image)
		{
			var bitmap = new BitmapImage();
			bitmap.BeginInit();
			var memoryStream = new MemoryStream();
			image.Save(memoryStream, image.RawFormat);
			memoryStream.Seek(0, SeekOrigin.Begin);
			bitmap.StreamSource = memoryStream;
			bitmap.EndInit();
			return bitmap;
		}
	}
}
