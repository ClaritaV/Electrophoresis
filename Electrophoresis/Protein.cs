using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electrophoresis
{
	public class Protein
	{
		public int Top
		{ get { return top; } }

		public int Bottom
		{ get { return bottom; } }

		private readonly int top, bottom;

		public Protein(int top, int bottom)
		{
			this.top = top;
			this.bottom = bottom;
		}
	}
}
