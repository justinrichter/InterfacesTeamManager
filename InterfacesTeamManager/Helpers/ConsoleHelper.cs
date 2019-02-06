using System;

namespace InterfacesTeamManager.Helpers
{
    static class ConsoleHelper
    {
        // Reads user input as a string then converts to lowercase
        public static string ReadInput(string prompt, bool forceToLowercase = false)
        {
            Console.WriteLine();
            Console.Write(prompt);
            string input = Console.ReadLine();
            return forceToLowercase ? input.ToLower() : input;
        }

        // Clears the console output
        public static void ClearOutput()
        {
            Console.Clear();
        }

        // Writes the provided message to the console
        public static void Output(string message)
        {
            Console.Write(message);
        }

        // Writes the provided format string to the console
        public static void Output(string format, params object[] arg)
        {
            Console.Write(format, arg);
        }

        // Writes the provided message to the console as a line
        public static void OutputLine(string message, bool outputBlankLineBeforeMessage = true)
        {
            if (outputBlankLineBeforeMessage)
            {
                Console.WriteLine();
            }
            Console.WriteLine(message);
        }

        // Writes the provided format string and args to the console as a line
        public static void OutputLine(string format, params object[] arg)
        {
            Console.WriteLine(format, arg);
        }

        /// Writes a blank line to the console
        public static void OutputBlankLine()
        {
            Console.WriteLine();
        }
    }
}
