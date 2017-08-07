using System;
using LiveHTS.Core.Model.Subject;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.ViewModel
{
    public class PartnerItem
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }

        public PartnerItem(ClientRelationship relationship)
        {
            Id = relationship.Id;
            FullName = relationship.Person.FullName;
            Gender = relationship.Person.Gender;
            BirthDate = relationship.Person.BirthDate.Value;
        }
    }
}