using System;

namespace StrategyDesignPatterns
{
    internal class Program
    {
        static void Main()
        {
            Context context = new Context();
            context.SwitchStrategy();
            Random r = new Random(37);
            for (int i = Context.Start; i < Context.Start + 15; i++)
            {
                if (r.Next(3) == 2)
                {
                    Console.Write("|| ");
                    context.SwitchStrategy();
                }
                Console.Write(context.Algorithm()+ " ");
            }
            Console.WriteLine();
            Console.ReadLine();
        }
    }


    internal class Context
    {
        public const int Start = 5;
        public int Counter = 5;

        private IStrategy strategy = new Strategy1();

        public int Algorithm()
        {
            return strategy.Move(this);
        }

        public void SwitchStrategy()
        {
            if(strategy is Strategy1)
                strategy = new Strategy2();
            else
            {
                strategy = new Strategy1();
            }
        }
    }

    interface IStrategy
    {
        int Move(Context c);
    }

    internal class Strategy1 : IStrategy
    {
        public int Move(Context c)
        {
            return ++c.Counter;
        }
    }

    internal class Strategy2 : IStrategy
    {
        public int Move(Context c)
        {
            return --c.Counter;
        }
    }
}
