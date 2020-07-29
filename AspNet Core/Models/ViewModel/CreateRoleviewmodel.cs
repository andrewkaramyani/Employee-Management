using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet_Core.Models.ViewModel
{
    public class CreateRoleviewmodel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
