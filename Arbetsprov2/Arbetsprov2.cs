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
        //StreamReader reader = new(@"C:\Users\Fractal ERA\Desktop\Vitec\Arbetsprov2\input.txt");
        //string input = reader.ReadToEnd();

        string testString = " { } { }";
        Console.WriteLine("Original: {0}", testString);
        //Console.WriteLine("Modified: {0}", NullifyJunk(testString));

        bool groupOpen = false;
        //int totalGroups = 0;
        //int points = 0;
        //int openToken = 0;
        //int closeToken = 0;

        int i = 0;
        foreach (var token in testString) // Iterates over input-string
        {
            i++;
            // If we find start of group while no group is open
            if (token == '{' && !groupOpen){
                groupOpen = true;
                Console.WriteLine(token);
                //points += countGroup(); // Räkna antalet grupper i den öppna gruppen. Ska returnera när gruppen stängs.

            }
        }


    }
  }