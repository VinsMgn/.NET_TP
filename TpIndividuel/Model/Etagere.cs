using System;
using System.Collections.Generic;
using System.Text;

namespace TpIndividuel.Model
{
	public class Etagere
	{
		public int Id { get; set; }
		public double PoidsMaximum { get; set; }
		public Secteur ReferentSecteur { get; set; }
		public int? IdSecteur { get; set; }
		public ICollection<PositionMagasin> ArticleIds { get; set; }


	}
}
