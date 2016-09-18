using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace Electrophoresis
{
	public class Electropherogram
	{
		public const double brightnessLevel = 0.022;//4 / 196

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

			int xProteinStart = 0;
			float sumDeltaPrevios = 0;
			for (var y = 1; y < data.GetLength(1); y++)
			{
				sumDeltaPrevios += Math.Abs(data[0, y] - data[0, y - 1]);
			}

			for (var x = 1; x < data.GetLength(0); x++)
			{
				float sumDelta = 0;
				int heigth = data.GetLength(1) - 1;
				for (var y = 1; y < data.GetLength(1); y++)
				{
					sumDelta += Math.Abs(data[x, y] - data[x, y - 1]);
				}

				if (sumDeltaPrevios / heigth < brightnessLevel && sumDelta / heigth > brightnessLevel) //previous is white row, current - with proteins
				{
					xProteinStart = x;
				}
				else if (sumDeltaPrevios / heigth > brightnessLevel && sumDelta / heigth < brightnessLevel) //previous row contains protein, current - does not
				{
					Seeds.Add(new Seed(xProteinStart, x));
				}
				sumDeltaPrevios = sumDelta;
			}

			image = new BitmapImage(new Uri(fileName));
		}
	}
}
