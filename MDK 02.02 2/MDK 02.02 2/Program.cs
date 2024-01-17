using System;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics.Arm;
public class Mainclass
{
    public static void Main(string[] args)
    {
        Tasks tasks = new Tasks();
        do
        {
            Console.Write("Выберите номер задачи: \n[1] Трамваи\n[2] Корреляция Спирмена\nВыбор: "); int ans = int.Parse(Console.ReadLine() ?? "1");
            Console.WriteLine(); switch (ans)
            {
                case 1:
                    tasks.FirstTask(); break;
                case 2:
                    tasks.SecondTask();
                    break;
                default:
                    Console.WriteLine("Некорректное число!"); break;
            }
            Console.WriteLine();
        } while (true);
    }
}
public class Tasks
{
    public void FirstTask()
    {
        double HumSpeed = 3; int TramOn = 60;
        int TramOff = 40; Console.WriteLine($"Человек шел со скоростью {HumSpeed} км/ч вдоль трамвайных путей.\nПо пути он насчитал {TramOn} встречных и {TramOff} обогнавших его трамваев.\nТрамваи ходят с равномерным интервалом.\nКакова средняя скорость трамвая?");
        Console.WriteLine($"Пусть скорость трамваев будет равна V.\nТогда скорость встречных трамваев будет равна V + {HumSpeed}, а скорость попутных трамваем равна V - {HumSpeed}"); Console.WriteLine($"Формула: {TramOn}/(V + {HumSpeed}) = {TramOff}/(V - {HumSpeed})\n{TramOn}(V - {HumSpeed}) = {TramOff}(V + {HumSpeed})\n{TramOn}V - {TramOff}V = {TramOff * HumSpeed} + {TramOn * HumSpeed}");
        Console.WriteLine($"Средняя скорость трамвая равна {(TramOff * HumSpeed + TramOn * HumSpeed) / (TramOn - TramOff)} км/ч");
    }
    public void SecondTask()
    {
        Console.Write("Введите количество пар элементов: "); int countPairs = int.Parse(Console.ReadLine() ?? "1");
        double[] x = new double[countPairs]; double[] y = new double[countPairs];
        for (int i = 0; i < countPairs; i++)
        {
            Console.Write($"Введите {i + 1}-ю пару чисел: ");
            string Pair = Console.ReadLine()?.Trim() ?? "0 0"; int[] arrPair = Array.ConvertAll(Pair.Split(' '), int.Parse);
            x[i] = arrPair[0]; y[i] = arrPair[1];
        }
        double spearmanCorrelation = CalculateSpearmanCorrelation(x, y); Console.WriteLine($"Корреляция Спирмена: {spearmanCorrelation}");
        static double CalculateSpearmanCorrelation(double[] x, double[] y)
        {
            int[] ranksX = RankData(x);
            int[] ranksY = RankData(y);
            double[] rankDifferences = new double[x.Length]; for (int i = 0; i < x.Length; i++)
            {
                rankDifferences[i] = ranksX[i] - ranksY[i];
            }
            double sumSquaredDifferences = rankDifferences.Sum(diff => Math.Pow(diff, 2)); int n = x.Length;
            double spearmanCorrelation = 1 - (6 * sumSquaredDifferences) / (n * (n * n - 1));
            return spearmanCorrelation;
        }
        static int[] RankData(double[] data)
        {
            int[] indexes = Enumerable.Range(0, data.Length).ToArray();
            Array.Sort(indexes, (x, y) => data[x].CompareTo(data[y]));
            int[] ranks = new int[data.Length]; for (int i = 0; i < data.Length; i++)
            {
                ranks[indexes[i]] = i + 1;
            }
            return ranks;
        }
    }
}