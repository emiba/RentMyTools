using System;
using RentMyTools.Api.Models;

namespace RentMyTools.Api.Infrastructure.DataOperations
{
    public class SaveEntityCommand<T> : Command<T> where T : Entity, new()
    {
        public T EntityToSave { get; set; }

        public Action<T,T> Mapping { get; set; }

        public override void Execute()
        {
            var entity = PrimeEntity();
            Mapping(EntityToSave, entity);
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