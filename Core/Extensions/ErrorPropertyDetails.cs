using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace Core.Extensions
{
    public class ErrorPropertyDetails
    {
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
    }

}
