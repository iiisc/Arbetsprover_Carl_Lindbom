class Shape(string t = "", int v = 0, int s = 0)
{
    private int Volume => v;
    private int Square => s;
    private string Type => t;
    public virtual int GetVolume()
    {
        return Volume;
    }
    public virtual int GetSquare()
    {
        return Square;
    }
    public virtual string GetShape()
    {
        return Type;
    }
}

class Block(int w, int l, int h) : Shape
{
    private int Volume => w*l*h;
    private int Square => w*l;
    public override int GetVolume()
    {
        return Volume;
    }
    public override int GetSquare()
    {
        return Square;
    }
    public override string GetShape()
    {
        return "Block";
    }
}

class Pyramid(int w, int l, int h) : Shape
{
    private int Volume => (w*l*h)/3;
    private int Square => w*l;

    public override int GetVolume()
    {
        return Volume;
    }
    public override int GetSquare()
    {
        return Square;
    }
    public override string GetShape()
    {
        return "Pyramid";
    }

}
class Program
{
    static Block AddBlock()
    {
        Console.Write("\n Du vill lägga till ett block:\n");
        Console.Write("\n Ange bredd: ");
        int width = Convert.ToInt32(Console.ReadLine());
        Console.Write("\n Ange längd: ");
        int length = Convert.ToInt32(Console.ReadLine());
        Console.Write("\n Ange höjd: ");
        int height = Convert.ToInt32(Console.ReadLine());

        Block block = new(width, length, height);
        return block;
    }
    static Pyramid AddPyramid()
    {
        Console.Write("\n Du vill lägga till en pyramid:\n");
        Console.Write("\n Ange basens bredd: ");
        int width = Convert.ToInt32(Console.ReadLine());
        Console.Write("\n Ange basens längd: ");
        int length = Convert.ToInt32(Console.ReadLine());
        Console.Write("\n Ange höjd: ");
        int height = Convert.ToInt32(Console.ReadLine());

        Pyramid pyramid = new(width, length, height);
        return pyramid;
    }
    static void RenderMenu()
        {
            Console.Clear();
            Console.WriteLine("Välj ditt alternativ: ");
            Console.WriteLine("1. Ange block ");
            Console.WriteLine("2. Ange pyramid ");
            Console.WriteLine("3. Visa data sorterat ");
            Console.WriteLine("4. Spara till fil ");
            Console.WriteLine("5. Öppna från fil ");
            Console.WriteLine("6. Avsluta ");
        }
    static void Main(string[] args)
    {
        bool menu = true;
        List<Shape> shapeList = new List<Shape>();
        string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
        string? directoryPath = Path.GetDirectoryName(exePath);

        while(menu == true) 
        {         
            RenderMenu();
            Console.WriteLine();
            string? userInput = Console.ReadLine();

            if (userInput == "1")
            {
                /** Adds a block **/ 
            shapeList.Add(AddBlock());
            }

            else if (userInput == "2")
            {
            /** Adds a pyramid **/ 
            shapeList.Add(AddPyramid());
            }

            else if (userInput == "3")
            {
                /** Displays a sorted list with highest volume at the top **/ 
                shapeList.Sort((x,y) => y.GetVolume().CompareTo(x.GetVolume()));
                foreach (var shape in shapeList) {
                    Console.WriteLine("\nForm: {2}. Volym: {0}. Yta: {1}", shape.GetVolume(), shape.GetSquare(), shape.GetShape());
                }
            }            

            else if (userInput == "4")
            {
                /** Write content of active shapeList to txt file **/ 
                Console.WriteLine("Name for output file: ");
                string? fileName = Console.ReadLine();
                string fullPath = directoryPath + @"\" + fileName + ".txt";

                if (!File.Exists(fullPath))
                {
                    using (StreamWriter writer = File.CreateText(fullPath))
                    {
                        foreach (var shape in shapeList) {
                            writer.WriteLine("{0},{1},{2}", shape.GetShape(), shape.GetVolume(), shape.GetSquare());
                        }
                        Console.WriteLine("\nFile created");
                    }	
                }
                else {
                    Console.WriteLine("\nFile already exists");
                }
            }        

            else if (userInput == "5")
            {
                /** Reads content of a txt file and parses content to shapeList **/ 
                Console.WriteLine("Filename: ");
                string? fileName = Console.ReadLine();
                string fullPath = directoryPath + @"\" + fileName;

                if (File.Exists(fullPath))
                    {
                    StreamReader reader = new(fullPath);
                    shapeList.Clear();
                    string? line = reader.ReadLine();
                    while (line != null)
                        {
                        string[] shapeValues = line.Split(',');
                        shapeList.Add(new Shape(shapeValues[0], Convert.ToInt32(shapeValues[1]), Convert.ToInt32(shapeValues[2])));
                        line = reader.ReadLine();
                        }
                    Console.WriteLine("\nFile parsed successfully");
                    reader.Close();
                }        
                else {
                    Console.WriteLine("\nFile does not exist");
                }
            }

            else if (userInput == "6")
            {
                /** Exits program **/ 
                menu = false;
            }

            if (userInput != "6")
            {           
            Console.WriteLine(" \nPress any key to continue");
            Console.ReadLine();
            }
        }
    }
}
