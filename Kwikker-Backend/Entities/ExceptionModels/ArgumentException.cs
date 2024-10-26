using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ExceptionModels
{
    public class ArgumentException:Exception
    {
        public ArgumentException(string? message) : base(message)
        {
        }

    }
}
