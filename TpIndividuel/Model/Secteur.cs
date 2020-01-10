using System;
using System.Collections.Generic;
using System.Text;

namespace TpIndividuel.Model
{
	public class Secteur
	{
		public int Id { get; set; }

		public string Nom { get; set; }

		public ICollection<Etagere> ReferalsEtagere { get; set; }


	}
}
