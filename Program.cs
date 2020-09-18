using System;

namespace IsPieceMoveFinder
{
    class Program
    {
        static int[] GetCorrectPlace()
        {
            bool isTrue;
            int w, h;
            int[] place = {0, 0};
            string str;

            do
            {
                str = Console.ReadLine();

                isTrue = (str.Length == 2);
                if (isTrue)
                {
                    isTrue = char.IsLetter(str[0]) && char.IsDigit(str[1]);
                    if (isTrue)
                    {
                        h = Convert.ToInt32(str[1]) - 48;
                        w = str[0]-64;
                        if (w < 1 && w > 8)
                        {
                            isTrue = false;
                        } else
                        {
                            place[0] = w;
                            place[1] = h;
                        }
                    }
                }

                if (!isTrue) Console.WriteLine("Пожалуйста, введите корректную позицию (например D4)");
            } while (!isTrue);

            return place;
        }

        static void Main(string[] args)
        {
            string pieceStr, placeStr;
            int pieceNum;
            bool isTrue;
            string[] pieceName = { "Король", "Ферзь", "Ладья", "Слон", "Конь", "Пешка" };
            int[] place_1 = { 0, 0 };


            Console.WriteLine("Выберите фигуру из списка: 1.Король 2.Ферзь 3.Ладья 4.Слон 5.Конь 6.Пешка");
            do
            {
                Console.Write("Выбрана фигура: ");
                pieceStr = Console.ReadLine();

                isTrue = int.TryParse(pieceStr, out pieceNum);
                if (isTrue) 
                    if (pieceNum < 1 || pieceNum > 6) isTrue = false;

                if (!isTrue) Console.WriteLine("Пожалуйста, введите число от 1 до 6, которое будет соответствовать номеру фигуры из списка выше.");
            } while (!isTrue);
            Console.WriteLine("(" + pieceName[pieceNum - 1] + ")");

            Console.Write("Введите начальное положение фигуры: ");
            place_1 = GetCorrectPlace();
            Console.WriteLine(place_1[0]);
            Console.WriteLine(place_1[1]);

            Console.Write("Введите конечное положение фигуры: ");
        }
    }
}
