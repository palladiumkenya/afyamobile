using System;
using System.Configuration;
using Cheesebaron.MvxPlugins.Settings.Interfaces;
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
            _viewModel=new ClientDemographicViewModel(null,null);
            _demographicDTO = Builder<ClientDemographicDTO>.CreateNew().Build();
            _viewModel.FirstName = _demographicDTO.FirstName;
            _viewModel.MiddleName = _demographicDTO.MiddleName;
            _viewModel.LastName = _demographicDTO.LastName;
            
            _viewModel.BirthDate = _demographicDTO.BirthDate;
        }
        [Test]
        public void should_Validate_BirthDate()
        {
            _demographicDTO.BirthDate = null;
            Assert.IsFalse(_viewModel.Validate());
            foreach (var error in _viewModel.Errors)
            {
                Console.WriteLine(error.ToString());
            }

            _demographicDTO.BirthDate = DateTime.Now.AddDays(1);
            Assert.IsFalse(_viewModel.Validate());
            foreach (var error in _viewModel.Errors)
            {
                Console.WriteLine(error.ToString());
            }
        }

        [Test]
        public void should_Validate_Age()
        {
            _viewModel.Age = 0;
            Assert.IsFalse(_viewModel.Validate());
            foreach (var error in _viewModel.Errors)
            {
                Console.WriteLine(error.ToString());
            }

            _viewModel.Age = -1;
            Assert.IsFalse(_viewModel.Validate());
            foreach (var error in _viewModel.Errors)
            {
                Console.WriteLine(error.ToString());
            }
        }

        [Test]
        public void should_Validate_Required()
        {

            _viewModel.FirstName = " ";
            _viewModel.LastName = " ";
            
            
            Assert.IsFalse(_viewModel.Validate());
            foreach (var error in _viewModel.Errors)
            {
                Console.WriteLine(error.ToString());
            }
        }
    }
}
