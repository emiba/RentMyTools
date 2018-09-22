using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Machine.Specifications;
using NSubstitute;
using Raven.Client.Documents.Session;
using RentMyTools.Api.Infrastructure.DataOperations;

namespace RentMyTools.Api.Tests.Infrastructure.DataOperations
{
    [Subject(typeof(SaveEntityCommand<TestEntity>))]
    public class When_saving_a_new_entity
    {
        private const string ExpectedString = "Something";

        private static IDocumentSession Session;
        private static SaveEntityCommand<TestEntity> Subject;
        private static TestEntity Entity;
        private static IMapper Mapper;

        private Establish context = () =>
        {
            Mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TestEntity, TestEntity>()
                    .ForMember(x => x.Id, o => o.Ignore());
            }));
            Entity = new TestEntity { AString = ExpectedString };
            Session = Substitute.For<IDocumentSession>();
            Subject = new SaveEntityCommand<TestEntity>
            {
                EntityToSave = Entity,
                Mapper = Mapper,
                Session = Session
            };
        };

        private Because of = () => Subject.Execute();

        private It should_have_created_a_new_entity = () => Subject.Result.Should().NotBe(Entity);

        private It should_update_the_properties = () => Subject.Result.AString.Should().Be(ExpectedString);
    }
}
