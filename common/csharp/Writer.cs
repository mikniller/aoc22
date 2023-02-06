
namespace Aoc.Common
{
    public abstract class Writer
    {
        public abstract void Write(string s);
        public abstract void WriteLine(string s);
    }

    public class ConsoleWriter: Writer
    {
        public override void Write(string s)
        {
            Console.Write(s);
        }

        public override void WriteLine(string s)
        {
            Console.WriteLine(s);
        }
    }

    public class DebugWriter : Writer
    {
        public override void Write(string s)
        {
            System.Diagnostics.Debug.Write(s);
        }

        public override void WriteLine(string s)
        {
            System.Diagnostics.Debug.WriteLine(s);
        }
    }

    public class NullWriter : Writer
    {
        public override void Write(string s)
        {
        }

        public override void WriteLine(string s)
        {
        }
    }
}
