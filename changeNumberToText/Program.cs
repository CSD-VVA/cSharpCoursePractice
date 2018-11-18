using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        // min skaicius 
        const long fromNumber = -9999999999;
        // max skaicius 
        const long toNumber = 9999999999;

        string inputString = "";
        long inputNumber = 0;

        Console.Write("Sveiki!");

        while (inputString != " ")
        {
            Console.WriteLine($"\n(Enter SPACE to exit.)\nIveskite skaiciu nuo {fromNumber} iki {toNumber}: ");
            inputString = Console.ReadLine();

            if (CheckIfGoodNumber(inputString))
            {
                Console.WriteLine("Skaicius teisingas!\n");

                inputNumber = Convert.ToInt64(inputString);

                if (CheckIfNumberInRange(fromNumber, toNumber, inputNumber))
                {
                    Console.WriteLine("Skaicius {0} zodziais: {1}", inputNumber, ChangeNumberToText(inputNumber));
                }
                else
                {
                    Console.WriteLine($"Blogas skaicius {inputNumber}, iveskite skaiciu reziuose: {fromNumber}..{toNumber}");
                }
            }
            else
            {
                Console.WriteLine($"Ivesti duomenys: {inputString} nera skaicius!");
            }
        }
        Console.WriteLine("\nAciu uz demesi, viso gero.");
        Console.ReadKey();
    }

    // bendra funkcija apjungti visom funkcijom kurias jus sukursit.
    static string ChangeNumberToText(long number)
    {
        string negative = IsNegative(number);
        string numberAsText = "";

        long absNumber = Math.Abs(number);

        if (absNumber < 10)
        {
            numberAsText = ChangeOnesToText(absNumber);
        }
        else if (absNumber < 20)
        {
            numberAsText = ChangeTeensToText(absNumber);
        }
        else if (absNumber < 100)
        {
            numberAsText = ChangeTensToText(absNumber);
        }
        else if (absNumber < 1000)
        {
            numberAsText = ChangeHundredsToText(absNumber);
        }
        else if (absNumber < 1000000)
        {
            numberAsText = ChangeThousandsToText(absNumber);
        }
        else if (absNumber < 1000000000)
        {
            numberAsText = ChangeMillionsToText(absNumber);
        }
        else if (absNumber < 10000000000)
        {
            numberAsText = ChangeBillionsToText(absNumber);
        }
        return negative + numberAsText;
    }

    // funkcija gauna string skaiciu, patikrina ar skaicius teisingu formatu. Pvz: "123", "-123" grazina true. "12a3", "1-23" grazina false.
    static bool CheckIfGoodNumber(string dataToCheck)
    {
        char[] charArray = dataToCheck.ToCharArray();
        char[] validChars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        bool result = true;

        if (dataToCheck == "" ||
            (charArray[0] != '-' && !validChars.Contains(charArray[0])))
        {
            result = false;
        }
        else
        {
            for (int i = 1; i < charArray.Length; i++)
            {
                if (!validChars.Contains(charArray[i]))
                {
                    result = false;
                    break;
                }
            }
        }
        return result;
    }

    // funkcija gauna true jei skaicius checkNumber yar tarp fromNumber ir toNumber (imtinai)
    private static bool CheckIfNumberInRange(long fromNumber, long toNumber, long checkNumber)
    {
        if (checkNumber >= fromNumber && checkNumber <= toNumber)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Minuso zenklas
    private static string IsNegative(long number)
    {
        if (number < 0)
        {
            return "minus ";
        }
        else
        {
            return "";
        }
    }

    // TODO : sukurti funkcija kuri grazina skaiciu -9...9 zodziais - changeOnesToText
    static string ChangeOnesToText(long number)
    {
        string[] digitsAsText = new string[] { "nulis", "vienas", "du", "trys",
            "keturi", "penki", "sesi", "septyni", "astuoni", "devyni" };

        Dictionary<long, string> dicDigitsAsText = new Dictionary<long, string>();

        for (int i = 0; i < digitsAsText.Length; i++)
        {
            dicDigitsAsText.Add(i, digitsAsText[i]);
        }

        return dicDigitsAsText[number];
    }

    // TODO : sukurti funkcija kuri grazina skaiciu -19...19 zodziais - changeTeensToText
    static string ChangeTeensToText(long number)
    {
        string[] teensAsText = new string[] { "desimt", "vienuolika", "dvylika", "trylika",
            "keturiolika", "penkiolika", "sesiolika", "septyniolika", "astuoniolika", "devyniolika" };

        Dictionary<long, string> dicTeensAsText = new Dictionary<long, string>();

        for (int i = 0; i < teensAsText.Length; i++)
        {
            dicTeensAsText.Add(i + 10, teensAsText[i]);
        }

        return dicTeensAsText[number];
    }

    // TODO : sukurti funkcija kuri grazina skaiciu -99...99 zodziais - changeTensToText
    static string ChangeTensToText(long number)
    {
        string[] tensAsText = new string[] { "dvidesimt", "trisdesimt", "keturesdesimt", "penkiasdesimt",
            "sesiasdesimt", "septyniasdesimt", "astuoniasdesimt", "devyniasdesimt" };

        Dictionary<long, string> dicTensAsText = new Dictionary<long, string>();

        for (int i = 0; i < tensAsText.Length; i++)
        {
            dicTensAsText.Add(i + 2, tensAsText[i]);
        }

        long tensDigit = DropOneDigitFromEnd(number);
        long ones = OnesFromNumber(number);

        string text = dicTensAsText[tensDigit];

        if (ones != 0)
        {
            text += " " + ChangeOnesToText(ones);
        }
        return text;
    }

    // TODO : sukurti funkcija kuri grazina skaiciu -999...999 zodziais - changeHundredsToText
    static string ChangeHundredsToText(long number)
    {
        string text;

        long hundreds = DropTwoDigitsFromEnd(number);

        string hundredsWord = WhichWordToUse(hundreds, "simtas");

        text = ChangeOnesToText(hundreds) + " " + hundredsWord;

        long tens = TensFromNumber(number);

        if (tens != 0)
        {
            if (tens < 10)
            {
                text += " " + ChangeOnesToText(tens);
            }
            else if (tens < 20)
            {
                text += " " + ChangeTeensToText(tens);
            }
            else
            {
                text += " " + ChangeTensToText(tens);
            }
        }
        return text;
    }

    // TODO : sukurti funkcija kuri grazina skaiciu -999999...999999 zodziais - changeThousandsToText
    static string ChangeThousandsToText(long number)
    {
        long thousands = ThousandsFromNumber(number);

        string text = ToText(thousands, "tukstantis");

        long hundreds = HundredsFromNumber(number);

        text += " " + ToText(hundreds, "");

        return text;
    }

    // TODO : sukurti funkcija kuri grazina skaiciu -999999999...999999999 zodziais - changeMillionsToText
    static string ChangeMillionsToText(long number)
    {
        long millions = MillionsFromNumber(number);

        string text = ToText(millions, "milijonas");

        long thousands = ThousandsFromNumber(number);

        text += " " + ToText(thousands, "tukstantis");

        long hundreds = HundredsFromNumber(number);

        text += " " + ToText(hundreds, "");

        return text;
    }

    // TODO : sukurti funkcija kuri grazina skaiciu -9999999999...9999999999 zodziais - changeBilllionsToText
    static string ChangeBillionsToText(long number)
    {
        long billions = number;

        for (int i = 0; i < 3; i++)
        {
            billions = DropThreeDigitsFromEnd(billions);
        }

        string billionsWord = WhichWordToUse(billions, "milijardas");

        string text = ChangeOnesToText(billions) + " " + billionsWord + 
            " " + ChangeMillionsToText(number);

        return text;
    }

    // Keisti i teksta
    static string ToText(long number, string word)
    {
        string text = "";
        string whichWord;

        if (word == "")
        {
            whichWord = "";
        }
        else
        {
            whichWord = " " + WhichWordToUse(number, word);
        }

        if (number != 0)
        {
            if (number < 10)
            {
                text = ChangeOnesToText(number) + whichWord;
            }
            else if (number < 20)
            {
                text = ChangeTeensToText(number) + whichWord;
            }
            else if (number < 100)
            {
                text = ChangeTensToText(number) + whichWord;
            }
            else
            {
                text = ChangeHundredsToText(number) + whichWord;
            }
        }
        return text;
    }

    // Kuria zodzio galune naudoti
    static int WhichWordIndex(long number)
    {
        int wordIndex;

        if (number % 10 == 0 ||
            (number > 10 && number < 20))
        {
            wordIndex = 0;
        }
        else if (number > 100 &&
            TensFromNumber(number) > 10 &&
            TensFromNumber(number) < 20)
        {
            wordIndex = 0;
        }
        else if (number % 10 == 1)
        {
            wordIndex = 1;
        }
        else
        {
            wordIndex = 2;
        }
        return wordIndex;
    }

    // Kuri zodi naudoti
    static string WhichWordToUse(long number, string word)
    {
        int index = WhichWordIndex(number);

        string[] hundredsWords = { "simtu", "simtas", "simtai" };
        string[] thousandsWords = { "tukstanciu", "tukstantis", "tukstanciai" };
        string[] millionsWords = { "milijonu", "milijonas", "milijonai" };
        string[] billionsWords = { "milijardu", "milijardas", "milijardai" };

        if (word == "simtas")
        {
            return hundredsWords[index];
        }
        else if (word == "tukstantis")
        {
            return thousandsWords[index];
        }
        else if (word == "milijonas")
        {
            return millionsWords[index];
        }
        else
        {
            return billionsWords[index];
        }
    }

    // Numesti paskutini skaiciu
    static long DropOneDigitFromEnd(long number)
    {
        return Convert.ToInt64(Math.Floor(0.1 * number));
    }

    // Numesti du paskutinius skaicius
    static long DropTwoDigitsFromEnd(long number)
    {
        return Convert.ToInt64(Math.Floor(0.01 * number));
    }

    // Numesti tris paskutinius skaicius
    static long DropThreeDigitsFromEnd(long number)
    {
        return Convert.ToInt64(Math.Floor(0.001 * number));
    }

    // Vienetai is skaiciaus
    static long OnesFromNumber(long number)
    {
        return number % 10;
    }

    // Desimtis is skaiciaus
    static long TensFromNumber(long number)
    {
        return number % 100;
    }

    // Simtai is skaiciaus
    static long HundredsFromNumber(long number)
    {
        return number % 1000;
    }

    // Tukstanciai is skaiciaus
    static long ThousandsFromNumber(long number)
    {
        long thousands = DropThreeDigitsFromEnd(number);
        thousands = thousands - DropThreeDigitsFromEnd(thousands) * 1000;

        return thousands;
    }

    // Milijonai is skaiciaus
    static long MillionsFromNumber(long number)
    {
        long millions = DropThreeDigitsFromEnd(DropThreeDigitsFromEnd(number));
        millions = millions - DropThreeDigitsFromEnd(millions) * 1000;

        return millions;
    }
}