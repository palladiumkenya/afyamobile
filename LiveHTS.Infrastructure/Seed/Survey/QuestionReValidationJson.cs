using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed.Survey
{
  public  class QuestionReValidationJson : ISeedJson<QuestionReValidation>
    {
        public  List<QuestionReValidation> Read()
        {
            string raw = @"";
            if(string.IsNullOrWhiteSpace(raw))
                return new List<QuestionReValidation>();

            return JsonConvert.DeserializeObject<List<QuestionReValidation>>(raw.Replace("^","\""));
        }
    }
}
