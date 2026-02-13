using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.API.Utils
{
    public static class MathRest
    {
        public static decimal Divide(string firstNum, string secondNum)
        {
            return decimal.Parse(firstNum) / decimal.Parse(secondNum);
        }

        public static decimal Multiply(string firstNum, string secondNum)
        {
            return decimal.Parse(firstNum) * decimal.Parse(secondNum);
        }

        public static decimal Subtract(string firstNum, string secondNum)
        {
            return decimal.Parse(firstNum) - decimal.Parse(secondNum);
        }

        public static decimal Sum(string firstNum, string secondNum)
        {
            return decimal.Parse(firstNum) + decimal.Parse(secondNum);
        }
    }
}