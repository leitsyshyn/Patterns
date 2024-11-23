using System.Collections.Generic;
using System;
using System.Text;

namespace Decorator
{
    class Program
    {
        static void Main()
        {
            List<string> ornamentVariants = new List<string> { "Білий янгол", "Золотий шар", "Червона кулька", "Срібний дзвіночок" };
            Console.OutputEncoding = Encoding.UTF8;
            Tree plainTree = new PlainTree();

            TreeWithOrnamentsDecorator decoratedTree = new TreeWithOrnamentsDecorator();
            decoratedTree.SetTree(plainTree);

            Console.WriteLine("Оберіть прикраси для ялинки, введіть їх через кому (напр. 1,2,3), 0 щоб пропустити:");
            for (int i = 0; i < ornamentVariants.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {ornamentVariants[i]}");
            }

            int addedOrnaments = 0;
            while(addedOrnaments < 1)
            {
                Console.Write("Ваш вибір: ");
                string userInput = Console.ReadLine();
                if (userInput == "0") break;
                if (!string.IsNullOrWhiteSpace(userInput))
                {
                    string[] inputs = userInput.Split(',');
                    foreach (var input in inputs)
                    {
                        if (int.TryParse(input.Trim(), out int choice) && choice >= 1 && choice <= ornamentVariants.Count)
                        {
                            decoratedTree.AddOrnament(ornamentVariants[choice - 1]);
                            addedOrnaments++;
                        }
                        else
                        {
                            Console.WriteLine($"Прикраси під номером \"{input}\" не знайшлось.");
                        }
                    }
                    if (addedOrnaments < 1)
                    {
                        Console.WriteLine("Оберіть прикраси для ялинки, введіть їх через кому (напр. 1,2,3), 0 щоб пропустити:");
                    }
                }
            }
            
            Console.WriteLine("\nОберіть режим освітлення для ялинки:");
            Console.WriteLine("0. Вимкнена");
            Console.WriteLine("1. Сталий");
            Console.WriteLine("2. Миготіння");
            Console.Write("Ваш вибір: ");
            string lightModeInput = Console.ReadLine();
            while (lightModeInput != "1" && lightModeInput != "2" && lightModeInput != "0")
            {
                Console.WriteLine("Невірний вибір. Будь ласка, введіть 0, 1 або 2.");
                Console.Write("Ваш вибір: ");
                lightModeInput = Console.ReadLine();
            } 
            TreeWithLightsDecorator treeWithLights = new TreeWithLightsDecorator(lightModeInput);
            treeWithLights.SetTree(decoratedTree);

            treeWithLights.Display();

            Console.Read();
        }
    }

    // AbstractTree
    abstract class Tree
    {
        public abstract void Display();
    }

    // ConcreteTree
    class PlainTree : Tree
    {
        public override void Display()
        {
            Console.WriteLine("Демонструють ялинку");
        }
    }

    // AbstractDecorator
    abstract class TreeDecorator : Tree
    {
        protected Tree tree;

        public void SetTree(Tree tree)
        {
            this.tree = tree;
        }

        public override void Display()
        {
            if (tree != null)
            {
                tree.Display();
            }
        }
    }

    // ConcreteDecoratorA
    class TreeWithOrnamentsDecorator : TreeDecorator
    {
        private List<string> ornaments = new List<string>();

        public void AddOrnament(string ornament)
        {
            if (!ornaments.Contains(ornament))
            {
                ornaments.Add(ornament);
                Console.WriteLine($"Додано прикрасу: {ornament}");
            }
            else
            {
                Console.WriteLine($"Прикраса {ornament} вже додана.");
            }
        }

        public override void Display()
        {
            base.Display();
            if (ornaments.Count != 0)
            {
                Console.WriteLine("з прикрасами:");
            }
            else
            {
                Console.WriteLine("Без прикрас");
            }
            foreach (var ornament in ornaments)
            {
                Console.WriteLine($" - {ornament}");
            }
        }
    }

    // ConcreteDecoratorB
    class TreeWithLightsDecorator : TreeDecorator
    {
        private string lightMode;

        public TreeWithLightsDecorator(string mode = "Сталий")
        {
            lightMode = mode;
        }

        public override void Display()
        {
            base.Display();
            ToggleLights();
        }

        private void ToggleLights()
        {
            if (lightMode == "0")
            {
                Console.WriteLine("яка вимкнена");
            }
            else if (lightMode == "1")
            {
                Console.WriteLine("яка світиться");
            }
            else if (lightMode == "2")
            {
                Console.WriteLine("яка миготить");
            }
        }
    }
}
