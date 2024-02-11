using System.Text;

class Program
  {
    static string NullifyJunk(string input) {
        bool parsingJunk = false;
        bool skipNext = false;

        var output = new StringBuilder();

        foreach (var token in input) 
        {
            if (skipNext) {
                skipNext = false;
                continue;
            }

            if (token == '<' && !parsingJunk){
                parsingJunk = true;
            }

            if (parsingJunk && token == '!') {
                skipNext = true;
                continue;
            }

            if(!parsingJunk) {
                output.Append(token);
            }

            if (token == '>' && parsingJunk) {
                parsingJunk = false;
            }
        }
        return output.ToString();
    }
    static void Main()
    {
        StreamReader reader = new(@"C:\Users\Fractal ERA\Desktop\Vitec\Arbetsprov2\input.txt");
        string input = reader.ReadToEnd();

        string testString = "abc<abc!>abc";
        Console.WriteLine("Original: {0}", testString);

        Console.WriteLine("Modified: {0}", NullifyJunk(testString));


    }
  }