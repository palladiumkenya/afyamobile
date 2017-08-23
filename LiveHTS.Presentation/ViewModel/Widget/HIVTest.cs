using System.Collections.Generic;
using LiveHTS.Core.Model.Lookup;

namespace LiveHTS.Presentation.ViewModel.Widget
{
    public class HIVTest
    {
        public string TestName { get; set; }
        public List<Test> Tests { get; set; }
        public CategoryItem TestResult { get; set; }
    }
}