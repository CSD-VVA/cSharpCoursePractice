using System;
using System.Collections.Generic;

class Program
{
    // Is kokiu skaiciu dauginame ieskodami magisko skaiciaus?
    public static int[] multiplyNumbers = { 2, 3, 4, 5, 6 };
    // Kelezenklio magisko skaiciaus ieskome? 2 ... 9
    public static int magicNumberDigitCount = 6;

    static void Main()
    {
        FindMagicNumber();
        Console.ReadKey();
    }

    private static void FindMagicNumber()
    {
        int allNumbersToTestCount = GetCountOfNumbersToTest(magicNumberDigitCount);
        int numberToTest = GetSmallestNumber(magicNumberDigitCount);

        List<int> magicNumbers = new List<int>();

        for (int i = 0; i < allNumbersToTestCount; i++)
        {
            if (AreAllNumberDigitsDifferent(numberToTest))
            {
                if (TestTheNumber(numberToTest))
                {
                    magicNumbers.Add(numberToTest);
                }
            }
            numberToTest++;
        }

        if (magicNumbers.Count == 0)
        {
            Console.WriteLine($"Nera tokio {magicNumberDigitCount}-zenklio magisko skaiciaus.");
        }
        else
        {
            foreach (int magicNumber in magicNumbers)
            {
                PrintResults(magicNumber);
            }
        }
    }

    private static int[] NumberToArray(int number)
    {
        int digitCount = GetDigitCount(number);
        int[] digitArray = new int[digitCount];

        for (int i = digitCount - 1; i >= 0; i--)
        {
            digitArray[i] = GetLastDigitOfANumber(number);
            number = DropLastDigitFromNumber(number);
        }
        return digitArray;
    }

    private static int GetDigitCount(int number)
    {
        return number.ToString().Length;
    }

    private static int GetLastDigitOfANumber(int number)
    {
        return number % 10;
    }

    private static int DropLastDigitFromNumber(int number)
    {
        return Convert.ToInt32(Math.Floor(0.1 * number));
    }

    private static int GetSmallestNumber(int numberDigitCount)
    {
        return Convert.ToInt32(Math.Pow(10, numberDigitCount - 1));
    }

    private static int GetBiggestNumber(int numberDigitCount)
    {
        return Convert.ToInt32(Math.Pow(10, numberDigitCount)) - 1;
    }

    private static int GetCountOfNumbersToTest(int digitCount)
    {
        return GetBiggestNumber(digitCount) - GetSmallestNumber(digitCount);
    }

    private static bool TestTheNumber(int number)
    {
        List<bool> testResults = new List<bool>();

        foreach (int multiplyNumber in multiplyNumbers)
        {
            int testNumber = number * multiplyNumber;

            if (AreAllNumberDigitsDifferent(testNumber) && 
                GetDigitCount(testNumber) == GetDigitCount(number))
            {
                if (DoTwoNumbersHaveSameDigits(number, testNumber))
                {
                    testResults.Add(true);
                }
                else
                {
                    testResults.Add(false);
                }
            }
            else
            {
                testResults.Add(false);
            }
        }
        return !testResults.Contains(false);
    }

    private static bool AreAllNumberDigitsDifferent(int number)
    {
        int[] digitArray = NumberToArray(number);
        int digitCount = GetDigitCount(number);

        for (int i = 0; i < digitCount; i++)
        {
            for (int j = 0; j < digitCount; j++)
            {
                if (i != j)
                {
                    if (digitArray[i] == digitArray[j])
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    private static bool DoTwoNumbersHaveSameDigits(int number1, int number2)
    {
        int[] digitArray1 = NumberToArray(number1);
        int[] digitArray2 = NumberToArray(number2);

        int digitCount = GetDigitCount(number1);

        for (int i = 0; i < digitCount; i++)
        {
            List<int> testedDigits = new List<int>();

            for (int j = 0; j < digitCount; j++)
            {
                if (digitArray1[i] == digitArray2[j])
                {
                    testedDigits.Add(digitArray2[j]);
                }
            }

            if (testedDigits.Count != 1)
            {
                return false;
            }
        }
        return true;
    }

    private static void PrintResults(int number)
    {
        Console.WriteLine($"\n{magicNumberDigitCount}-zenklis magiskas skaicius yra:");
        Console.WriteLine("    " + number + "\n");

        for (int i = 0; i < multiplyNumbers.Length; i++)
        {
            Console.WriteLine($"x{multiplyNumbers[i]}: {number * multiplyNumbers[i]}");
        }
    }
}