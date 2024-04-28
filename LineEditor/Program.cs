using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

class TextEditor
{
    private List<string> lines;
    private string filename;
    private const string AllowedExtension = ".txt"; //Check for the allowed file extensions to load
    private const long MaxFileSize = 1048576; // 1 MB : Maximum file size allowed to load is 1 MB. It is for security reason

    public TextEditor(string filename)
    {
        this.filename = filename;
        this.lines = new List<string>();
    }

    public async Task<bool> LoadFileAsync()
    {
        try
        {
            if (!CheckFileExtension(filename))
            {
                Console.WriteLine($"Invalid file extension. Only {AllowedExtension} files are allowed.");
                return false;
            }
            //Validate if the file size within 1MB or not 
            FileInfo fileInfo = new FileInfo(filename);
            if (fileInfo.Length > MaxFileSize)
            {
                Console.WriteLine($"File size exceeds the maximum allowed size of {MaxFileSize} bytes.");
                return false;
            }
            //Read the input file
            using (StreamReader reader = new StreamReader(filename))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    lines.Add(line);
                }
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("File not found.");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading file: {ex.Message}");
            return false;
        }
        return true;
    }

    private bool CheckFileExtension(string filename)
    {
        string extension = Path.GetExtension(filename);
        return string.Equals(extension, AllowedExtension, StringComparison.OrdinalIgnoreCase);
    }
    public async Task SaveFileAsync()
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (string line in lines)
                {
                    await writer.WriteLineAsync(line);
                }
            }
            Console.WriteLine("File saved successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving file: {ex.Message}");
        }
    }

    public void DisplayFile()
    {
        for (int i = 0; i < lines.Count; i++)
        {
            Console.WriteLine($"{i + 1}: {lines[i]}");
        }
    }

    public async Task InsertLineAsync(int lineNumber, string text)
    {
        lines.Insert(lineNumber - 1, text);
    }

    public async Task DeleteLineAsync(int lineNumber)
    {
        lines.RemoveAt(lineNumber - 1);
    }

    public async Task RunEditorAsync()
    {
        bool fileLoaded = await LoadFileAsync();
        if(!fileLoaded) { return ; }

        Console.WriteLine("File loaded successfully");

        Console.Write("\nPlease enter following command(s) for corresponding operation(s)" + Environment.NewLine +
               "list  : To display all lines" + Environment.NewLine +
               "ins n : To insert at line numbur n" + Environment.NewLine +
               "del n : To delete line number n" + Environment.NewLine +
               "save  : To save the file: " + Environment.NewLine +
               "quit  : To quit from the app" + Environment.NewLine);

        while (true)
        {
            Console.WriteLine();
            Console.Write(" a>> ");
            string[] command = Console.ReadLine()!.Trim().Split(' ');

            if (command.Length == 0)
                continue;

            string operation = command[0].ToLower();

            switch (operation)
            {
                case "list":
                    DisplayFile();
                    break;
                case "ins":
                    try
                    {
                        int lineNumber = int.Parse(command[1]);
                        if ((lines.Count + 1) >= lineNumber)
                        {
                            Console.WriteLine("Enter text to insert");
                            string text = Console.ReadLine()!;
                            await InsertLineAsync(lineNumber, text);
                            Console.WriteLine("Inserted successfully");
                        }
                        else
                            Console.WriteLine("Out of index to insert. Please check the input index");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Invalid command format for insert.");
                    }
                    break;
                case "del":
                    try
                    {
                        int lineNumber = int.Parse(command[1]);
                        if (lines.Count >= lineNumber)
                        {
                            await DeleteLineAsync(lineNumber);
                            Console.WriteLine("Deleted successfully");
                        }
                        else
                            Console.WriteLine("Out of index to delete. Please check the input index");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Invalid command format for delete.");
                    }
                    break;
                case "save":
                    await SaveFileAsync();
                    break;
                case "quit":
                    return;
                default:
                    Console.WriteLine("Invalid command.");
                    break;
            }
        }
    }
}

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Enter the full file path to load, for example : C:\\temp\\myfile.txt");
        Console.Write(" a>> ");
        string filename = Console.ReadLine()!;
        if (filename == string.Empty)
        {
            Console.WriteLine("File name cannot be empty");
            return;
        }
        TextEditor editor = new TextEditor(filename);
        await editor.RunEditorAsync();

    }
}