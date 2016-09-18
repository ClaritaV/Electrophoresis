using System.Drawing;

namespace Electrophoresis
{
	public static class BitmapHelper
	{
		public static float[,] LoadImage(string fileName)
		{
			// загрузка изображения
			using (var bitmap = new Bitmap(fileName))
			{
				// создание массива данных и копирование в него пикселей
				var data = new float[bitmap.Width, bitmap.Height];
				for (int x = 0; x < bitmap.Width; x++)
					for (int y = 0; y < bitmap.Height; y++)
					{
						var color = bitmap.GetPixel(x, y);
						data[x, y] = color.GetBrightness();
					}
				// возврат результата загрузки
				return data;
			}
		}
	}
}
