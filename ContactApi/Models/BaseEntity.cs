using System;
using System.ComponentModel.DataAnnotations;

namespace ContactApi.Models
{
	// This class can be reused for all the other entities
    public class BaseEntity
    {
        [Key]
		[Required]
		public int Id { get; set; }

		public DateTime CreatedDateTime { get; set; }

		public DateTime LastUpdatedDateTime { get; set; }
    }
}