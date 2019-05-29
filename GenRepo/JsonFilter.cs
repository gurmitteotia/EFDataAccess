using System;
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
            var filterDto = JsonConvert.DeserializeObject<TopFilter>(_jsonData);

            return Build<T>(filterDto);
        }

        private Filter<T> Build<T>(TopFilter filterDto)
        {
            if (filterDto.Operator==OperatorType.None)
                return GenRepo.Filter<T>.Nothing;
            switch (filterDto.Operator)
            {
                case OperatorType.None:
                    return GenRepo.Filter<T>.Nothing;
                case OperatorType.Unary:
                    return BuildUnary<T>(filterDto);
                case OperatorType.And:
                    return BuildAnd<T>(filterDto);
                case OperatorType.Or:
                    return BuildOr<T>(filterDto);
            }
            
            throw new ArgumentException("Unknown operator type");
        }

        private Filter<T> BuildOr<T>(TopFilter filterDto)
        {
            var lhs = Filter<T>(filterDto.LHS);
            var rhs = Filter<T>(filterDto.RHS);
            return lhs.Or(rhs);
        }

        private static Filter<T> Filter<T>(FilterDto filterDto)
        {
            return GenRepo.Filter<T>.Create(filterDto.Property, filterDto.Operation, filterDto.Value);
        }

        private Filter<T> BuildAnd<T>(TopFilter filterDto)
        {
            var lhs = Filter<T>(filterDto.LHS);
            var rhs = Filter<T>(filterDto.RHS);
            return lhs.And(rhs);
        }

        private Filter<T> BuildUnary<T>(TopFilter filterDto)
        {
            return Filter<T>(filterDto.LHS);
        }

        private class FilterDto
        {
            public string Property;
            [JsonConverter(typeof(StringEnumConverter))]
            public OperationType Operation;
            public object Value;
        }

        private class TopFilter
        {
            [JsonConverter(typeof(StringEnumConverter))]
            public OperatorType Operator;
            public FilterDto LHS;
            public FilterDto RHS;
        }

        private enum OperatorType
        {
            None,
            Unary,
            And,
            Or,
            
        }
    }

    
}