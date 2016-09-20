using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace Electrophoresis
{
	public class Electropherogram
	{
		public const double brightnessLevel = 0.022;
		public const double proteinBrightnessLevel = 0.35;

		public BitmapImage Image
		{ get { return image; } }

		private readonly BitmapImage image;

		public IList<Seed> Seeds
		{ get { return seeds; } }

		private readonly IList<Seed> seeds = new List<Seed>();

		public IList<Protein> Proteins
		{ get { return proteins; } }

		private readonly IList<Protein> proteins = new List<Protein>();

		public Electropherogram(string fileName)
		{
			var data = BitmapHelper.LoadImage(fileName);

			int xSeedStart = 0;
			float sumDeltaPrevios = 0;
			for (var y = 1; y < data.GetLength(1); y++)
			{
				sumDeltaPrevios += Math.Abs(data[0, y] - data[0, y - 1]);
			}

			int heigth = data.GetLength(1) - 1;

			for (var x = 1; x < data.GetLength(0); x++)
			{
				float sumDelta = 0;
				for (var y = 1; y < data.GetLength(1); y++)
				{
					sumDelta += Math.Abs(data[x, y] - data[x, y - 1]);
				}

				if (sumDeltaPrevios / heigth < brightnessLevel && sumDelta / heigth > brightnessLevel) //previous is white row, current - with proteins
				{
					xSeedStart = x;
				}
				else if (sumDeltaPrevios / heigth > brightnessLevel && sumDelta / heigth < brightnessLevel) //previous row contains protein, current - does not
				{
					Seeds.Add(new Seed(xSeedStart, x));
				}
				sumDeltaPrevios = sumDelta;
			}

			foreach (var seed in Seeds)
			{
				// сужение столбца
				seed.InflateSize();
				
				// расчёт минимума и максимума яркости
				float minValue = float.MaxValue, maxValue = float.MinValue;
				var allValues = new List<float>(data.GetLength(1));
				for (int y = 0; y < data.GetLength(1); y++)
				{
					float value = 0;
					for (int x = seed.Left; x < seed.Right; x++)
					{
						value += data[x, y];
					}
					minValue = Math.Min(minValue, value);
					maxValue = Math.Max(maxValue, value);
					allValues.Add(value);
				}

				// нормирование значений
				for (int v = 0; v < allValues.Count; v++)
				{
					allValues[v] = (allValues[v] - minValue)/(maxValue - minValue);
				}

				int yProteinStart = 0;
				sumDeltaPrevios = allValues[0];
				for (int y = 1; y < data.GetLength(1); y++)
				{
					float sumDelta = allValues[y];

					if (sumDeltaPrevios > proteinBrightnessLevel && sumDelta < proteinBrightnessLevel)
					{
						yProteinStart = y;
					}
					else if (sumDeltaPrevios < proteinBrightnessLevel && sumDelta > proteinBrightnessLevel)
					{
						seed.Proteins.Add(new Protein(yProteinStart, y));
					}
					sumDeltaPrevios = sumDelta;
				}

				// сужение столбца
				seed.RestoreSize();
			}

			image = new BitmapImage(new Uri(fileName));
		}
	}
}
