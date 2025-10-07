using System;

namespace MatrixCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Matrix Calc.");
            
            int n, m;
            Console.Write("Введите количество строк (n): ");
            n = int.Parse(Console.ReadLine());
            Console.Write("Введите количество столбцов (m): ");
            m = int.Parse(Console.ReadLine());

            double[,] matrix1 = new double[n, m];
            double[,] matrix2 = new double[n, m];

            Console.WriteLine("\nВыберите способ заполнения матриц:");
            Console.WriteLine("1 - Ввод с клавиатуры");
            Console.WriteLine("2 - Заполнение случайными числами");
            int choice = int.Parse(Console.ReadLine());

            if (choice == 1)
            {
                FillMatrixFromKeyboard(matrix1, "первой");
                FillMatrixFromKeyboard(matrix2, "второй");
            }
            else if (choice == 2)
            {
                Console.Write("Введите нижнюю границу a: ");
                int a = int.Parse(Console.ReadLine());
                Console.Write("Введите верхнюю границу b: ");
                int b = int.Parse(Console.ReadLine());
                
                FillMatrixRandom(matrix1, a, b);
                FillMatrixRandom(matrix2, a, b);
            }

            Console.WriteLine("\nПервая матрица:");
            PrintMatrix(matrix1);
            Console.WriteLine("\nВторая матрица:");
            PrintMatrix(matrix2);

            Console.WriteLine("\nВыберите операцию:");
            Console.WriteLine("1 - Сложение матриц");
            Console.WriteLine("2 - Умножение матриц");
            int operation = int.Parse(Console.ReadLine());

            if (operation == 1)
            {
                if (matrix1.GetLength(0) == matrix2.GetLength(0) && matrix1.GetLength(1) == matrix2.GetLength(1))
                {
                    double[,] result = AddMatrices(matrix1, matrix2);
                    Console.WriteLine("\nРезультат сложения:");
                    PrintMatrix(result);
                }
                else
                {
                    Console.WriteLine("Ошибка: матрицы должны иметь одинаковые размеры для сложения!");
                }
            }
            else if (operation == 2)
            {
                if (matrix1.GetLength(1) == matrix2.GetLength(0))
                {
                    double[,] result = MultiplyMatrices(matrix1, matrix2);
                    Console.WriteLine("\nРезультат умножения:");
                    PrintMatrix(result);
                }
                else
                {
                    Console.WriteLine("Ошибка: количество столбцов первой матрицы должно равняться количеству строк второй матрицы!");
                }
            }
        }

        static void FillMatrixFromKeyboard(double[,] matrix, string name)
        {
            Console.WriteLine($"\nЗаполнение {name} матрицы:");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write($"Элемент [{i},{j}]: ");
                    matrix[i, j] = double.Parse(Console.ReadLine());
                }
            }
        }

        static void FillMatrixRandom(double[,] matrix, int a, int b)
        {
            Random rand = new Random();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = rand.Next(a, b + 1);
                }
            }
        }

        static void PrintMatrix(double[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

        static double[,] AddMatrices(double[,] matrix1, double[,] matrix2)
        {
            int rows = matrix1.GetLength(0);
            int cols = matrix1.GetLength(1);
            double[,] result = new double[rows, cols];
            
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = matrix1[i, j] + matrix2[i, j];
                }
            }
            return result;
        }

        static double[,] MultiplyMatrices(double[,] matrix1, double[,] matrix2)
        {
            int rows1 = matrix1.GetLength(0);
            int cols1 = matrix1.GetLength(1);
            int cols2 = matrix2.GetLength(1);
            double[,] result = new double[rows1, cols2];
            
            for (int i = 0; i < rows1; i++)
            {
                for (int j = 0; j < cols2; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < cols1; k++)
                    {
                        sum += matrix1[i, k] * matrix2[k, j]; // Сумма произведений элементов строки первой матрицы на элементы столбца второй
                    }
                    result[i, j] = sum;
                }
            }
            return result;
        }
    }
}
