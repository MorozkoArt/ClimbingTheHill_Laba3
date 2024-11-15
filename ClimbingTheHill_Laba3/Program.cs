using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_method_of_climbing_the_hill
{
    internal class Program
    {
        public static int L = 5;

        // Перевод из десятичной в двоичную
        public static string DecimalToBinary(int dec)
        {
            string binary = "";
            while (dec > 0)
            {
                binary += (dec % 2).ToString();
                dec /= 2;
            }
            while (binary.Length < 5)
            {
                binary += "0";
            }
            char[] stringArray = binary.ToCharArray();
            Array.Reverse(stringArray);
            string reversedBinary = new string(stringArray);
            return reversedBinary;

        }
        // Подсчет приспособленности через квадратичную функцию
        public static int QuadraticAdaptationFunction(int dec)
        {
            return Convert.ToInt32(Math.Pow((dec - Convert.ToInt32(Math.Pow(2, L - 1))), 2));
        }
        // Подсчет хэммингово расстояния
        public static int TheHammingDistance(string s1, string s2)
        {
            int distance = 0;
            if (s1.Length == s2.Length)
            {
                for (int i = 0; i < s1.Length; ++i)
                {
                    if (s1[i] != s2[i])
                        distance++;
                }
                return distance;
            }
            return -1;
        }
        // Метод для поиска индекса элемента в списке
        public static int SearchIndice(string s, List<List<string>> strings)
        {
            for (int i = 0; i < strings.Count; ++i)
            {
                if (strings[i][0] == s)
                {
                    return i;
                }
            }
            return -1;

        }
        // Метод для поиска индекса элемента в списке (2)
        public static int SearchIndice2(string s, List<List<string>> strings)
        {
            for (int i = 0; i < strings.Count; ++i)
            {
                if (strings[i][1] == s)
                {
                    return i;
                }
            }
            return -1;

        }
        static void Main(string[] args)
        {
            Random rnd = new Random();
            List<List<string>> binary_encodings = new List<List<string>>();
            int n = 0;
            while (true)
            {
                int path = -1;
                Console.WriteLine("Меню");
                Console.WriteLine("1 - Сгенерировать список кодировок");
                Console.WriteLine("2 - Запустить алгоритм");
                Console.WriteLine("3 - Завершить работу программы");
                path = int.Parse(Console.ReadLine());
                switch (path)
                {
                    // Генерация списка кодировок и приспособленностей
                    case 1:
                        Console.WriteLine("Выберите способ задания приспособленностей");
                        Console.WriteLine("1 - приспособленность вычисляется как квадратичная функция");
                        Console.WriteLine("2 - случайное задание приспособленностей");
                        Console.WriteLine("3 - приспособленность соответствует натуральному значению бинарного кода");
                        int AlgorithmPath = -1;
                        AlgorithmPath = int.Parse(Console.ReadLine());
                        binary_encodings.Clear();
                        for (int i = 0; i < Math.Pow(2, L); ++i)
                        {
                            List<string> list = new List<string>();
                            list.Add(DecimalToBinary(i));
                            if (AlgorithmPath == 1)
                            {
                                list.Add(QuadraticAdaptationFunction(i).ToString());
                            }
                            else if (AlgorithmPath == 2)
                            {
                                string randomAd = "";
                                bool flag = true;
                                while (flag == true)
                                {
                                    randomAd = rnd.Next(0, Convert.ToInt32(Math.Pow(2, L))).ToString();
                                    if (SearchIndice2(randomAd, binary_encodings) == -1)
                                    {
                                        list.Add(randomAd);
                                        flag = false;
                                    }
                                }
                            }
                            else if (AlgorithmPath == 3)
                            {
                                list.Add(i.ToString());

                            }
                            Console.WriteLine("Кодировка: " + list[0] + " Приспособленность: " + list[1]);
                            binary_encodings.Add(list);
                        }
                        if (AlgorithmPath == 1 || AlgorithmPath == 2 || AlgorithmPath == 3)
                            Console.WriteLine("Список успешно сгенерирован!");
                        else
                            Console.WriteLine("Ошибка ввода");
                        break;
                    // Алгоритм
                    case 2:
                        if (binary_encodings.Count > 0)
                        {
                            string MaxS = DecimalToBinary(rnd.Next(0, binary_encodings.Count));
                            string max_Si = MaxS;
                            string max = binary_encodings[SearchIndice(MaxS, binary_encodings)][1];
                            List<string> locality = new List<string>();
                            // Начальная окрестность
                            for (int i = 0; i < Math.Pow(2, L); ++i)
                            {
                                if (TheHammingDistance(MaxS, binary_encodings[i][0]) == 1)
                                    locality.Add(binary_encodings[i][0]);
                            }
                            Console.WriteLine("Введите количество шагов от 1 до 31");
                            n = int.Parse(Console.ReadLine());
                            // Основная часть алгаритма
                            for (int i = 0; i < n; ++i)
                            {
                                max_Si = MaxS;
                                // Вывод промежуточных данных алгоритма
                                Console.WriteLine("Номер шага: " + (i + 1) + " Максимальная приспособленность: " + max + " Максимальная кодировка: " + MaxS);
                                Console.WriteLine("|Окрестность|" + "Приспособленность|");
                                Console.WriteLine("-".PadRight(31, '-'));
                                for (int k = 0; k < locality.Count; k++)
                                {
                                    string s1 = locality[k].PadRight(9);
                                    string s2 = binary_encodings[SearchIndice(locality[k], binary_encodings)][1];
                                    s2 = s2.PadRight(11);
                                    Console.WriteLine("|".PadRight(3) + s1 + "|".PadRight(7) + s2 + "|");
                                    Console.WriteLine("-".PadRight(31, '-'));
                                }
                                // Выбор максимального элемента из окрестности 
                                string Si = "";
                                int MaxLocation = -1;
                                for (int f = 0; f < locality.Count; f++)
                                {
                                    if (MaxLocation < int.Parse(binary_encodings[SearchIndice(locality[f], binary_encodings)][1]))
                                    {
                                        Si = locality[f];
                                        MaxLocation = int.Parse(binary_encodings[SearchIndice(locality[f], binary_encodings)][1]);
                                    }
                                }
                                Console.WriteLine(" Выбираемая кодировка (Si): " + Si + " Её приспособленность: " + binary_encodings[SearchIndice(Si, binary_encodings)][1]);
                                locality.Remove(Si);
                                // Смена максимума, в случае удоволетворения условию
                                if (int.Parse(max) < int.Parse(binary_encodings[SearchIndice(Si, binary_encodings)][1]))
                                {
                                    MaxS = Si;
                                    max = binary_encodings[SearchIndice(Si, binary_encodings)][1];
                                    Console.WriteLine("Произошла смена максимума. Максимальная приспособленность: " + max
                                        + " Максимальная кодировка: " + MaxS);
                                    locality.Clear();
                                    // Новая окрестность для нового элемента
                                    for (int j = 0; j < Math.Pow(2, L); ++j)
                                    {
                                        if (TheHammingDistance(MaxS, binary_encodings[j][0]) == 1 && binary_encodings[j][0] != max_Si)
                                            locality.Add(binary_encodings[j][0]);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("/".PadRight(140, '/'));
                                    break;
                                }
                                Console.WriteLine("/".PadRight(140, '/'));
                            }
                            Console.WriteLine("Результат работы алгоритма: максимальная приспособленность: " + max + " Максимальная кодировка: " + MaxS);
                        }
                        else
                        {
                            Console.WriteLine("Список пустой!!!");
                        }
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Неккоректный ввод");
                        break;
                }
            }
        }
    }
}

