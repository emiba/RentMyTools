using System;
using AutoMapper;
using RentMyTools.Api.Models;

namespace RentMyTools.Api.Infrastructure.DataOperations
{
    public class SaveEntityCommand<T> : Command<T>, INeedMapper where T : Entity, new()
    {
        public IMapper Mapper { get; set; }
        public T EntityToSave { get; set; }

        public override void Execute()
        {
            var entity = PrimeEntity();
            Mapper.Map(EntityToSave, entity);
            Result = entity;
        }

        private T PrimeEntity()
        {
            if (string.IsNullOrEmpty(EntityToSave.Id))
                return CreateEntity();

            return Session.Load<T>(EntityToSave.Id) ?? CreateEntity();
        }

        private T CreateEntity()
        {
            var entity = new T();
            Session.Store(entity);
            
            return entity;
        }
    }
}