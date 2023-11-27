using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace Core.Extensions
{
    public class ErrorDetails
    {
        public string Message { get; set; }
        public bool Status { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class ValidationErrorDetail : ErrorDetails
    {
        public List<ErrorPropertyDetails> Errors { get; set; }

    }
}
