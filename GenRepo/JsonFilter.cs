using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GenRepo
{
    public class JsonFilter
    {
        private readonly string _jsonData;

        public JsonFilter(string jsonData)
        {
            _jsonData = jsonData;
        }

        public Filter<T> Instance<T>()
        {
            var filterDto = JsonConvert.DeserializeObject<FilterDto>(_jsonData);

            return Build<T>(filterDto);
        }

        private Filter<T> Build<T>(FilterDto filterDto)
        {
            if (string.IsNullOrEmpty(filterDto.Property))
                return Filter<T>.Nothing;

            return Filter<T>.Create(filterDto.Property, filterDto.Operation, filterDto.Value);
        }

        private class FilterDto
        {
            public string Property;
            [JsonConverter(typeof(StringEnumConverter))]
            public OperationType Operation;
            public object Value;
        }
    }

    
}