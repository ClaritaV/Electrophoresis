using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace Electrophoresis
{
	public class Electropherogram
	{
		public const double proteinSaturationLevel = 0.011;
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

			float saturationPrevious = 0, saturationCurrent;
			int xProteinStart = 0;
			for (var y = 0; y < data.GetLength(1); y++)
			{
				saturationPrevious += data[0, y];
			}

			for (var x = 1; x < data.GetLength(0); x++)
			{
				saturationCurrent = 0;
				for (var y = 0; y < data.GetLength(1); y++)
				{
					saturationCurrent += data[x, y];
				}
				if (saturationCurrent/data.GetLength(1) < proteinSaturationLevel &&
				    saturationPrevious/data.GetLength(1) > proteinSaturationLevel)
				{
					xProteinStart = x;
				}
				else if (saturationCurrent / data.GetLength(1) > proteinSaturationLevel &&
					saturationPrevious / data.GetLength(1) < proteinSaturationLevel)
				{
					Seeds.Add(new Seed(xProteinStart, x));
				}
				saturationPrevious = saturationCurrent;
			}

			image = new BitmapImage(new Uri(fileName));
		}
	}
}
