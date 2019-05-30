using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GenRepo
{
    public class FilterExpression
    {
    }
    public class LeafFilterExpression : FilterExpression
    {
        public string Property;
        [JsonConverter(typeof(StringEnumConverter))]
        public OperationType Operation;
        public object Value;
    }

    public class CompositeFilterExpression : FilterExpression
    {
        public FilterExpression LHS;
        public FilterExpression RHS;
        [JsonConverter(typeof(StringEnumConverter))]
        public LogicalOperator LogicalOperator;
    }
    public enum LogicalOperator
    {
        None,
        And,
        Or,
    }

    public static class FilterExpressionExtension
    {
        public static string Json(this FilterExpression expression)
        {
            return JsonConvert.SerializeObject(expression);
        }
    }
}