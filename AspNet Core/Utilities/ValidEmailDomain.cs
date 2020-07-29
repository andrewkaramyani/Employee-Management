using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet_Core.Utilities
{
    public class ValidEmailDomain : ValidationAttribute
    {
        private readonly string allowDomain;

        public ValidEmailDomain(string allowDomain)
        {
            this.allowDomain = allowDomain;
        }
        public override bool IsValid(object value)
        {
            string[] strings = value.ToString().Split("@");
            return strings[1].ToUpper() == allowDomain.ToUpper();
        }
    }
}
