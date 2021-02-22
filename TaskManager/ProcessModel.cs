using System.Collections.Generic;
using System.Text;

namespace TaskManager
{
    public class ProcessModel
    {
        public int Threads { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public int Id { get; set; }
        public long Memory { get; set; }
        public string Priority { get; set; }
        public IEnumerable<ThreadModel> ThreadsInfo { get; set; }

        public override string ToString()
        {
            StringBuilder threadsInfo = new StringBuilder();
            foreach (var thread in ThreadsInfo)
            {
                threadsInfo.AppendLine(thread.ToString());
            }
            return "Process name: " + Name + "\t" + "Username: " + UserName + "\t" + "Process Id: " + Id + "\t" +
                   "Memory: " + Memory + "\t" + "Priority: " + Priority + threadsInfo;
        }
    }
    public struct ThreadModel
    {
        public int Id { get; set; }
        public string Priority { get; set; }
        public override string ToString()
        {
            return "Thread Id: " + "\t" + Id + "Priority: " + Priority;
        }
    }
}
