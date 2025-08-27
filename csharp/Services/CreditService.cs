namespace Camunda8Training.Services;

public class CreditService {
    public double GetCustomerCredit(string? customerId) {
        var customerCredit = Convert.ToDouble(customerId?.Substring(customerId.Length - 2, 2));
        return customerCredit;
    }

    public double DeductCredit(double customerCredit, double amountToDeduct) {
        return amountToDeduct > customerCredit ? amountToDeduct - customerCredit : 0.0;
    }
}