namespace Elasticsearch.API.Exceptions
{
    public class NotFoundException:Exception
    {
        public NotFoundException(string type, object id)
        : base($"{type} türündeki {id} id değerine sahip olan obje bulunamadı! ") { }

    }
}
