using System;
using ToyRobotSimulator;

namespace ToyRobotSimulator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Toy Robot Simulator");
            Console.WriteLine("Commands: PLACE X,Y,F | MOVE | LEFT | RIGHT | REPORT | EXIT");
            Console.WriteLine("Example: PLACE 0,0,NORTH");

            Simulator simulator = new Simulator();
            string commandInput;

            while (true)
            {
                Console.Write("> ");
                commandInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(commandInput))
                {
                    continue;
                }

                if (commandInput.Equals("EXIT", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Exiting simulator.");
                    break;
                }

                string result = simulator.ProcessCommand(commandInput);
                if (!string.IsNullOrEmpty(result))
                {
                    Console.WriteLine(result);
                }
            }
        }
    }
}


// --- ToyRobotSimulator.Tests Project ---
// Add a reference to ToyRobotSimulator project in ToyRobotSimulator.Tests
