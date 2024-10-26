using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ExceptionModels
{
    public class CompositeKeyNotFoundException:NotFoundException
    {
        public CompositeKeyNotFoundException(int firstKey,int secondKey,string entity,string firstEntity,string secondEntity) 
            : base($"the {entity} with {firstEntity}id: {firstKey}  and {secondEntity}id: {secondKey}doesn't exist in database.")
        { }
    }
}
