using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDDesignPrinciples
{
    public class DependencyInversionPrinciple
    {
        public void Example1Demo()
        {
            IShape r = new Recetangle(2, 3);
            Draw.DrawShap(r);
            IShape c = new Circle(3);
            Draw.DrawShap(c);
        }

        public void Example2Demo()
        {
            IShop clothShop = new Cloth();
            IShop drinkShop = new Drink();
            Customer customer = new Customer();
            customer.Shopping(clothShop);
            customer.Shopping(drinkShop);
        }
    }

    #region Example 1
    public interface IShape
    {
        public double ComputeArea();
        public double ComputePerimeter();
    }

    public class Recetangle : IShape
    {
        public Recetangle()
        {

        }
        public Recetangle(double width, double height)
        {
            Width = width;
            Height = height;
        }

        private double Width { get; set; }
        private double Height { get; set; }
        public double ComputeArea()
        {
            return Width * Height;
        }

        public double ComputePerimeter()
        {
            return 2 * (Height + Width);
        }
    }

    public class Circle : IShape
    {
        public Circle(double radius)
        {
            Radius = radius;
        }

        private double Radius { get; set; }
        public double ComputeArea()
        {
            return Radius * Radius * Math.PI;
        }

        public double ComputePerimeter()
        {
            return 2 * Radius * Math.PI;
        }
    }

    public class Draw
    {
        public static void DrawShap(IShape shape)
        {
            Console.WriteLine($"Draw {nameof(shape)}: Area is {shape.ComputeArea()}");
        }
    }

    #endregion

    #region Example 2

    public interface IShop
    {
        public void Sell();
    }

    public class Cloth : IShop
    {
        public void Sell()
        {
            Console.WriteLine($"{nameof(Cloth)} shop sell dresses, skirts, shirts...");
        }
    }

    public class Drink : IShop
    {
        public void Sell()
        {
            Console.WriteLine($"{nameof(Drink)} shop sell beer, milk tea, juice...");
        }
    }

    public class Customer
    {
        #region Violate Dependency Inversion Principle
        // violate dependency inversion principle because whenever there is new subtype of shop,
        // customer class need to add new method for the new shop
        public void Shopping(Drink drink)
        {
            drink.Sell();
        }

        public void Shopping(Cloth cloth)
        {
            cloth.Sell();
        }
        #endregion

        #region Follow Dependency Inversion Principle
        // follow dependency inversion principle because no change required when there is new type of shop created
        public void Shopping(IShop shop)
        {
            shop.Sell();
        }

        #endregion
    }

    #endregion
}