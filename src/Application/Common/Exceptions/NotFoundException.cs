
namespace Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
     /// <summary>
     /// 
     /// </summary>
     /// <param name="entityName">Имя сущности, на которой вызвано исключение</param>
     /// <param name="id">Id сущности</param>
        public NotFoundException(string entityName, object id) 
            : base($"{entityName} Id={id} wasn't found")
        {

        }
    }
}
