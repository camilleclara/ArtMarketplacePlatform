namespace MarketPlaceException
{
    public class BusinessRuleException: Exception
    {
        public string errorCode;
        public BusinessRuleException(string message, string errCode) : base(message)
        {
            errorCode = errCode;
        }
    }
}
