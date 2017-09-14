using LiveHTS.Core.Model.Config;
using MvvmCross.Core.ViewModels;

namespace LiveHTS.Presentation.Interfaces.ViewModel
{
    public interface IPracticeViewModel
    {
        Practice Practice { get; set; }
        string Code { get; set; }
        string Name { get; set; }
        string PracticeTypeId { get; set; }
        int? CountyId { get; set; }
        IMvxCommand SearchPracticeCommand { get; }
        IMvxCommand SavePracticeCommand { get; }
    }
}