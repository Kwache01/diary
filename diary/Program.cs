using System;
using System.Collections.Generic;

class Program
{
    static List<Note> notes = new List<Note>();
    static int currentDateIndex = 0;

    static void Main(string[] args)
    {
        notes.Add(new Note("Заметка 1", "Сделать практос по ОАиП", new DateTime(2023, 10, 19)));
        notes.Add(new Note("Заметка 2", "Посмотреть фильм", new DateTime(2023, 10, 21)));
        notes.Add(new Note("Заметка 3", "", new DateTime(2023, 10, 23)));

        while (true)
        {
            Console.Clear();
            ShowMenu();
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            switch (keyInfo.Key)
            {
                case ConsoleKey.LeftArrow:
                    ChangeDate(-1);
                    break;
                case ConsoleKey.RightArrow:
                    ChangeDate(1);
                    break;
                case ConsoleKey.Enter:
                    ShowNoteDetails();
                    break;
                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;
            }
        }
    }

    static void ShowMenu()
    {
        DateTime currentDate = GetSelectedDate();
        Console.WriteLine("Ежедневник");
        Console.WriteLine("===========");
        Console.WriteLine($"Дата: {currentDate.ToShortDateString()}\n");

        List<Note> notesForDate = GetNotesForDate(currentDate);
        foreach (Note note in notesForDate)
        {
            Console.WriteLine(note.Title);
        }

        Console.WriteLine("\nИспользуйте стрелки влево/вправо для переключения даты.");
        Console.WriteLine("Нажмите Enter для просмотра подробной информации о заметке.");
        Console.WriteLine("Нажмите Escape для выхода.");
    }

    static void ChangeDate(int increment)
    {
        currentDateIndex += increment;
        if (currentDateIndex < 0)
        {
            currentDateIndex = 0;
        }
        else if (currentDateIndex >= GetUniqueDates().Count)
        {
            currentDateIndex = GetUniqueDates().Count - 1;
        }
    }

    static void ShowNoteDetails()
    {
        DateTime currentDate = GetSelectedDate();
        List<Note> notesForDate = GetNotesForDate(currentDate);

        Console.Clear();
        Console.WriteLine("Подробная информация о заметке");
        Console.WriteLine("==============================\n");

        foreach (Note note in notesForDate)
        {
            Console.WriteLine($"Название: {note.Title}");
            Console.WriteLine($"Описание: {note.Description}");
            Console.WriteLine($"Дата: {note.Date.ToShortDateString()}");
            Console.WriteLine();
        }

        Console.WriteLine("Нажмите любую клавишу для возврата в меню.");
        Console.ReadKey();
    }

    static DateTime GetSelectedDate()
    {
        List<DateTime> uniqueDates = GetUniqueDates();
        return uniqueDates[currentDateIndex];
    }

    static List<DateTime> GetUniqueDates()
    {
        List<DateTime> uniqueDates = new List<DateTime>();
        foreach (Note note in notes)
        {
            if (!uniqueDates.Contains(note.Date))
            {
                uniqueDates.Add(note.Date);
            }
        }
        uniqueDates.Sort();
        return uniqueDates;
    }

    static List<Note> GetNotesForDate(DateTime date)
    {
        List<Note> notesForDate = new List<Note>();
        foreach (Note note in notes)
        {
            if (note.Date == date)
            {
                notesForDate.Add(note);
            }
        }
        return notesForDate;
    }
}

class Note
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }

    public Note(string title, string description, DateTime date)
    {
        Title = title;
        Description = description;
        Date = date;
    }
}
