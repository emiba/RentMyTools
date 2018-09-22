using System;
using AutoMapper;
using Raven.Client.Documents;

namespace RentMyTools.Api.Infrastructure.DataOperations
{
    public class RavenDBDataOperationExecutor : IDataOperationExecutor
    {
        private readonly IDocumentStore _store;
        private readonly IMapper _mapper;

        public RavenDBDataOperationExecutor(IDocumentStore store, IMapper mapper)
        {
            _store = store;
            _mapper = mapper;
        }

        public void Execute(Command command)
        {
            using(var session = _store.OpenSession())
            {
                command.Session = session;
                AddMapperIfNeeded(command);
                command.Execute();
                session.SaveChanges();
            }
        }

        private void AddMapperIfNeeded(Command command)
        {
            if (!(command is INeedMapper needMapper))
                return;

            needMapper.Mapper = _mapper;
        }

        public T Execute<T>(Command<T> command)
        {
            Execute(command as Command);

            return command.Result;
        }

        public T Execute<T>(Query<T> query)
        {
            using(var session = _store.OpenSession())
            {
                query.Session = session;
                AddMapperIfNeeded(query);
                query.Execute();
                return query.Result;
            }
        }

        private void AddMapperIfNeeded<T>(Query<T> query)
        {
            if (!(query is INeedMapper needMapper))
                return;

            needMapper.Mapper = _mapper;
        }
    }
}