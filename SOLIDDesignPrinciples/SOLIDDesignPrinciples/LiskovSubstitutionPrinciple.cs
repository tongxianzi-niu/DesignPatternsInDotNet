using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDDesignPrinciples
{
    internal class LiskovSubstitutionPrinciple
    {
        // Derived or child classes must be substitutable for their base or parent classes
    }

    public class Rectangle
    {
        public virtual int Width { get; set; }

        public virtual int Height { get; set; }

        public Rectangle()
        {

        }

        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }

    public class Square : Rectangle
    {
        public override int Width { set { base.Width = base.Height = value; } }
        public override int Height { set { base.Height = base.Width = base.Height = value; } }

        public Square(int width, int height) : base(width, height)
        {
        }
    }

    public abstract class Quadrangle
    {
        public abstract int Width { get; }
        public abstract int Height { get; }

        public abstract void SetHeight(int height);
        public abstract void SetWidth(int width);

    }

    public class Rectangle2 : Quadrangle
    {
        public int RecWidth;
        public int RecHeight;

        public override int Width => this.RecWidth;

        public override int Height => this.RecHeight;

        public override void SetHeight(int h) => RecWidth = RecHeight = h;

        public override void SetWidth(int w) => RecWidth = RecHeight = w;
    }

    public class Square2 : Quadrangle
    {
        public int SqaureWidth;

        public override int Width => this.SqaureWidth;

        public override int Height => this.SqaureWidth;

        public override void SetWidth(int w) => SqaureWidth = w;

        public override void SetHeight(int h) => SqaureWidth = h;
    }
}
