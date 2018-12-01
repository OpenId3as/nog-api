using NSubstitute;
using NUnit.Framework;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Collaborators;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories;
using OpenId3as.DivulgacaoONGs.Infra.Data.Context.Postgres;
using System;

namespace OpenId3as.DivulgacaoONGs.Infra.Data.Test
{
    [TestFixture]
    public class CollaboratorTests
    {
        private NOGContext context;
        private ICollaboratorRepository collaboratorRepository;

        [SetUp]
        public void Setup()
        {
            context = Substitute.For<NOGContext>();
            collaboratorRepository = Substitute.For<ICollaboratorRepository>();
        }

        [Test]
        public void Collaborators_InsertCollaborator_InsertedSuccessfully()
        {
            var newCollaborator = new Collaborator()
            {
                FirstName = "first",
                LastName = "last",
                Email = "test@test.com",
                Birth = DateTime.Now
            };

            var insertedCollaborator = new Collaborator()
            {
                Id = 1,
                FirstName = "first",
                LastName = "last",
                Email = "test@test.com",
                Birth = DateTime.Now
            };

            var result = collaboratorRepository.Add(newCollaborator).Returns(insertedCollaborator);

            Assert.NotNull(result, message: "The result is null");
            Assert.AreNotEqual(insertedCollaborator.Id, newCollaborator.Id, message: "The id is equal");
        }
    }
}