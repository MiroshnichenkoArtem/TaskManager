using System;

namespace TaskManager
{
    class Program
    {
        static void Main()
        {
            ProcessRepository repository = new ProcessRepository();
            foreach (var process in repository.GetAllProcesses())
            {
                if(process !=null)
                    Console.WriteLine(process.ToString());
            }

            Console.ReadKey();

        }
    }
}
