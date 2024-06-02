using System;

namespace BridgePatternExample
{
    // Інтерфейс IRenderer
    public interface IRenderer
    {
        void Render(string shape);
    }

    // Конкретні реалізації інтерфейсу IRenderer
    public class VectorRenderer : IRenderer
    {
        public void Render(string shape)
        {
            Console.WriteLine($"Drawing {shape} as vector graphics.");
        }
    }

    public class RasterRenderer : IRenderer
    {
        public void Render(string shape)
        {
            Console.WriteLine($"Drawing {shape} as pixels.");
        }
    }

    // Базовий клас Shape
    public abstract class Shape
    {
        protected IRenderer _renderer;

        protected Shape(IRenderer renderer)
        {
            _renderer = renderer;
        }

        public abstract void Draw();
    }

    // Конкретні класи фігур
    public class Circle : Shape
    {
        public Circle(IRenderer renderer) : base(renderer) { }

        public override void Draw()
        {
            _renderer.Render("Circle");
        }
    }

    public class Square : Shape
    {
        public Square(IRenderer renderer) : base(renderer) { }

        public override void Draw()
        {
            _renderer.Render("Square");
        }
    }

    public class Triangle : Shape
    {
        public Triangle(IRenderer renderer) : base(renderer) { }

        public override void Draw()
        {
            _renderer.Render("Triangle");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Використання VectorRenderer
            IRenderer vectorRenderer = new VectorRenderer();
            Shape vectorCircle = new Circle(vectorRenderer);
            Shape vectorSquare = new Square(vectorRenderer);
            Shape vectorTriangle = new Triangle(vectorRenderer);

            vectorCircle.Draw();
            vectorSquare.Draw();
            vectorTriangle.Draw();

            // Використання RasterRenderer
            IRenderer rasterRenderer = new RasterRenderer();
            Shape rasterCircle = new Circle(rasterRenderer);
            Shape rasterSquare = new Square(rasterRenderer);
            Shape rasterTriangle = new Triangle(rasterRenderer);

            rasterCircle.Draw();
            rasterSquare.Draw();
            rasterTriangle.Draw();
        }
    }
}
