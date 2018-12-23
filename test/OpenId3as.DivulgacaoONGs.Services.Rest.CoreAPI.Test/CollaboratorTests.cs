using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using OpenId3as.DivulgacaoONGs.Application.Interfaces;
using OpenId3as.DivulgacaoONGs.Application.ViewModels.Collaborators;
using OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI.Controllers;

namespace OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI.Test
{
    [TestFixture]
    public class CollaboratorTests
    {
        private ICollaboratorAppService _collaboratorAppService;

        [SetUp]
        public void SetUp()
        {
            _collaboratorAppService = Substitute.For<ICollaboratorAppService>();
        }

        [TestCase(1)]
        public void Collaborator_GetCollaboratorById_ReturnSuccessStatusWithCollaborator(long id)
        {
            _collaboratorAppService.GetById(id).Returns<CollaboratorViewModel>(new CollaboratorViewModel { });

            var collaborator = new CollaboratorController(null, _collaboratorAppService, null);
            var result = collaborator.Get(id);

            Assert.AreEqual(200, ((ObjectResult)result).StatusCode, message: "Status code is different of 200");
            Assert.IsNotNull(((ObjectResult)result).Value, message: "The search returned no value");
        }

        [TestCase(2)]
        public void Collaborator_GetCollaboratorById_ReturnSuccessStatusWithoutCollaborator(long id)
        {
            _collaboratorAppService.GetById(id).ReturnsNull();

            var collaborator = new CollaboratorController(null, _collaboratorAppService, null);
            var result = collaborator.Get(id);

            Assert.AreEqual(204, ((StatusCodeResult)result).StatusCode, message: "Status code is different of 204");
        }
    }
}
