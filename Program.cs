using static MNLab4.RungeKuttaSolver;

Console.WriteLine("Choose function F(x, y):");
Console.WriteLine("1: F(x, y) = a * x + b * y");
Console.WriteLine("2: F(x, y) = a * x - b * y");
Console.WriteLine("3: F(x, y) = a * x * b * y");
Console.WriteLine("4: F(x, y) = (a * x) / (b * y)");
int choice = int.Parse(Console.ReadLine());

Func<double, double, double, double, double> F = choice switch
{
    1 => F1,
    2 => F2,
    3 => F3,
    4 => F4,
    _ => throw new ArgumentException("Invalid choice!")
};

Console.Write("Enter x0: ");
double x0 = double.Parse(Console.ReadLine());
Console.Write("Enter y0: ");
double y0 = double.Parse(Console.ReadLine());
Console.Write("Enter step size h: ");
double h = double.Parse(Console.ReadLine());
Console.Write("Enter number of steps n: ");
int n = int.Parse(Console.ReadLine());

double a, b;
while (true)
{
    Console.Write("Enter constant a (non-zero): ");
    a = double.Parse(Console.ReadLine());
    if (a != 0) break;
    Console.WriteLine("Constant a cannot be zero! Try again.");
}

while (true)
{
    Console.Write("Enter constant b (non-zero): ");
    b = double.Parse(Console.ReadLine());
    if (b != 0) break;
    Console.WriteLine("Constant b cannot be zero! Try again.");
}

Console.WriteLine("\nChoose method:");
Console.WriteLine("1: Runge-Kutta 3rd order");
Console.WriteLine("2: Runge-Kutta 4th order");
int methodChoice = int.Parse(Console.ReadLine());

(List<double> xVals, List<double> yVals) results;
string methodName;

if (methodChoice == 1)
{
    methodName = "Runge-Kutta 3rd order";
    results = RungeKutta3(x0, y0, h, n, F, a, b);
}
else if (methodChoice == 2)
{
    methodName = "Runge-Kutta 4th order";
    results = RungeKutta4(x0, y0, h, n, F, a, b);
}
else
{
    Console.WriteLine("Invalid choice!");
    return;
}

PlotResults(results.xVals, results.yVals, methodName);