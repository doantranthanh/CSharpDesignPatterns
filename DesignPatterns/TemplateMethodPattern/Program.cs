﻿using System;

namespace TemplateMethodPattern
{
    internal class Program
    {
        static void Main()
        {
            var m = new Algorithm();
            Algorithm.TemplateMethod(new ClassA());
            Algorithm.TemplateMethod(new ClassB());

            Console.ReadLine();
        }
    }

    interface IPrimitives
    {
        string Operation1();
        string Operation2();
    }

    internal class ClassA : IPrimitives {
        public string Operation1()
        {
            return "ClassA:Op1 ";
        }

        public string Operation2()
        {
            return "ClassA:Op2 ";

        }
    }

    internal class ClassB : IPrimitives
    {
        public string Operation1()
        {
            return "ClassB:Op1 ";
        }

        public string Operation2()
        {
            return "ClassB:Op2 ";

        }
    }

    internal class Algorithm
    {
        public static void TemplateMethod(IPrimitives a)
        {
            var s = a.Operation1() + a.Operation2();
            Console.WriteLine(s);
        }
    }
}
