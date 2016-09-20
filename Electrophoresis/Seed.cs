using System.Collections.Generic;

namespace Electrophoresis
{
	public class Seed
	{
		public IList<Protein> Proteins
		{ get { return proteins; } }

		public int Left
		{ get { return left; } }

		public int Right
		{ get { return right; } }

		private readonly IList<Protein> proteins = new List<Protein>();
		private int left, right;

		public Seed(int left, int right)
		{
			this.left = left;
			this.right = right;
		}

		public void InflateSize()
		{
			left += InflateDelta;
			right -= InflateDelta;
		}

		public void RestoreSize()
		{
			left -= InflateDelta;
			right += InflateDelta;
		}

		public const int InflateDelta = 2;
	}
}
