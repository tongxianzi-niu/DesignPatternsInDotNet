using SOLIDDesignPrinciples;


#region Open/Closed Principle Demo
Payment pmt1 = new Payment(PaymentMode.Cash, 10.00M, Category.Food, "KFC");
Payment pmt2 = new Payment(PaymentMode.Giro, 20.00M, Category.Food, "Old Hen");
Payment pmt3 = new Payment(PaymentMode.Cash, 5.50M, Category.Stationary, "Pens");
List<Payment> paymentList = new List<Payment>() { pmt1, pmt2, pmt3 };

// old method
PaymentFilter paymentFilter = new PaymentFilter();
Console.WriteLine($"Old Method filter by category");
foreach (Payment item in paymentFilter.FilterByCategory(paymentList, Category.Food))
{
    Console.WriteLine($"{item.Remarks}: {item.Amount}");
}

Console.WriteLine($"Old Method filter by payment mode");
foreach (Payment item in paymentFilter.FilterByPaymentMode(paymentList, PaymentMode.Cash))
{
    Console.WriteLine($"{item.Remarks}: {item.Amount}");
}

// new method
Console.WriteLine($"Better Method filter by category");
BetterPaymentFilter betterFilter = new BetterPaymentFilter();
foreach (Payment item in betterFilter.Filter(paymentList, new CategoryCriteria(Category.Food)))
{
    Console.WriteLine($"{item.Remarks}: {item.Amount}");
}

Console.WriteLine($"Better Method filter by payment mode");
foreach (Payment item in betterFilter.Filter(paymentList, new PaymentModeCriteria(PaymentMode.Cash)))
{
    Console.WriteLine($"{item.Remarks}: {item.Amount}");
}


Console.WriteLine($"Better Method filter by payment mode and category");
foreach (Payment item in betterFilter.Filter(paymentList, new AndFilterCriteria<Payment>(new PaymentModeCriteria(PaymentMode.Cash), new CategoryCriteria(Category.Stationary))))
{
    Console.WriteLine($"{item.Remarks}: {item.Amount}");
}

#endregion