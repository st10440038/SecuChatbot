using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace SecuBot.GUI
{
    public class TaskItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ReminderDate { get; set; }
        public bool IsCompleted { get; set; }

        public string ReminderText => ReminderDate.HasValue
            ? $"Reminder set for {ReminderDate.Value.ToShortDateString()}"
            : "No reminder set";

        public string Display => $"{(IsCompleted ? "✅" : "📝")} {Title} - {Description} ({ReminderText})";
    }
}
