using RentMyTools.Models;

namespace RentMyTools.Api.Infrastructure.DataOperations
{
    public class GetEntityByIdQuery<T> : Query<T> where T : Entity
    {
        public string Id { get; set; }

        public override void Execute()
        {
            Result = Session.Load<T>(Id);
        }
    }
}