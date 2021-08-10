using System;
using System.ComponentModel.DataAnnotations;

namespace ExerciceWebAPI.Models
{
    public class Follower
    {
        public Guid ID { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, MinimumLength = 3,
        ErrorMessage = "First Name should be minimum 3 characters and a maximum of 50 characters")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, MinimumLength = 3,
        ErrorMessage = "Last Name should be minimum 3 characters and a maximum of 50 characters")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }
    }
}
