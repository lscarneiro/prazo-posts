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
            JArray jarr = new JArray();
            foreach (var failure in Failures)
            {
                jarr.Add(new JObject(
                    new JProperty("PropertyName", failure.PropertyName),
                    new JProperty("ErrorMessage", failure.ErrorMessage)
                ));
            }
            return jarr.ToString();
        }
    }
}
