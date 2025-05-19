using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceException
{
    public class InvalidLoginOrPasswordException : BusinessRuleException
    {
        private const string ErrorCode = "InvalidLoginOrPassword";

        public InvalidLoginOrPasswordException(string message)
            : base(message, ErrorCode)
        {
        }
    }
}
