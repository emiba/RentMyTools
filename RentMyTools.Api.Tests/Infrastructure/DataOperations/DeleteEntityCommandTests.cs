using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Machine.Specifications;
using NSubstitute;
using Raven.Client.Documents.Session;
using RentMyTools.Api.Infrastructure.DataOperations;
using RentMyTools.Api.Models;

namespace RentMyTools.Api.Tests.Infrastructure.DataOperations
{
    [Subject(typeof(DeleteEntityCommand<TestEntity>))]
    public class When_deleting_non_existing_id
    {
        private static DeleteEntityCommand<TestEntity> Subject;
        private static IDocumentSession Session;
        private static Exception Exception;

        private Establish context = () =>
        {
            Session = Substitute.For<IDocumentSession>();
            Subject = new DeleteEntityCommand<TestEntity> {Id = "1234", Session = Session};
        };

        private Because of = () => Exception = Catch.Exception(() => Subject.Execute());

        private It should_throw_argumentoutofrangeexception =
            () => Exception.Should().BeOfType<ArgumentOutOfRangeException>();
    }

    public class TestEntity : Entity
    { }
}
