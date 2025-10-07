using System;
using System.Collections.Generic;
using System.Linq;

class AdvancedCalendar
{
    private static Dictionary<int, string> months = new Dictionary<int, string>()
    {
        {1, "Январь"}, {2, "Февраль"}, {3, "Март"}, {4, "Апрель"},
        {5, "Май"}, {6, "Июнь"}, {7, "Июль"}, {8, "Август"},
        {9, "Сентябрь"}, {10, "Октябрь"}, {11, "Ноябрь"}, {12, "Декабрь"}
    };

    private static Dictionary<int, int> daysInMonth = new Dictionary<int, int>()
    {
        {1, 31}, {2, 28}, {3, 31}, {4, 30}, {5, 31}, {6, 30},
        {7, 31}, {8, 31}, {9, 30}, {10, 31}, {11, 30}, {12, 31}
    };

    // Праздничные дни (месяц, день)
    private static List<(int month, int day)> holidays = new List<(int, int)>
    {
        (1, 1), (1, 2), (1, 3), (1, 4), (1, 5), (1, 6), (1, 7), (1, 8),
        (2, 23), (3, 8), (5, 1), (5, 2), (5, 3), (5, 4), (5, 5), (5, 8), (5, 9), (5, 10),
        (6, 12), (11, 4)
    };

    static void Main()
    {
        Console.WriteLine("kalendar 2024\n");
        
        while (true)
        {
            DisplayMainMenu();
            var choice = GetMenuChoice(1, 6);
            
            switch (choice)
            {
                case 1:
                    CheckSpecificDay();
                    break;
                case 2:
                    DisplayMonthCalendar();
                    break;
                case 3:
                    CountWorkingDays();
                    break;
                case 4:
                    FindNextHoliday();
                    break;
                case 5:
                    CompareMonths();
                    break;
                case 6:
                    Console.WriteLine("До свидания!");
                    return;
            }
            
            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
            Console.Clear();
        }
    }
    
    static void DisplayMainMenu()
    {
        Console.WriteLine("Выберите функцию:");
        Console.WriteLine("1. Проверить конкретный день");
        Console.WriteLine("2. Показать календарь месяца");
        Console.WriteLine("3. Посчитать рабочие дни в периоде");
        Console.WriteLine("4. Найти следующий выходной");
        Console.WriteLine("5. Сравнить два месяца");
        Console.WriteLine("6. Выход");
        Console.Write("Ваш выбор: ");
    }
    
    static int GetMenuChoice(int min, int max)
    {
        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < min || choice > max)
        {
            Console.Write($"Ошибка! Введите число от {min} до {max}: ");
        }
        return choice;
    }
    
    // ФУНКЦИЯ 1: Проверка конкретного дня
    static void CheckSpecificDay()
    {
        Console.WriteLine("\n=== ПРОВЕРКА ДНЯ ===\n");
        
        int month = GetMonth();
        int day = GetDay(month);
        int firstDayOfWeek = GetFirstDayOfWeek();
        
        CheckDayType(month, day, firstDayOfWeek);
    }
    
    // ФУНКЦИЯ 2: Отображение календаря месяца
    static void DisplayMonthCalendar()
    {
        Console.WriteLine("\n=== КАЛЕНДАРЬ МЕСЯЦА ===\n");
        
        int month = GetMonth();
        int firstDayOfWeek = GetFirstDayOfWeek();
        int days = GetDaysInMonth(month);
        
        Console.WriteLine($"\n=== {months[month].ToUpper()} ===\n");
        Console.WriteLine("Пн Вт Ср Чт Пт Сб Вс");
        Console.WriteLine(new string('-', 21));
        
        // Пустые клетки до первого дня
        for (int i = 1; i < firstDayOfWeek; i++)
        {
            Console.Write("   ");
        }
        
        // Выводим дни
        for (int day = 1; day <= days; day++)
        {
            bool isHoliday = IsHoliday(month, day) || IsWeekend(month, day, firstDayOfWeek);
            Console.ForegroundColor = isHoliday ? ConsoleColor.Red : ConsoleColor.White;
            Console.Write($"{day,2} ");
            Console.ResetColor();
            
            if ((day + firstDayOfWeek - 1) % 7 == 0)
            {
                Console.WriteLine();
            }
        }
        Console.WriteLine();
        
        // Легенда
        Console.WriteLine("\nЛегенда: ");
        Console.Write("Рабочие дни: "); 
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("белый");
        Console.ResetColor();
        Console.Write("Выходные: ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("красный");
        Console.ResetColor();
    }
    
    // ФУНКЦИЯ 3: Подсчет рабочих дней в периоде
    static void CountWorkingDays()
    {
        Console.WriteLine("\n=== ПОДСЧЕТ РАБОЧИХ ДНЕЙ ===\n");
        
        Console.WriteLine("Введите начальную дату:");
        int startMonth = GetMonth();
        int startDay = GetDay(startMonth);
        
        Console.WriteLine("Введите конечную дату:");
        int endMonth = GetMonth();
        int endDay = GetDay(endMonth);
        
        int firstDayOfWeek = GetFirstDayOfWeek();
        
        if (startMonth > endMonth || (startMonth == endMonth && startDay > endDay))
        {
            Console.WriteLine("Ошибка! Начальная дата должна быть раньше конечной.");
            return;
        }
        
        int workingDays = 0;
        int totalDays = 0;
        int currentMonth = startMonth;
        int currentDay = startDay;
        
        while (currentMonth < endMonth || (currentMonth == endMonth && currentDay <= endDay))
        {
            totalDays++;
            if (!IsHoliday(currentMonth, currentDay) && !IsWeekend(currentMonth, currentDay, firstDayOfWeek))
            {
                workingDays++;
            }
            
            currentDay++;
            if (currentDay > GetDaysInMonth(currentMonth))
            {
                currentDay = 1;
                currentMonth++;
            }
        }
        
        Console.WriteLine($"\nРезультат:");
        Console.WriteLine($"Период: {startDay} {months[startMonth]} - {endDay} {months[endMonth]}");
        Console.WriteLine($"Всего дней: {totalDays}");
        Console.WriteLine($"Рабочих дней: {workingDays}");
        Console.WriteLine($"Выходных дней: {totalDays - workingDays}");
    }
    
    // ФУНКЦИЯ 4: Поиск следующего выходного
    static void FindNextHoliday()
    {
        Console.WriteLine("\n=== ПОИСК СЛЕДУЮЩЕГО ВЫХОДНОГО ===\n");
        
        Console.WriteLine("Введите текущую дату:");
        int currentMonth = GetMonth();
        int currentDay = GetDay(currentMonth);
        int firstDayOfWeek = GetFirstDayOfWeek();
        
        int foundMonth = currentMonth;
        int foundDay = currentDay + 1;
        
        while (true)
        {
            if (foundDay > GetDaysInMonth(foundMonth))
            {
                foundDay = 1;
                foundMonth++;
                if (foundMonth > 12) foundMonth = 1;
            }
            
            if (IsHoliday(foundMonth, foundDay) || IsWeekend(foundMonth, foundDay, firstDayOfWeek))
            {
                string dayName = GetDayName(foundMonth, foundDay, firstDayOfWeek);
                Console.WriteLine($"\nСледующий выходной: {foundDay} {months[foundMonth]} ({dayName})");
                
                if (IsHoliday(foundMonth, foundDay))
                    Console.WriteLine("раздничный день!");
                else
                    Console.WriteLine("Выходной день");
                break;
            }
            
            foundDay++;
        }
    }
    
    // ФУНКЦИЯ 5: Сравнение двух месяцев
    static void CompareMonths()
    {
        Console.WriteLine("\n=== СРАВНЕНИЕ МЕСЯЦЕВ ===\n");
        
        Console.WriteLine("Первый месяц:");
        int month1 = GetMonth();
        Console.WriteLine("Второй месяц:");
        int month2 = GetMonth();
        
        int firstDayOfWeek = GetFirstDayOfWeek();
        
        int workingDays1 = CountWorkingDaysInMonth(month1, firstDayOfWeek);
        int workingDays2 = CountWorkingDaysInMonth(month2, firstDayOfWeek);
        int holidays1 = GetDaysInMonth(month1) - workingDays1;
        int holidays2 = GetDaysInMonth(month2) - workingDays2;
        
        Console.WriteLine($"\n=== СРАВНЕНИЕ {months[month1]} vs {months[month2]} ===");
        Console.WriteLine($"Рабочих дней: {workingDays1} vs {workingDays2}");
        Console.WriteLine($"Выходных дней: {holidays1} vs {holidays2}");
        Console.WriteLine($"Всего дней: {GetDaysInMonth(month1)} vs {GetDaysInMonth(month2)}");
        
        if (workingDays1 > workingDays2)
            Console.WriteLine($"В {months[month1]} больше рабочих дней на {workingDays1 - workingDays2}");
        else if (workingDays2 > workingDays1)
            Console.WriteLine($"В {months[month2]} больше рабочих дней на {workingDays2 - workingDays1}");
        else
            Console.WriteLine("Количество рабочих дней одинаковое");
    }
    
    // Вспомогательные методы
    static int GetMonth()
    {
        Console.WriteLine("Доступные месяцы:");
        foreach (var m in months)
        {
            Console.WriteLine($"{m.Key}. {m.Value}");
        }
        
        int month;
        while (true)
        {
            Console.Write("Выберите месяц (1-12): ");
            if (int.TryParse(Console.ReadLine(), out month) && month >= 1 && month <= 12)
                return month;
            Console.WriteLine("Ошибка! Введите число от 1 до 12.");
        }
    }
    
    static int GetDay(int month)
    {
        int maxDays = GetDaysInMonth(month);
        int day;
        while (true)
        {
            Console.Write($"Введите день (1-{maxDays}): ");
            if (int.TryParse(Console.ReadLine(), out day) && day >= 1 && day <= maxDays)
                return day;
            Console.WriteLine($"Ошибка! Введите число от 1 до {maxDays}.");
        }
    }
    
    static int GetFirstDayOfWeek()
    {
        int dayOfWeek;
        while (true)
        {
            Console.Write("Введите номер дня недели начала месяца (1-Пн, 7-Вс): ");
            if (int.TryParse(Console.ReadLine(), out dayOfWeek) && dayOfWeek >= 1 && dayOfWeek <= 7)
                return dayOfWeek;
            Console.WriteLine("Ошибка! Введите число от 1 до 7.");
        }
    }
    
    static int GetDaysInMonth(int month) => month == 2 ? 29 : daysInMonth[month]; // 2024 - високосный
    
    static bool IsWeekend(int month, int day, int firstDayOfWeek)
    {
        int dayOfWeek = (firstDayOfWeek + day - 2) % 7 + 1;
        return dayOfWeek == 6 || dayOfWeek == 7; // Суббота или воскресенье
    }
    
    static bool IsHoliday(int month, int day) => holidays.Contains((month, day));
    
    static void CheckDayType(int month, int day, int firstDayOfWeek)
    {
        string dayName = GetDayName(month, day, firstDayOfWeek);
        bool isHoliday = IsHoliday(month, day);
        bool isWeekend = IsWeekend(month, day, firstDayOfWeek);
        string dayType = (isHoliday || isWeekend) ? "ВЫХОДНОЙ" : "РАБОЧИЙ";
        
        Console.WriteLine($"\n{day} {months[month]} - это {dayName}");
        Console.WriteLine($"Тип дня: {dayType}");
        
        if (isHoliday) Console.WriteLine("Праздничный день!");
        else if (isWeekend) Console.WriteLine("Выходной день");
        else Console.WriteLine("Рабочий день");
    }
    
    static string GetDayName(int month, int day, int firstDayOfWeek)
    {
        int dayOfWeek = (firstDayOfWeek + day - 2) % 7 + 1;
        return dayOfWeek switch
        {
            1 => "Понедельник", 2 => "Вторник", 3 => "Среда", 4 => "Четверг",
            5 => "Пятница", 6 => "Суббота", 7 => "Воскресенье", _ => "Неизвестно"
        };
    }
    
    static int CountWorkingDaysInMonth(int month, int firstDayOfWeek)
    {
        int workingDays = 0;
        int days = GetDaysInMonth(month);
        
        for (int day = 1; day <= days; day++)
        {
            if (!IsHoliday(month, day) && !IsWeekend(month, day, firstDayOfWeek))
            {
                workingDays++;
            }
        }
        
        return workingDays;
    }
}
