using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactApi.Models
{
    public class Contact : BaseEntity
    {

		public string FirstName { get; set; }
		public string LastName { get; set; }

		[Required]
		[DataType(DataType.EmailAddress)]
		[Column(TypeName = "varchar(255)")]
		public string Email { get; set; }

		[Column(TypeName = "varchar(255)")]
		public string PhoneNumber { get; set; }
		public bool IsActive { get; set; }
    }
}