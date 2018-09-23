using System;
using RentMyTools.Models;

namespace RentMyTools.Api.Infrastructure.DataOperations
{
    public class DeleteEntityCommand<T> : Command where T : Entity
    {
        public string Id { get; set; }

        public override void Execute()
        {
            var entityToDelete = Session.Load<T>(Id);
            if (entityToDelete == null)
                throw new ArgumentOutOfRangeException(nameof(Id), Id, $"No entity with the given id found.");

            Session.Delete(entityToDelete);
        }
    }
}