using System;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.Survey
{
    public class QuestionValidation : Entity<Guid>
    {
        [Indexed]
        public string ValidatorId { get; set; }
        [Indexed]
        public string ValidatorTypeId { get; set; }
        public int Revision { get; set; }
        public string MinLimit { get; set; }
        public string MaxLimit { get; set; }
        [Indexed]
        public Guid QuestionId { get; set; }

        public QuestionValidation()
        {
            Id = LiveGuid.NewGuid();
        }

        public override string ToString()
        {
            //$@"{ValidatorId}{ValidatorTypeId.ToLower().Equals("None".ToLower()) ? string.Empty : $",{ValidatorTypeId}")}";

            var mainInfo = $@"{ValidatorId}{(ValidatorTypeId.ToLower().Equals("None".ToLower()) ? "": $" | {ValidatorTypeId}")}";
            var minInfo = string.IsNullOrWhiteSpace(MinLimit) ? string.Empty : $"{MinLimit}";
            var maxInfo = string.IsNullOrWhiteSpace(MaxLimit) ? string.Empty : $"-{MaxLimit}";
            return $"{mainInfo} {minInfo} {maxInfo}  [{Revision}]";

        }
    }
}