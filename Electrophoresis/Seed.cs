using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electrophoresis
{
	public class Seed
	{
		public IList<Protein> Proteins
		{ get { return proteins; } }

		private readonly IList<Protein> proteins = new List<Protein>();

		public int Left { get; set; }
		public int Right { get; set; }
	}
}
