using System.Text;
class Program
  {
    static int points = 0;
    static string NullifyJunk(string input)
    /** 
    Takes a string. Removes blocks of junk. Returns string with only '{' or '}'
    */
    {
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
                if (token == '{' || token == '}')
                {
                    output.Append(token);
                }
            }

            if (token == '>' && parsingJunk) {
                parsingJunk = false;
            }
        }
        return output.ToString();
    }
    static string ParseOuterGroup(string input)
    /** 
    Takes a string. Removes outermost groups and returns that as string.
    Adds points to static variable 'points' according to rules provided. 
    */
    {
        var output = new StringBuilder();
        bool parsingGroup = false;
        int groupsOpen = 0;
        int groupsClosed = 0;
        
        foreach (var token in input)
        {
            if (token == '{' && !parsingGroup){
                parsingGroup = true;
                continue;
            }

            if (parsingGroup && token == '}' && groupsOpen == groupsClosed) 
            {
                parsingGroup = false;
                points += groupsClosed + 1;
                groupsOpen = 0;
                groupsClosed = 0;
                continue;
            }

            if (parsingGroup && token == '{') 
            {
                groupsOpen += 1;
            }

            if (parsingGroup && token == '}') 
            {
                groupsClosed += 1;
            }

            if(parsingGroup)
            {
                output.Append(token);
            }   
        }
        return output.ToString();
    }
    static void Main()
    {
        /* 
        Input into text-file for safe string encapsulation
        Filepath hardcoded since we only need to parse specific file once.
        */
        string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
        string? directoryPath = Path.GetDirectoryName(exePath);
        string fullPath = directoryPath + @"\input.txt";
        Console.WriteLine(fullPath);


        //StreamReader reader = new(@"C:\Users\Fractal ERA\Desktop\Vitec\Arbetsprov2\input.txt");
        StreamReader reader = new(fullPath);

        string input = reader.ReadToEnd();

        //  NullifyJunk removes blocks of junks and only saves '{' and '}'
        string cleanedInput = NullifyJunk(input);

        /* 
        Iterate over the the cleanedInput, every iteration removes outermost groups, 
        reducing size of cleanedInput per iteration 
        */
        while (cleanedInput.Length > 0)
        {
            cleanedInput = ParseOuterGroup(cleanedInput);
        }

        Console.WriteLine("Points: {0}", points);
    }
  }