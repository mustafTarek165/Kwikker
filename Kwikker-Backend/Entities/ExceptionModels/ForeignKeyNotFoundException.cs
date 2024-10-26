using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ExceptionModels
{
    public class ForeignKeyNotFoundException:Exception
    {
        public ForeignKeyNotFoundException(int foreignKey, string entity,string foreignEntity) : base($"the {entity} with {foreignEntity}id: {foreignKey} doesn't exist in database.")
        { }
    }
}
