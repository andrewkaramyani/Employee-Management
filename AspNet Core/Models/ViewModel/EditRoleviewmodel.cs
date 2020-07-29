using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet_Core.Models.ViewModel
{
    public class EditRoleviewmodel
    {

        public EditRoleviewmodel()
        {
            Users = new List<string>();
        }
        public string Id { get; set; }
        [Required(ErrorMessage ="Role Name Is required")]
        public string RoleName { get; set; }

        public List<string> Users { get; set; }
    }
}
