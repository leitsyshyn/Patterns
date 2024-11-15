using System;
using System.Text;

namespace PrototypeFigure
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            try
            {
                IFigure figure = new Rectangle(10, 20);
                IFigure clonedFigure = figure.Clone();
                figure.GetInfo();
                clonedFigure.GetInfo();
                figure = new Circle(15);
                clonedFigure = figure.Clone();
                figure.GetInfo();
                clonedFigure.GetInfo();
                figure = new Triangle(3, 4, 5);
                clonedFigure = figure.Clone();
                figure.GetInfo();
                clonedFigure.GetInfo();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            Console.Read();
        }
    }

    interface IFigure
    {
        IFigure Clone();
        void GetInfo();
    }
    class Rectangle : IFigure
    {
        int width;
        int height;
        public Rectangle(int w, int h)
        {
            width = w;
            height = h;
        }
        public IFigure Clone()
        {
            return new Rectangle(this.width, this.height);
        }
        public void GetInfo()
        {
            Console.WriteLine("Прямокутник довжиною {0} и шириною {1}", height, width);
        }
    }
    class Circle : IFigure
    {
        int radius;
        public Circle(int r)
        {
            radius = r;
        }
        public IFigure Clone()
        {
            return new Circle(this.radius);
        }
        public void GetInfo()
        {
            Console.WriteLine("Круг радіусом {0}", radius);
        }
    }



    class Triangle : IFigure
    {
        int sideA;
        int sideB;
        int sideC;
        public Triangle(int a, int b, int c)
        {
            if (!IsValidTriangle(a, b, c))
            {
                throw new ArgumentException(string.Format("Трикутник зі сторонами {0}, {1} та {2} не існує", a, b, c));
            }
            sideA = a;
            sideB = b;
            sideC = c;
        }
        public IFigure Clone()
        {
            return new Triangle(this.sideA, this.sideB, this.sideC);
        }
        public void GetInfo()
        {
            Console.WriteLine("Трикутник зі сторонами {0}, {1} та {2}", sideA, sideB, sideC);
        }

        private static bool IsValidTriangle(double a, double b, double c)
        {
            return (a + b > c) && (a + c > b) && (b + c > a);
        }
    }
}
