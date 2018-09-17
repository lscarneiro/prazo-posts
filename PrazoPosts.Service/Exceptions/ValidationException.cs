using System;
using System.Collections.Generic;
using FluentValidation.Results;
using Newtonsoft.Json.Linq;

namespace PrazoPosts.Service.Exceptions
{
    public class ValidationException : Exception
    {
        public IList<ValidationFailure> Failures { get; }
        public ValidationException(IList<ValidationFailure> failures)
        {
            Failures = failures;
        }

        public string ToJson()
        {
            JObject jerr = new JObject();
            foreach (var failure in Failures)
            {
                jerr.Add(failure.PropertyName, failure.ErrorMessage);
            }
            JObject jobj = new JObject(new JProperty("validationErrors", jerr));
            return jobj.ToString();
        }
    }
}
