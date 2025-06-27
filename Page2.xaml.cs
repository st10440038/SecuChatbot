using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static SecuBot.GUI.Page1;


namespace SecuBot.GUI
{
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
            LoadTasks();
        }

        private void LoadTasks()
        {
            var tasks = TaskManager.Tasks;

            if (tasks.Count == 0)
            {
                EmptyMessage.Visibility = Visibility.Visible;
                TaskList.ItemsSource = null;
            }
            else
            {
                EmptyMessage.Visibility = Visibility.Collapsed;
                TaskList.ItemsSource = tasks;
            }
        }


        private void RefreshTaskList()
        {
            TaskList.ItemsSource = null;
            TaskList.ItemsSource = TaskManager.Tasks;
        }


        private void CompleteTask_Click(object sender, RoutedEventArgs e)
        {
            var task = (sender as Button)?.Tag as TaskItem;
            if (task == null) return;

            var result = MessageBox.Show($"Mark the task \"{task.Title}\" as completed?", "Confirm Complete", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                TaskManager.Complete(TaskManager.Tasks.IndexOf(task));
                RefreshTaskList();
            }
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            var task = (sender as Button)?.Tag as TaskItem;
            if (task == null) return;

            var result = MessageBox.Show($"Are you sure you want to delete \"{task.Title}\"?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                TaskManager.Delete(TaskManager.Tasks.IndexOf(task));
                RefreshTaskList();
            }
        }

        private void GoBackToChat_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(MainWindow.ChatPageInstance);
        }
    }
}