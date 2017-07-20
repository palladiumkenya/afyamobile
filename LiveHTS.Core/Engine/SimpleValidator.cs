using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Interfaces.Engine;
using LiveHTS.Core.Model.Interview;

namespace LiveHTS.Core.Engine
{
    public class SimpleValidator:IValidator
    {
        public bool Validate(Response response)
        {
            if (!response.Question.HasValidations)
                return true;

            var validations = response.Question.Validations;
            var vlist = new List<bool>();

            foreach (var validation in validations)
            {
                var result = validation.Evaluate(response);
                vlist.Add(result);
            }

            return vlist.All(x => x);
        }
    }
}