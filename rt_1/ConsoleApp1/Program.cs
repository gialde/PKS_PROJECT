using System;

class SimpleCalculator
{
    private double currentValue = 0;
    private double memoryValue = 0;
    private string lastOperation = "";

    public void Run()
    {

        Console.WriteLine("Доступные операции: +, -, *, /, %, 1/x, x^2, sqrt, M+, M-, MR, C, AC, exit");
        
        while (true)
        {
            Console.WriteLine($"\nТекущее значение: {currentValue}");
            Console.Write("Введите операцию или число: ");
            string input = Console.ReadLine().Trim().ToLower();

            if (input == "exit") break;
            
            ProcessInput(input);
        }
    }

    private void ProcessInput(string input)
    {
        try
        {
            switch (input)
            {
                case "+": case "-": case "*": case "/": case "%":
                    lastOperation = input;
                    Console.Write("Введите число: ");
                    if (double.TryParse(Console.ReadLine(), out double num))
                        PerformOperation(num);
                    break;
                
                case "1/x":
                    if (currentValue != 0)
                        currentValue = 1 / currentValue;
                    else
                        Console.WriteLine("Ошибка: деление на ноль");
                    break;
                
                case "x^2":
                    currentValue *= currentValue;
                    break;
                
                case "sqrt":
                    if (currentValue >= 0)
                        currentValue = Math.Sqrt(currentValue);
                    else
                        Console.WriteLine("Ошибка: отрицательное число под корнем");
                    break;
                
                case "m+":
                    memoryValue += currentValue;
                    Console.WriteLine($"Значение в памяти: {memoryValue}");
                    break;
                
                case "m-":
                    memoryValue -= currentValue;
                    Console.WriteLine($"Значение в памяти: {memoryValue}");
                    break;
                
                case "mr":
                    currentValue = memoryValue;
                    break;
                
                case "c":
                    currentValue = 0;
                    break;
                
                case "ac":
                    currentValue = 0;
                    memoryValue = 0;
                    lastOperation = "";
                    break;
                
                default:
                    if (double.TryParse(input, out double number))
                        currentValue = number;
                    else
                        Console.WriteLine("Неверный ввод");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    private void PerformOperation(double num)
    {
        switch (lastOperation)
        {
            case "+": currentValue += num; break;
            case "-": currentValue -= num; break;
            case "*": currentValue *= num; break;
            case "/": 
                if (num != 0)
                    currentValue /= num;
                else
                    Console.WriteLine("Ошибка: деление на ноль");
                break;
            case "%": currentValue %= num; break;
        }
    }
}

class Program
{
    static void Main()
    {
        SimpleCalculator calculator = new SimpleCalculator();
        calculator.Run();
    }
}