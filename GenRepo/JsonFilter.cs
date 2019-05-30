using System;
using Newtonsoft.Json.Linq;

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
            var filterDto = JObject.Parse(_jsonData);

            return Build<T>(filterDto);
        }

        private Filter<T> Build<T>(JToken filter)
        {
            var property = filter["Property"];
            if (property != null)
                return BuildLeaf<T>(filter);
            var op = filter["LogicalOperator"];
            if (op != null)
            {
                var opValue = ParseEnum<LogicalOperator>(op.Value<string>());
                switch (opValue)
                {
                    case LogicalOperator.And:
                        return BuildAnd<T>(filter);
                    case LogicalOperator.Or:
                        return BuildOr<T>(filter);
                    default:
                        throw new ArgumentException($"Not supported operator {opValue}");
                }
            }
            return Filter<T>.Nothing;
        }

        private Filter<T> BuildOr<T>(JToken o)
        {
            var lhs = Build<T>(o["LHS"]);
            var rhs = Build<T>(o["RHS"]);
            return lhs.Or(rhs);
        }

        private Filter<T> BuildLeaf<T>(JToken o)
        {
            var dto = o.ToObject<LeafFilterExpression>();
            return Filter<T>.Create(dto.Property, dto.Operation, dto.Value);
        }

        private Filter<T> BuildAnd<T>(JToken o)
        {
            var lhs = Build<T>(o["LHS"]);
            var rhs = Build<T>(o["RHS"]);
            return lhs.And(rhs);
        }

        private static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }

    
}