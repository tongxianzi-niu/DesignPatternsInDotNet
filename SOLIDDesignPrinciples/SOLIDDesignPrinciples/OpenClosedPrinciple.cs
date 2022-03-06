using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDDesignPrinciples
{
    public class OpenClosedPrinciple
    {
        // Open/Closed Principle
        // Software entities (classes, modules, functions, etc.) should be open for extension, but closed for modification
    }

    public enum PaymentMode
    {
        Cheque,
        Cash,
        EFT,
        Giro
    }
    public enum Category
    {
        Food,
        Drink,
        Stationary,
        Travel,
        Fashion
    }
    public class Payment
    {
        public Guid Id = Guid.NewGuid();
        public DateTime PaymentDate = DateTime.Today;
        public PaymentMode PaymentMode;
        public decimal Amount;
        public Category Category;
        public string Remarks;

        public Payment(PaymentMode paymentMode, decimal amount, Category category, string remarks)
        {
            PaymentMode = paymentMode;
            Amount = amount;
            Category = category;
            Remarks = remarks;
        }
    }

    #region old method - not follow Open/Close Principle
    // this is not good because everytime when there is new filter criteria, the PaymentFilter class will be modified
    public class PaymentFilter
    {
        // we should not put this method inside Payment Class due to Single Responsibility Rule
        public IEnumerable<Payment> FilterByCategory(IEnumerable<Payment> payments, Category category)
        {
            foreach (var item in payments)
            {
                if (item.Category == category)
                    yield return item;
            }
        }

        public IEnumerable<Payment> FilterByPaymentMode(IEnumerable<Payment> payments, PaymentMode mode)
        {
            foreach (var item in payments)
            {
                if (item.PaymentMode == mode)
                    yield return item;
            }
        }

        public IEnumerable<Payment> FilterByPaymentModeAndCategory(IEnumerable<Payment> payments, PaymentMode mode, Category category)
        {
            foreach (var item in payments)
            {
                if (item.PaymentMode == mode && item.Category == category)
                    yield return item;
            }
        }
    }

    #endregion

    #region new method - follow Open/Closed Principle

    public interface IFilterCriteria<T>
    {
        bool isSatisfied(T t);
    }

    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, IFilterCriteria<T> criteria);
    }


    // this is good because everytime when there is new filter criteria, the existing CategoryCriteria and PaymentModeCriteria
    // will not be modified, new implementation of IFilterCriteria<T> will be created
    public class CategoryCriteria : IFilterCriteria<Payment>
    {
        private Category Category;

        public CategoryCriteria(Category category)
        {
            Category = category;
        }

        public bool isSatisfied(Payment t)
        {
            return t.Category == Category;
        }
    }

    public class PaymentModeCriteria : IFilterCriteria<Payment>
    {
        private PaymentMode PaymentMode;

        public PaymentModeCriteria(PaymentMode mode)
        {
            PaymentMode = mode;
        }

        public bool isSatisfied(Payment t)
        {
            return t.PaymentMode == PaymentMode;
        }
    }

    public class AndFilterCriteria<T> : IFilterCriteria<T>
    {
        private IFilterCriteria<T> FirstCriteria;
        private IFilterCriteria<T> SecondCriteria;

        public AndFilterCriteria(IFilterCriteria<T> firstCriteria, IFilterCriteria<T> secondCriteria)
        {
            FirstCriteria = firstCriteria ?? throw new ArgumentNullException(paramName: nameof(firstCriteria));
            SecondCriteria = secondCriteria ?? throw new ArgumentNullException(paramName: nameof(secondCriteria));
        }

        public bool isSatisfied(T t)
        {
            return FirstCriteria.isSatisfied(t) && SecondCriteria.isSatisfied(t);
        }
    }

    public class BetterPaymentFilter : IFilter<Payment>
    {
        public IEnumerable<Payment> Filter(IEnumerable<Payment> items, IFilterCriteria<Payment> criteria)
        {
            foreach (var item in items)
            {
                if (criteria.isSatisfied(item))
                    yield return item;
            }
        }
    }

    #endregion
}
