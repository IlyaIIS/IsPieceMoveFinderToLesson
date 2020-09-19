using Microsoft.VisualBasic;
using System;
using System.Security.Cryptography.X509Certificates;

namespace IsPieceMoveFinder
{
    class Program
    {
        static int[] GetCorrectPlace() //Требует у пользователя позицию фигуры в корректной форме, а затем раскладывает её на 2 числа в массив
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
                        if ((w < 1 && w > 8) && (h < 1 && h > 8))
                        {
                            isTrue = false;
                        } else
                        {
                            place[0] = w - 1;
                            place[1] = h - 1;
                        }
                    }
                }

                if (!isTrue) Console.WriteLine("Пожалуйста, введите корректную позицию (A1-H8)");
            } while (!isTrue);

            return place;
        }

        static byte[,] GetChessBoard(int[] place, int piece) //Вычисление поля возможных холов фигуры
        {
            byte[,] cb = { { 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0 },
                           { 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0 } };
            int[] placeNow = { 0, 0 };

            if (piece == 1) //Король
            {
                cb[place[0], place[1]] = 2;

                if (place[0] < 7                ) cb[place[0] + 1, place[1]    ] = 1;
                if (place[0] < 7 && place[1] > 0) cb[place[0] + 1, place[1] - 1] = 1;
                if (                place[1] > 0) cb[place[0]    , place[1] - 1] = 1;
                if (place[0] > 0 && place[1] > 0) cb[place[0] - 1, place[1] - 1] = 1;
                if (place[0] > 0                ) cb[place[0] - 1, place[1]    ] = 1;
                if (place[0] > 0 && place[1] < 7) cb[place[0] - 1, place[1] + 1] = 1;
                if (                place[1] < 7) cb[place[0]    , place[1] + 1] = 1;
                if (place[0] < 7 && place[1] < 7) cb[place[0] + 1, place[1] + 1] = 1;
            }else

            if (piece == 2) //Ферзь &#%@#^@#&%%
            {
                //----------------
                placeNow[0] = place[0]; placeNow[1] = place[1];
                while (placeNow[0] < 7 && placeNow[1] > 0)
                {
                    placeNow[0]++;
                    placeNow[1]--;
                    cb[placeNow[0], placeNow[1]] = 0;
                }

                placeNow[0] = place[0]; placeNow[1] = place[1];
                while (placeNow[0] > 0 && placeNow[1] > 0)
                {
                    placeNow[0]--;
                    placeNow[1]--;
                    cb[placeNow[0], placeNow[1]] = 1;
                }

                placeNow[0] = place[0]; placeNow[1] = place[1];
                while (placeNow[0] > 0 && placeNow[1] < 7)
                {
                    placeNow[0]--;
                    placeNow[1]++;
                    cb[placeNow[0], placeNow[1]] = 1;
                }

                placeNow[0] = place[0]; placeNow[1] = place[1];
                while (placeNow[0] < 7 && placeNow[1] < 7)
                {
                    placeNow[0]++;
                    placeNow[1]++;
                    cb[placeNow[0], placeNow[1]] = 1;
                }
                //--------------------
                placeNow[0] = place[0]; placeNow[1] = place[1];
                while (placeNow[0] < 7)
                {
                    placeNow[0]++;
                    cb[placeNow[0], placeNow[1]] = 1;
                }

                placeNow[0] = place[0]; placeNow[1] = place[1];
                while (placeNow[1] > 0)
                {
                    placeNow[1]--;
                    cb[placeNow[0], placeNow[1]] = 1;
                }

                placeNow[0] = place[0]; placeNow[1] = place[1];
                while (placeNow[0] > 0)
                {
                    placeNow[0]--;
                    cb[placeNow[0], placeNow[1]] = 1;
                }

                placeNow[0] = place[0]; placeNow[1] = place[1];
                while (placeNow[1] < 7)
                {
                    placeNow[1]++;
                    cb[placeNow[0], placeNow[1]] = 1;
                }
                //----------------------
            }
            else

            if (piece == 3) //Ладья
            {
                placeNow[0] = place[0]; placeNow[1] = place[1];
                while (placeNow[0] < 7 && placeNow[1] > 0)
                {
                    placeNow[0]++;
                    placeNow[1]--;
                    cb[placeNow[0], placeNow[1]] = 1;
                }

                placeNow[0] = place[0]; placeNow[1] = place[1];
                while (placeNow[0] > 0 && placeNow[1] > 0)
                {
                    placeNow[0]--;
                    placeNow[1]--;
                    cb[placeNow[0], placeNow[1]] = 1;
                }

                placeNow[0] = place[0]; placeNow[1] = place[1];
                while (placeNow[0] > 0 && placeNow[1] < 7)
                {
                    placeNow[0]--;
                    placeNow[1]++;
                    cb[placeNow[0], placeNow[1]] = 1;
                }

                placeNow[0] = place[0]; placeNow[1] = place[1];
                while (placeNow[0] < 7 && placeNow[1] < 7)
                {
                    placeNow[0]++;
                    placeNow[1]++;
                    cb[placeNow[0], placeNow[1]] = 1;
                }
            }else

            if (piece == 4) //Слон
            {
                placeNow[0] = place[0]; placeNow[1] = place[1];
                while (placeNow[0] < 7)
                {
                    placeNow[0]++;
                    cb[placeNow[0], placeNow[1]] = 1;
                }

                placeNow[0] = place[0]; placeNow[1] = place[1];
                while (placeNow[1] > 0)
                {
                    placeNow[1]--;
                    cb[placeNow[0], placeNow[1]] = 1;
                }

                placeNow[0] = place[0]; placeNow[1] = place[1];
                while (placeNow[0] > 0)
                {
                    placeNow[0]--;
                    cb[placeNow[0], placeNow[1]] = 1;
                }

                placeNow[0] = place[0]; placeNow[1] = place[1];
                while (placeNow[1] < 7)
                {
                    placeNow[1]++;
                    cb[placeNow[0], placeNow[1]] = 1;
                }
            }else

            if (piece == 5) //Конь
            {
                if ((place[0] + 2 <= 7) && (place[1] + 1 <= 7)) cb[place[0] + 2, place[1] + 1] = 1;
                if ((place[0] + 2 <= 7) && (place[1] - 1 >= 0)) cb[place[0] + 2, place[1] - 1] = 1;
                if ((place[0] + 1 <= 7) && (place[1] - 2 >= 0)) cb[place[0] + 1, place[1] - 2] = 1;
                if ((place[0] - 1 >= 0) && (place[1] - 2 >= 0)) cb[place[0] - 1, place[1] - 2] = 1;
                if ((place[0] - 2 >= 0) && (place[1] - 1 >= 0)) cb[place[0] - 2, place[1] - 1] = 1;
                if ((place[0] - 2 >= 0) && (place[1] + 1 <= 7)) cb[place[0] - 2, place[1] + 1] = 1;
                if ((place[0] - 1 >= 0) && (place[1] + 2 <= 7)) cb[place[0] - 1, place[1] + 2] = 1;
                if ((place[0] + 1 <= 7) && (place[1] + 2 <= 7)) cb[place[0] + 1, place[1] + 2] = 1;
            }else

            if (piece == 6) //Пешка
            {
                if (place[1] <  7) cb[place[0], place[1] + 1] = 1;
                if (place[1] >  0) cb[place[0], place[1] - 1] = 1;
                if (place[1] == 1) cb[place[0], place[1] + 2] = 1;
                if (place[1] == 6) cb[place[0], place[1] - 2] = 1;
            }


            return cb;
        }


        static void Main(string[] args)
        {
            byte[,] chessBoard = { { 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0 }, 
                                   { 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0 } };
            string pieceStr;
            int pieceNum, k;
            bool isTrue;
            string[] pieceName = { "Король", "Ферзь", "Ладья", "Слон", "Конь", "Пешка" };
            int[] place_1, place_2;


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

            Console.Write("Введите конечное  положение фигуры: ");
            place_2 = GetCorrectPlace();

            chessBoard = GetChessBoard(place_1, pieceNum);
            chessBoard[place_1[0], place_1[1]] = 2;

            if (chessBoard[place_2[0], place_2[1]] == 1) { Console.WriteLine("Ход действителен!");   }else
                                                         { Console.WriteLine("Ход недействителен!"); }

            chessBoard[place_2[0], place_2[1]] = 3;

            Console.WriteLine("Желаете отобразить доску? (1.Да 2.Нет)");
            if (int.TryParse(Console.ReadLine(), out k) && k == 1)
            {
                Console.WriteLine("   A B C D E F G H");
                Console.WriteLine(" ");
                for(int i = 7; i >= 0; i--)
                {
                    Console.Write(i+1 + "  ");
                    for (int ii = 0; ii < 8; ii++)
                    {
                        Console.Write(chessBoard[ii,i] + " ");
                    }
                    Console.WriteLine(" ");
                }
            }
        }
    }
}
