using System;
using System.Collections.Generic;

namespace WorkerCompanyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Worker> workers = new List<Worker>();
            while (true)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine("1. Додати працівника");
                Console.WriteLine("2. Показати інформацію про працівників");
                Console.WriteLine("3. Показати інформацію про конкретного працівника");
                Console.WriteLine("4. Вийти");
                Console.Write("Оберіть опцію: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddWorker(workers);
                        break;
                    case "2":
                        ShowWorkers(workers);
                        break;
                    case "3":
                        ShowSpecificWorker(workers);
                        break;
                    case "4":
                        Console.WriteLine("Вихід із програми.");
                        return;
                    default:
                        Console.WriteLine("Невірна опція. Спробуйте ще раз.");
                        break;
                }
            }
        }

        static void AddWorker(List<Worker> workers)
        {
            Console.Write("Введіть повне ім'я працівника: ");
            string fullName = Console.ReadLine();

            Console.Write("Введіть місто проживання працівника: ");
            string homeCity = Console.ReadLine();

            Console.Write("Введіть дату початку роботи (рррр-мм-дд): ");
            DateTime startDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Введіть назву компанії: ");
            string companyName = Console.ReadLine();

            Console.Write("Введіть місто головного офісу компанії: ");
            string mainOfficeCity = Console.ReadLine();

            Console.Write("Введіть посаду працівника: ");
            string position = Console.ReadLine();

            Console.Write("Введіть зарплату працівника: ");
            decimal salary = decimal.Parse(Console.ReadLine());

            Console.Write("Чи працює працівник на повний робочий день? (так/ні): ");
            bool isFullTime = Console.ReadLine().Trim().ToLower() == "так";

            Company company = new Company(companyName, mainOfficeCity, position, salary, isFullTime);
            Worker worker = new Worker(fullName, homeCity, startDate, company);
            workers.Add(worker);

            Console.WriteLine("Працівника додано успішно!");
        }

        static void ShowWorkers(List<Worker> workers)
        {
            if (workers.Count == 0)
            {
                Console.WriteLine("Список працівників порожній.");
                return;
            }

            foreach (var worker in workers)
            {
                Console.WriteLine(worker);
            }
        }

        static void ShowSpecificWorker(List<Worker> workers)
        {
            if (workers.Count == 0)
            {
                Console.WriteLine("Список працівників порожній.");
                return;
            }

            Console.Write("Введіть індекс працівника (0-based): ");
            int index = int.Parse(Console.ReadLine());

            if (index >= 0 && index < workers.Count)
            {
                Console.WriteLine(workers[index]);
            }
            else
            {
                Console.WriteLine("Невірний індекс!");
            }
        }
    }

    public class Worker
    {
        public string FullName { get; set; }
        public string HomeCity { get; set; }
        public DateTime StartDate { get; set; }
        public Company WorkPlace { get; set; }

        public Worker() { }

        public Worker(string fullName, string homeCity, DateTime startDate, Company workPlace)
        {
            FullName = fullName;
            HomeCity = homeCity;
            StartDate = startDate;
            WorkPlace = workPlace;
        }

        public Worker(Worker other)
        {
            FullName = other.FullName;
            HomeCity = other.HomeCity;
            StartDate = other.StartDate;
            WorkPlace = new Company(other.WorkPlace);
        }

        public int GetWorkExperience()
        {
            return (DateTime.Now.Year - StartDate.Year) * 12 + DateTime.Now.Month - StartDate.Month;
        }

        public bool LivesNotFarFromTheMainOffice()
        {
            return string.Equals(HomeCity, WorkPlace.MainOfficeCity, StringComparison.OrdinalIgnoreCase);
        }

        public override string ToString()
        {
            return $"\nІм'я: {FullName}\n" +
                   $"Місто проживання: {HomeCity}\n" +
                   $"Дата початку роботи: {StartDate.ToShortDateString()}\n" +
                   $"Компанія: {WorkPlace.Name}\n" +
                   $"Головний офіс: {WorkPlace.MainOfficeCity}\n" +
                   $"Посада: {WorkPlace.Position}\n" +
                   $"Зарплата: {WorkPlace.Salary}\n" +
                   $"Повний робочий день: {(WorkPlace.IsFullTimeEmployee ? "Так" : "Ні")}\n" +
                   $"Стаж роботи (місяців): {GetWorkExperience()}\n" +
                   $"Проживає поруч із головним офісом: {(LivesNotFarFromTheMainOffice() ? "Так" : "Ні")}";
        }
    }

    public class Company
    {
        public string Name { get; set; }
        public string MainOfficeCity { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }
        public bool IsFullTimeEmployee { get; set; }

        public Company() { }

        public Company(string name, string mainOfficeCity, string position, decimal salary, bool isFullTimeEmployee)
        {
            Name = name;
            MainOfficeCity = mainOfficeCity;
            Position = position;
            Salary = salary;
            IsFullTimeEmployee = isFullTimeEmployee;
        }

        public Company(Company other)
        {
            Name = other.Name;
            MainOfficeCity = other.MainOfficeCity;
            Position = other.Position;
            Salary = other.Salary;
            IsFullTimeEmployee = other.IsFullTimeEmployee;
        }

        public override string ToString()
        {
            return $"Назва: {Name}, Головний офіс: {MainOfficeCity}, Посада: {Position}, Зарплата: {Salary}, Повний день: {(IsFullTimeEmployee ? "Так" : "Ні")}";
        }
    }
}
