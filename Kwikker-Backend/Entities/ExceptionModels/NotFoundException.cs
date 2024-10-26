

namespace Entities.ExceptionModels
{
    public  class NotFoundException:Exception
    {
        public NotFoundException(string? message) : base(message)
        {
        }

        public NotFoundException(int id, string entity) : base($"the {entity} with id: {id} doesn't exist in database.")
        { }
    }
}
