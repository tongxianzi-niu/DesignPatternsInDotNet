using SOLIDDesignPrinciples;


#region Open/Closed Principle Demo
OpenClosedPrinciple oCDemo = new OpenClosedPrinciple();
oCDemo.Demo();
#endregion

Console.WriteLine();

#region Liskov Substitution Principle Demo

static int Area(Rectangle r) => r.Width * r.Height;

Rectangle r = new Rectangle(2, 3);
Console.WriteLine($"{nameof(r.Width)}:{r.Width}, {nameof(r.Height)}:{r.Height}, {nameof(Area)}: {Area(r)}");


Rectangle s = new Square(2, 3);

// expected result should be 6 but actual result is 9
// child class cannot replace parent class so square is not subtype of rectangle
Console.WriteLine($"{nameof(s.Width)}:{s.Width}, {nameof(s.Height)}:{s.Height}, {nameof(Area)}: {Area(s)}");

#endregion

Console.WriteLine();

#region Dependency Injection Priciple Demo
DependencyInversionPrinciple dIDemo1 = new DependencyInversionPrinciple();
dIDemo1.Example1Demo();

DependencyInversionPrinciple dIDemo2 = new DependencyInversionPrinciple();
dIDemo2.Example2Demo();
#endregion