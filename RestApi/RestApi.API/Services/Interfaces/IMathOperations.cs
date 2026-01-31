namespace RestApi.API.Services.Interfaces
{
    public interface IMathOperations
    {
        decimal Sum(string firstNum, string secondNum);
        decimal Subtract(string firstNum, string secondNum);
        decimal Multiply(string firstNum, string secondNum);
        decimal Divide(string firstNum, string secondNum);

    }
}
