using System;
using LiveHTS.Core.Model.Survey;

namespace LiveHTS.Presentation.Events
{
    public class ConceptChangedEvent:EventArgs
    {
        public Concept Concept { get; }

        public ConceptChangedEvent(Concept concept)
        {
            Concept = concept;
        }
    }
}