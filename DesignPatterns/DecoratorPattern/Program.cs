using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace DecoratorPattern
{
    sealed class Program
    {
      
        static void Main()
        {
            Console.WriteLine("Decorator Pattern\n");

            IComponent component = new Component();

            Display("1. Basic component: ", component);
            Display("2. A-decorated: ", new DecoratorA(component));
            Display("3. B-decorated: ", new DecoratorB(component));
            Display("4. B-A-decorated: ", new DecoratorB(new DecoratorA(component)));

            DecoratorB b = new DecoratorB(new Component());
            Display("5. A-B-decorated: ", new DecoratorA(b));

            // invoking its added statt and added behavior
            Console.WriteLine("\t\t\t" + b.AddedState + b.AddedBehavior());

            using (var consoleDecorator = new ConsoleDecorator(Console.Out, 30))
            {
                Console.SetOut(consoleDecorator);
            }
            //Console.Write("C# 3.0 Design Patterns.");
            Console.WriteLine("Question No. 4: Decorate the Console class so that Write and WriteLine methods are trapped and the output is reformatted for lines of a given size, avoiding unsightly wrap-arounds. Test your decorator with the program in Example 2 - 1.");

            Console.ReadLine();
        }

        static void Display(string s, IComponent c)
        {
            Console.WriteLine(s + c.Operation());
        }
    }

    internal class PrivateSetterClass
    {
        private string _privateSetter;

        public string PrivateSetter
        {
            get { return _privateSetter; }
            private set {
                _privateSetter = string.Equals(value, "thanhDoan") ? "you are hero" : value;
            }
        }

        public PrivateSetterClass(string inputValue)
        {
            PrivateSetter = inputValue;
        }
    }

    internal interface IComponent
    {
        string Operation();
    }

    internal sealed class Component : IComponent
    {
        public string Operation()
        {
            return "I am walking";
        }
    }

    internal sealed class DecoratorA : IComponent
    {
        private readonly IComponent _component;

        public DecoratorA(IComponent component)
        {
            _component = component;
        }

        public string Operation()
        {
            var s = _component.Operation();
            s += " and listening to Classic FM";
            return s;
        }
    }

    internal sealed class DecoratorB : IComponent
    {
        private readonly IComponent _component;

        public string AddedState = " pass the Coffee Shop";

        public DecoratorB(IComponent component)
        {
            _component = component;
        }

        public string Operation()
        {
            var s = _component.Operation();
            s += " to school";
            return s;
        }

        public string AddedBehavior()
        {
            return " and I bought a cappuccino";
        }
    }


    internal sealed class ConsoleDecorator : TextWriter
    {
        private TextWriter _originalWriter;
        private int _lineSize;

        public ConsoleDecorator(TextWriter inWriter, int sizeOfLine)
        {
            _originalWriter = inWriter;
            if(sizeOfLine > 0)
            {
                _lineSize = sizeOfLine;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(sizeOfLine));
            }
        }

        public override void Write(string value)
        {
            if (!String.IsNullOrEmpty(value))
            {
                string[] words = value.Split(new char[] {' '}, StringSplitOptions.None);
                int runningLength = 0;
                int countOfWords = words.Count();
                for(int index =0; index< countOfWords; index++)
                {
                    string wordToPrint = string.Empty;
                    if (index != countOfWords - 1)
                    {
                        runningLength += words[index].Length + 1;
                        wordToPrint = words[index] + " ";
                    }
                    else
                    {
                        runningLength += words[index].Length;
                        wordToPrint = words[index];
                    }

                    if (runningLength >= _lineSize)
                    {
                        // Start a new line because printing the next word would violate the limit
                        _originalWriter.WriteLine();
                        // Reset the runningLength since a new line was begun
                        runningLength = words[index].Length;
                    }

                    _originalWriter.Write(wordToPrint);
                }

            }
            else if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            _originalWriter.Flush();
        }

        public override void WriteLine(string value)
        {
            this.Write(value);
            _originalWriter.WriteLine();
            _originalWriter.Flush();
        }

        public override Encoding Encoding {
            get
            {
                return _originalWriter.Encoding;
            }
        }
    }
}
