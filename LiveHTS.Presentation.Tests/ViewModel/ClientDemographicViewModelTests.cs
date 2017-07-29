using System;
using FizzWare.NBuilder;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.Presentation.ViewModel;
using NUnit.Framework;

namespace LiveHTS.Presentation.Tests.ViewModel
{
    [TestFixture]
    public class ClientDemographicViewModelTests
    {
        private IClientDemographicViewModel _viewModel;
        private ClientDemographicDTO _demographicDTO;

        [SetUp]
        public void SetUp()
        {
            _viewModel=new ClientDemographicViewModel(null);
            _demographicDTO = Builder<ClientDemographicDTO>.CreateNew().Build();
        }
        [Test]
        public void should_Validate_BirthDate()
        {
            _demographicDTO.BirthDate = null;
            _viewModel.ClientDemographicDTO = _demographicDTO;
            Assert.IsFalse(_viewModel.Validate());
            foreach (var error in _viewModel.Errors)
            {
                Console.WriteLine(error.ToString());
            }

            _demographicDTO.BirthDate = DateTime.Now.AddDays(1);
            _viewModel.ClientDemographicDTO = _demographicDTO;
            Assert.IsFalse(_viewModel.Validate());
            foreach (var error in _viewModel.Errors)
            {
                Console.WriteLine(error.ToString());
            }
        }

        [Test]
        public void should_Validate_Required()
        {
            _demographicDTO = Builder<ClientDemographicDTO>.CreateNew().Build();
            _demographicDTO.FirstName = " ";
            _demographicDTO.LastName = " ";
            _demographicDTO.Gender = " ";
            _viewModel.ClientDemographicDTO = _demographicDTO;
            Assert.IsFalse(_viewModel.Validate());
            foreach (var error in _viewModel.Errors)
            {
                Console.WriteLine(error.ToString());
            }
        }
    }
}
