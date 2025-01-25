using System;
using System.Collections.Generic;
using System.IO;


// List of prompts for journal entries
List<string> prompts = new List<string>
{
    "Who was the most interesting person I interacted with today?",
    "What was the best part of my day?",
    "How did I see the hand of the Lord in my life today?",
    "What was the strongest emotion I felt today?",
    "If I had one thing I could do over today, what would it be?"
};

// Method to display the menu options
void DisplayMenu()
{
    Console.WriteLine("1. Write a new entry");
    Console.WriteLine("2. Display the journal");
    Console.WriteLine("3. Save the journal to a file");
    Console.WriteLine("4. Load the journal from a file");
    Console.WriteLine("5. Quit");
    Console.Write("Select an option: ");
}

// Method to get a random prompt from the list
string GetPrompt()
{
    Random random = new Random();
    int index = random.Next(prompts.Count);
    return prompts[index];
}

// Create a new Journal instance
Journal journal = new Journal();
bool running = true;

// Main loop to display the menu and handle user input
while (running)
{
    DisplayMenu();
    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            // Write a new entry
            string prompt = GetPrompt();
            Console.WriteLine(prompt);
            Console.Write("Your response: ");
            string response = Console.ReadLine();
            journal.AddEntry(prompt, response);
            break;
        case "2":
            // Display the journal entries
            journal.DisplayEntries();
            break;
        case "3":
            // Save the journal to a file
            Console.Write("Enter filename to save: ");
            string saveFilename = Console.ReadLine();
            journal.SaveToFile(saveFilename);
            break;
        case "4":
            // Load the journal from a file
            Console.Write("Enter filename to load: ");
            string loadFilename = Console.ReadLine();
            journal.LoadFromFile(loadFilename);
            break;
        case "5":
            // Quit the application
            running = false;
            break;
        default:
            Console.WriteLine("Invalid option. Please try again.");
            break;
    }
}

// Journal class to manage journal entries
public class Journal
{
    public List<JournalEntry> Entries { get; set; } = new List<JournalEntry>();

    // Method to add a new entry to the journal
    public void AddEntry(string prompt, string response)
    {
        Entries.Add(new JournalEntry(prompt, response));
    }

    // Method to display all journal entries
    public void DisplayEntries()
    {
        foreach (var entry in Entries)
        {
            Console.WriteLine(entry.ToString());
        }
    }

    // Recursive method to display entries
    public void DisplayEntriesRecursive(int index = 0)
    {
        if (index < Entries.Count)
        {
            Console.WriteLine(Entries[index].ToString());
            DisplayEntriesRecursive(index + 1);
        }
    }

    // Method to save journal entries to a file
    public void SaveToFile(string filename)
    {
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            foreach (var entry in Entries)
            {
                outputFile.WriteLine(entry.ToString());
            }
        }
    }

    // Method to load journal entries from a file
    public void LoadFromFile(string filename)
    {
        Entries.Clear();
        string[] lines = File.ReadAllLines(filename);
        foreach (string line in lines)
        {
            string[] parts = line.Split('|');
            if (parts.Length == 3)
            {
                string date = parts[0];
                string prompt = parts[1];
                string response = parts[2];
                Entries.Add(new JournalEntry(prompt, response) { Date = date });
            }
        }
    }
}

// JournalEntry class to represent a single journal entry
public class JournalEntry
{
    public string Date { get; set; }
    public string Prompt { get; set; }
    public string Response { get; set; }

    public JournalEntry(string prompt, string response)
    {
        Date = DateTime.Now.ToString("yyyy-MM-dd");
        Prompt = prompt;
        Response = response;
    }

    public override string ToString()
    {
        return $"{Date}|{Prompt}|{Response}";
    }
}
