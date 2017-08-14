using System;
using System.Linq;
using LiveHTS.Core.Interfaces.Model;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.ViewModel;

namespace LiveHTS.Presentation.DTO
{
    public class FormDTO
    {
        public Guid Id { get; set; }
        public  string Display { get; set; }

        public FormDTO()
        {
        }

        public FormDTO(Guid id, string display)
        {
            Id = id;
            Display = display;
        }
    }
}