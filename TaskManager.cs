using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecuBot.GUI;
using System.Windows.Controls;


namespace SecuBot.GUI
{
    public static class TaskManager
    {
        public static List<TaskItem> Tasks { get; } = new List<TaskItem>();

        public static void Add(TaskItem task) => Tasks.Add(task);

        public static void Delete(int index)
        {
            if (index >= 0 && index < Tasks.Count)
                Tasks.RemoveAt(index);
        }

        public static void Complete(int index)
        {
            if (index >= 0 && index < Tasks.Count)
                Tasks[index].IsCompleted = true;

        }
    }

}
