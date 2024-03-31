using System;
using System.Threading;
namespace Potok
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Крок: ");
            int krok = int.Parse(Console.ReadLine()); //крок 
            Console.WriteLine("Кiлькiсть потокiв: ");
            byte potok = byte.Parse(Console.ReadLine()); //кількість потоків
            double[] sum = new double[potok]; //сума чисел кожного потоку
            double[] elem = new double[potok]; //кількість доданків для обч-ня суми
            bool neStop = false;
            Thread[] potoki_no_ne_vetra = new Thread[potok]; 
            for(int i = 0; i < potok; i++) //ств-ня та запуск потоків
            {
                int mesto = i;
                potoki_no_ne_vetra[i] = new Thread(() => Schitalka(mesto, krok, sum, elem, ref neStop)); //ств-ня потоку + виклик методу
                potoki_no_ne_vetra[i].Start(); //запуск потоку
            }    
            Thread stopPotok = new Thread(() => StopPotok(ref neStop)); 
            stopPotok.Start();
            stopPotok.Join();
            for(int i = 0; i < potok; i++) //вив-ня інф-ї про кожен потік
            {
                Console.WriteLine($"Потiк {i+1}: сума = {sum[i]}  кiлькiсть доданкiв = {elem[i]}");
            }
        }
        static void Schitalka(int mesto, int krok, double[] sum, double[] elem, ref bool neStop)//метод рахує суму еле-ів кожного потоку
        {
            double summ = 0;
            double elemm = 0;
            for( int i = 0; !neStop; i+= krok)
            {
                summ += i;
                elemm++;
            }
            sum[mesto] = summ;
            elem[mesto] = elemm;
        }
        static void StopPotok(ref bool neStop) //метод зупиняє через вказаний час роботу поточного потоку
        {
            Thread.Sleep(10000);
            neStop = true;
        }
    }
}