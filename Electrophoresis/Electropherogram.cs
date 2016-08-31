using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace Electrophoresis
{
	public class Electropherogram
	{
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

			for (var x = 0; x < data.GetLength(0); x++)
			{
				float saturation = 0;
				for (var y = 0; y < data.GetLength(1); y++)
				{
					saturation += data[x, y];
				}
			}

			image = new BitmapImage(new Uri(fileName));
		}
	}
}
