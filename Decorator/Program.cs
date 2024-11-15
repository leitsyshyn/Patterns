using System.Collections.Generic;
using System;
using System.Text;

namespace Decorator
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Tree plainTree = new PlainTree();

            TreeWithOrnamentsDecorator decoratedTree = new TreeWithOrnamentsDecorator();
            decoratedTree.SetTree(plainTree);
            decoratedTree.AddOrnament("Білий янгол");
            decoratedTree.AddOrnament("Золотий шар");
            decoratedTree.AddOrnament("Червона кулька");
            decoratedTree.AddOrnament("Срібний колокольчик");

            TreeWithLightsDecorator treeWithLights = new TreeWithLightsDecorator("Миготіння");
            treeWithLights.SetTree(decoratedTree);

            treeWithLights.Display();

            Console.Read();
        }
    }

    //AbstractTree
    abstract class Tree
    {
        public abstract void Display();
    }

    //ConcreteTree
    class PlainTree : Tree
    {
        public override void Display()
        {
            Console.WriteLine("Демонструють ялинку ");
        }
    }

    //AbstractDecorator
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

    //ConcreteDecoratorA
    class TreeWithOrnamentsDecorator : TreeDecorator
    {
        private List<string> ornaments = new List<string>();

        public void AddOrnament(string ornament)
        {
            ornaments.Add(ornament);
        }

        public override void Display()
        {
            base.Display();
            Console.WriteLine("з прикрасами:");
            foreach (var ornament in ornaments)
            {
                Console.WriteLine($" - {ornament}");
            }
        }
    }

    //ConcreteDecoratorB
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
            if (lightMode == "Миготіння")
            {
                Console.WriteLine("яка миготить");
            }
            else
            {
                Console.WriteLine("яка світиться");
            }
        }
    }
}

