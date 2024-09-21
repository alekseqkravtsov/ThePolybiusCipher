using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Security;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace InfoDefendLab_1
{
    internal class Cryptography
    {
        private char[,] PolybiusRectangle = new char[8, 9]
        {
                { 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З'},
                { 'И', 'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р'},
                { 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ'},
                { 'Ъ', 'Ы', 'Ь', 'Э', 'Ю', 'Я', 'а', 'б', 'в'},
                { 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к'},
                { 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у'},
                { 'ф', 'ч', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь'},
                { 'э', 'ю', 'я', ' ', '.', ':', '!', '?', ','},
        };

        public void startProgram()
        {
            printMatrix();

            while (true)
            {
                Console.Write("\nВведите сообщение: ");
                string message = Console.ReadLine();


                string lockedMessage = Encrypt(message);

                if (lockedMessage != "")
                {
                    Console.WriteLine("Зашифрованное сообщение: " + lockedMessage);

                    lockedMessage = Decrypt(lockedMessage);
                    Console.WriteLine("Расшифрованное сообщение: " + lockedMessage);
                }
                else
                    Console.WriteLine("Сообщение содержит недопустимый символ для таблицы Полибия.");
            }
        }

        public void printMatrix()
        {
            Console.WriteLine("Таблица полибия:\n");

            int i, j;
            for (i = 0; i < 8; i++)
            {
                for (j = 0; j < 9; j++)
                {
                    Console.Write(PolybiusRectangle[i, j] + " ");
                }
                Console.WriteLine("");
            }
        }

        private string GetIndex(char character)
        {

            string index = "";


            for (int rows = 0; rows < 8; rows++)
            {
                for (int column = 0; column < 9; column++)
                {
                    if (PolybiusRectangle[rows, column] == character)
                    {
                        index += (rows + 1).ToString() + (column + 1).ToString();
                        break;
                    }
                }

                if (index != "")
                    break;
            }

            return index;
        }

        private string GetSymbol(string number)
        {
            string symbol = "";

            if (int.TryParse(number, out int num))
            {
                int row = (num / 10) - 1;
                int column = (num % 10) - 1;

                symbol += PolybiusRectangle[row, column];

            }
            return symbol;

        }

        public string Encrypt(string message)
        {
            string lockedMessage = "";
            char[] characters = message.ToCharArray();

            foreach (var character in characters)
            {
                if (GetIndex(character) == "")
                    return "";
                else
                    lockedMessage += GetIndex(character) + " ";
            }
            return lockedMessage;

        }

        public string Decrypt(string encrypt)
        {
            string message = "";
            string[] numbers = encrypt.Split(' ');

            foreach (var number in numbers)
            {
                message += GetSymbol(number);
            }


            return message;

        }

        static void Main(string[] args)
        {
            Cryptography cryptography = new Cryptography();
            cryptography.startProgram();
        }
    }
}
