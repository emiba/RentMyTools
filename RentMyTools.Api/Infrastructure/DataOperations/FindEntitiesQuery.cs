using System.Collections.Generic;
using System.Linq;
using RentMyTools.Api.Models;

namespace RentMyTools.Api.Infrastructure.DataOperations
{
    public class FindEntitiesQuery<T> : Query<IEnumerable<T>>
    {
        public override void Execute()
        {
            Result = Session.Query<T>().ToArray();
        }
    }
}