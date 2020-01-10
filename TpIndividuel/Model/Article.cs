using System;
using System.Collections.Generic;
using System.Text;

namespace TpIndividuel.Model
{
	public class Article
	{
		public int Id { get; set; }

		public string Libelle { get; set; }
		public string SKU { get; set; }

		public DateTime DateSortie { get; set; }
		public double PrixInitial { get; set; }

		public double Poids { get; set; }

		public ICollection<PositionMagasin> EtagereIds { get; set; }

	}
}
