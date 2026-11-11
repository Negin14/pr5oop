using System;

namespace ConsoleApp
{
    public class TCircle<T> where T : struct, IComparable, IConvertible
    {
        private T radius;

        public T Radius
        {
            get => radius;
            set
            {
                if (Convert.ToDouble(value) < 0)
                {
                    throw new ArgumentException("Радіус не може бути від'ємним.");
                }
                radius = value;
            }
        }

        public TCircle()
        {
            radius = default(T);
        }

        public TCircle(T radius)
        {
            Radius = radius;
        }

        public TCircle(TCircle<T> previousCircle)
        {
            radius = previousCircle.radius;
        }

        public void InputData()
        {
            while (true)
            {
                Console.Write("Введіть радіус: ");
                string input = Console.ReadLine();
                try
                {
                    radius = (T)Convert.ChangeType(input, typeof(T));
                    if (Convert.ToDouble(radius) < 0)
                    {
                        Console.WriteLine("Помилка: радіус не може бути від'ємним. Використовуйте тільки додатні цифри.");
                        continue;
                    }
                    break;
                }
                catch
                {
                    Console.WriteLine("Помилка: використано некоректне значення. Будь ласка, використовуйте тільки цифри.");
                }
            }
        }

        public void OutputData()
        {
            Console.WriteLine($"Коло з радіусом: {radius}");
        }

        public double GetArea()
        {
            double r = Convert.ToDouble(radius);
            return Math.PI * r * r;
        }

        public double GetSectorArea(double angleInDegrees)
        {
            return GetArea() * (angleInDegrees / 360.0);
        }

        public double GetLength()
        {
            double r = Convert.ToDouble(radius);
            return 2 * Math.PI * r;
        }

        public int CompareTo(TCircle<T> other)
        {
            double r1 = Convert.ToDouble(this.radius);
            double r2 = Convert.ToDouble(other.radius);
            return r1.CompareTo(r2);
        }

        public static TCircle<T> operator +(TCircle<T> c1, TCircle<T> c2)
        {
            double res = Convert.ToDouble(c1.radius) + Convert.ToDouble(c2.radius);
            T newRadius = (T)Convert.ChangeType(res, typeof(T));
            return new TCircle<T>(newRadius);
        }

        public static TCircle<T> operator -(TCircle<T> c1, TCircle<T> c2)
        {
            double res = Convert.ToDouble(c1.radius) - Convert.ToDouble(c2.radius);
            if (res < 0)
            {
                res = 0;
            }
            T newRadius = (T)Convert.ChangeType(res, typeof(T));
            return new TCircle<T>(newRadius);
        }

        public static TCircle<T> operator *(TCircle<T> c, double number)
        {
            double res = Convert.ToDouble(c.radius) * number;
            T newRadius = (T)Convert.ChangeType(res, typeof(T));
            return new TCircle<T>(newRadius);
        }

        public static TCircle<T> operator *(double number, TCircle<T> c)
        {
            return c * number;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;

            TCircle<double> c1 = new TCircle<double>();
            Console.WriteLine("Введення для першого кола (c1):");
            c1.InputData();

            TCircle<double> c2 = new TCircle<double>();
            Console.WriteLine("\nВведення для другого кола (c2):");
            c2.InputData();

            TCircle<double> c3 = new TCircle<double>(c1);

            Console.WriteLine("\n--- Дані c1 ---");
            c1.OutputData();
            Console.WriteLine($"Площа круга: {c1.GetArea():F2}");
            Console.WriteLine($"Площа сектора (90 град): {c1.GetSectorArea(90):F2}");
            Console.WriteLine($"Довжина кола: {c1.GetLength():F2}");

            Console.WriteLine("\n--- Операції над c1 та c2 ---");
            TCircle<double> sum = c1 + c2;
            Console.Write("c1 + c2 = ");
            sum.OutputData();

            TCircle<double> diff = c1 - c2;
            Console.Write("c1 - c2 = ");
            diff.OutputData();

            TCircle<double> mult = c1 * 2;
            Console.Write("c1 * 2 = ");
            mult.OutputData();

            Console.WriteLine("\n--- Порівняння ---");
            int comp = c1.CompareTo(c2);
            if (comp > 0)
            {
                Console.WriteLine("c1 більше за c2");
            }
            else if (comp < 0)
            {
                Console.WriteLine("c1 менше за c2");
            }
            else
            {
                Console.WriteLine("c1 дорівнює c2");
            }

            Console.ReadKey();
        }
    }
}