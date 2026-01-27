using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VFHCatalogMVC.Domain.Common
{
    public class BaseEntity
    {
		protected BaseEntity(int id)
		{
			this.Id = id;
		}

		protected BaseEntity()
		{
			this.Id = 0;
		}

		[Column("id")]
		public int Id { get; set; }
	}
}
