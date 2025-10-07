using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.WriteLine("kalendar");
        
        int firstDayOfWeek = GetFirstDayOfWeek();
        
        DisplayCalendar(firstDayOfWeek);
        
        ProcessDayQueries(firstDayOfWeek);
    }
    
    static int GetFirstDayOfWeek()
    {
        int dayOfWeek;
        while (true)
        {
            Console.Write("\nВведите номер дня недели, с которого начинается месяц (1-7): ");
            if (int.TryParse(Console.ReadLine(), out dayOfWeek) && dayOfWeek >= 1 && dayOfWeek <= 7)
            {
                return dayOfWeek;
            }
            Console.WriteLine("Введите число от 1 до 7.");
        }
    }
    
    static void DisplayCalendar(int firstDayOfWeek)
    {
        Console.WriteLine("\n=== КАЛЕНДАРЬ МАЯ ===");
        Console.WriteLine("Пн Вт Ср Чт Пт Сб Вс");
        
        for (int i = 1; i < firstDayOfWeek; i++)
        {
            Console.Write("   ");
        }
        
        for (int day = 1; day <= 31; day++)
        {
            Console.Write($"{day,2} ");
            
            if ((day + firstDayOfWeek - 1) % 7 == 0)
            {
                Console.WriteLine();
            }
        }
        Console.WriteLine("\n");
    }
    
    static void ProcessDayQueries(int firstDayOfWeek)
    {
        Console.WriteLine("Введите номер дня месяца для проверки (1-31) или '0' для выхода:");
        
        while (true)
        {
            Console.Write("День: ");
            string input = Console.ReadLine();
            
            if (input == "0")
            {
                Console.WriteLine("Программа завершена.");
                break;
            }
            
            if (int.TryParse(input, out int day))
            {
                if (day >= 1 && day <= 31)
                {
                    CheckDayType(day, firstDayOfWeek);
                }
                else
                {
                    Console.WriteLine("Ошибка! Введите число от 1 до 31.");
                }
            }
            else
            {
                Console.WriteLine("Ошибка! Введите корректное число.");
            }
        }
    }
    
    static void CheckDayType(int day, int firstDayOfWeek)
    {
        int dayOfWeek = (firstDayOfWeek + day - 2) % 7 + 1;
        
        string dayName = GetDayName(dayOfWeek);
        bool isWeekend = IsWeekend(day, dayOfWeek);
        string dayType = isWeekend ? "ВЫХОДНОЙ" : "РАБОЧИЙ";
        
        Console.WriteLine($"{day} мая - это {dayName}");
        Console.WriteLine($"Тип дня: {dayType}");
        
        if (isWeekend)
        {
            if (day >= 1 && day <= 5)
                Console.WriteLine("→ Праздничные дни с 1 по 5 мая");
            else if (day >= 8 && day <= 10)
                Console.WriteLine("→ Праздничные дни с 8 по 10 мая");
            else if (dayOfWeek == 6)
                Console.WriteLine("→ Суббота");
            else if (dayOfWeek == 7)
                Console.WriteLine("→ Воскресенье");
        }
        Console.WriteLine();
    }
    
    static string GetDayName(int dayOfWeek)
    {
        return dayOfWeek switch
        {
            1 => "Понедельник",
            2 => "Вторник",
            3 => "Среда", 
            4 => "Четверг",
            5 => "Пятница",
            6 => "Суббота",
            7 => "Воскресенье",
            _ => "Неизвестно"
        };
    }
    
    static bool IsWeekend(int day, int dayOfWeek)
    {
        if (dayOfWeek == 6 || dayOfWeek == 7)
            return true;
        
        if ((day >= 1 && day <= 5) || (day >= 8 && day <= 10))
            return true;
        
        return false;
    }
}
