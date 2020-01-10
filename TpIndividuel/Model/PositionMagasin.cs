using System;
using System.Collections.Generic;
using System.Text;

namespace TpIndividuel.Model
{
	public class PositionMagasin
	{
		public int IdArticle { get; set; }
		public Article Article { get; set; }
		public int IdEtagere { get; set; }
		public Etagere Etagere { get; set; }
		public int Quantite { get; set; }

	}
}
