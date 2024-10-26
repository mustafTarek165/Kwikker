
using System.Dynamic;


namespace Service.Contracts.Contracts
{
    public interface IDataShaper<T>
    {
        IEnumerable<ExpandoObject> ShapeData(IEnumerable<T> entities, string
        fieldsString);
        ExpandoObject ShapeData(T entity, string fieldsString);
    }
}
