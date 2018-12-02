using NSubstitute;
using NUnit.Framework;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Collaborators;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories;

namespace OpenId3as.DivulgacaoONGs.Infra.Data.Test
{
    [TestFixture]
    public class CollaboratorTests
    {
        private ICollaboratorRepository _collaboratorRepository;

        [SetUp]
        public void Setup()
        {
            _collaboratorRepository = Substitute.For<ICollaboratorRepository>();
        }

        [TestCase(1)]
        public void Collaborator_SearchCollaborator_FoundSuccessfully(long id)
        {
            _collaboratorRepository.GetById(id).Returns<Collaborator>(new Collaborator { });
            var result = _collaboratorRepository.GetById(id);

            Assert.NotNull(result, message: "The result is null");
        }
    }
}