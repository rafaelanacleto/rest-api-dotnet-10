using RestApi.API.Services.Interfaces;

namespace RestApi.API.Services
{
    public class MathOperations : IMathOperations
    {
        public decimal Divide(string firstNum, string secondNum)
        {
            return decimal.Parse(firstNum) / decimal.Parse(secondNum);
        }

        public decimal Multiply(string firstNum, string secondNum)
        {
            return decimal.Parse(firstNum) * decimal.Parse(secondNum);
        }

        public decimal Subtract(string firstNum, string secondNum)
        {
            return decimal.Parse(firstNum) - decimal.Parse(secondNum);
        }

        public decimal Sum(string firstNum, string secondNum)
        {
            return decimal.Parse(firstNum) + decimal.Parse(secondNum);
        }
    }
}
