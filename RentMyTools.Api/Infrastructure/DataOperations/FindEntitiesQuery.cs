using System.Collections.Generic;
using System.Linq;

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