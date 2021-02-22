using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Diagnostics;
using System.Management;

namespace TaskManager
{
    public class ProcessRepository
    {
        public IEnumerable<ProcessModel> GetAllProcesses()
        {
            var localProcesses = Process.GetProcesses();
                return localProcesses.Select(GetNewProcessModel);
        }
        public ProcessModel GetNewProcessModel(Process process)
        {
            try
            {
                var model = new ProcessModel
                {
                    Id = process.Id,
                    Name = process.ProcessName,
                    UserName = GetProcessOwner(process.Id),
                    Priority = process.PriorityClass.ToString(),
                    Threads = process.Threads.Count,
                    ThreadsInfo = GetThreadsInfo(process.Threads),
                    Memory = process.WorkingSet64,
                };
                return model;
            }
            catch (Win32Exception)
            {
                return null;
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }
        public string GetProcessOwner(int processId)
        {
            string query = "Select * From Win32_Process Where ProcessID = " + processId;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            ManagementObjectCollection processList = searcher.Get();

            foreach (ManagementObject obj in processList)
            {
                string[] argList = new string[] { string.Empty, string.Empty };
                int returnVal = Convert.ToInt32(obj.InvokeMethod("GetOwner", argList));
                if (returnVal == 0)
                {
                    // return DOMAIN\user
                    return argList[1] + "\\" + argList[0];
                }
            }

            return "NO OWNER";
        }

        public IEnumerable<ThreadModel> GetThreadsInfo(ProcessThreadCollection threads)
        {
            ICollection<ThreadModel> threadsInfo = new List<ThreadModel>();
            for(int i = 0; i< threads.Count; i++)
            {
                ThreadModel model = new ThreadModel();
                model.Id = threads[i].Id;
                model.Priority = threads[i].PriorityLevel.ToString();
                threadsInfo.Add(model);

            }

            return threadsInfo;
        }
        
    }
}
