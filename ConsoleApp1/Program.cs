namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var commandStack = new Stack<AppendTextCommand>();
            var redoStack = new Stack<AppendTextCommand>();
            var originalText = "";


            while (true)
            {
                Console.Write("Type The Text To appended ('exit' to Exit, 'undo' to Undo,''redo' to Redo ): ");
                var input = Console.ReadLine();
                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                    break;
                else if (input.Equals("undo", StringComparison.OrdinalIgnoreCase))
                {
                    if (commandStack.Count > 0)
                    {
                        var command = commandStack.Pop();
                        originalText = command.Undo();
                        redoStack.Push(command);


                    }
                }
                else if (input.Equals("redo", StringComparison.OrdinalIgnoreCase))
                {
                    if (redoStack.Count > 0)
                    {
                        var command = redoStack.Pop();
                        commandStack.Push(command);
                        originalText = command.Execute();

                    }
                    else
                    {
                        Console.WriteLine("the stack is empty you can't redo from it ");
                    }
                }
                else
                {
                    var command = new AppendTextCommand(originalText, input);
                    originalText = command.Execute();
                    commandStack.Push(command);
                }
            }



        }
    }

    class AppendTextCommand
    {
        private string _originalText;
        private string _textToAppend;
        public AppendTextCommand(string originalText, string textToAppend)
        {
            _originalText = originalText;
            _textToAppend = textToAppend;
        }
        public string Execute()
        {
            _originalText += _textToAppend;
            Console.WriteLine($"Your Text is : " + _originalText);
            return _originalText + " ";

        }
        public string Undo()
        {
            _originalText = _originalText.Substring(0, _originalText.Length - _textToAppend.Length);
            Console.WriteLine(_originalText);
            return _originalText;
        }

    }
}
