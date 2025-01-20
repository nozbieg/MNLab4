using OxyPlot;
using OxyPlot.Series;
using OxyPlot.SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MNLab4;
public class RungeKuttaSolver
{
    public static double F1(double x, double y, double a, double b) =>  a* x + b* y;
    public static double F2(double x, double y, double a, double b) => a * x - b * y;

    public static double F3(double x, double y, double a, double b) => a * x * b * y;

    public static double F4(double x, double y, double a, double b) =>  y != 0 ? (a * x) / (b * y) : 0;

    public static (List<double> xVals, List<double> yVals) RungeKutta3(double x0, double y0, double h, int n, Func<double, double, double, double, double> F, double a, double b)
    {
        var xVals = new List<double> { x0 };
        var yVals = new List<double> { y0 };

        for (int i = 0; i < n; i++)
        {
            double xi = xVals.Last();
            double yi = yVals.Last();
            double m1 = F(xi, yi, a, b);
            double m2 = F(xi + h / 2, yi + (h / 2) * m1, a, b);
            double m3 = F(xi + h, yi - h * m1 + 2 * h * m2, a, b);
            double yiPlus1 = yi + (h / 6) * (m1 + 4 * m2 + m3);
            double xiPlus1 = xi + h;
            xVals.Add(xiPlus1);
            yVals.Add(yiPlus1);
        }
        return (xVals, yVals);
    }
    public static (List<double> xVals, List<double> yVals) RungeKutta4(double x0, double y0, double h, int n, Func<double, double, double, double, double> F, double a, double b)
    {
        var xVals = new List<double> { x0 };
        var yVals = new List<double> { y0 };

        for (int i = 0; i < n; i++)
        {
            double xi = xVals.Last();
            double yi = yVals.Last();
            double m1 = F(xi, yi, a, b);
            double m2 = F(xi + h / 2, yi + (h / 2) * m1, a, b);
            double m3 = F(xi + h / 2, yi + (h / 2) * m2, a, b);
            double m4 = F(xi + h, yi + h * m3, a, b);
            double yiPlus1 = yi + (h / 6) * (m1 + 2 * m2 + 2 * m3 + m4);
            double xiPlus1 = xi + h;
            xVals.Add(xiPlus1);
            yVals.Add(yiPlus1);
        }
        return (xVals, yVals);
    }

    public static void PlotResults(List<double> xVals, List<double> yVals, string methodName)
    {
        Console.WriteLine($"\nWyniki u¿ywaj¹c {methodName}:");
        for (int i = 0; i < xVals.Count; i++)
        {
            Console.WriteLine($"x = {xVals[i]:F4}, y = {yVals[i]:F4}");
        }

        var plotModel = new PlotModel { Title = $"Rozwi¹zanie równania metod¹ {methodName}" };
        var lineSeries = new LineSeries
        {
            Title = methodName,
            MarkerType = MarkerType.Circle
        };

        for (int i = 0; i < xVals.Count; i++)
        {
            lineSeries.Points.Add(new DataPoint(xVals[i], yVals[i]));
        }

        plotModel.Series.Add(lineSeries);

        using (var stream = new System.IO.FileStream($"{methodName}.png", System.IO.FileMode.Create))
        {
            PngExporter.Export(plotModel, stream, 800, 600);
        }

        Console.WriteLine($"Wykres zapisany jako {methodName}.png");
    }
}
