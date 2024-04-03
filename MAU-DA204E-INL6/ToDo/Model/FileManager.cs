using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ToDo.Model
{
    /// <summary>
    /// FileManager is a static class that handles saving and loading of TaskManagers
    /// </summary>
    public static class FileManager
    {
        // These options are used to be able to handle the enumerator in the Tasks.
        private static JsonSerializerOptions options = new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumConverter() },
            WriteIndented = true
        };
        /// <summary>
        /// Saves an observable collection of tasks to a file in JSON format
        /// </summary>
        /// <param name="taskList">ObservableCollection of Tasks</param>
        /// <returns>True if successfull</returns>
        public static bool SaveFile(ObservableCollection<Task> taskList)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "JSON files (*.json)|*.json";
                var result = saveFileDialog.ShowDialog();
                if(result == true)
                {
                    string filePath = saveFileDialog.FileName;
                    var jsonString = JsonSerializer.Serialize(taskList, options);
                    File.WriteAllText(filePath, jsonString);
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
                return false;
            }
        }
        /// <summary>
        /// Open a previously saved JSON of an ObservableCollection of Tasks
        /// </summary>
        /// <returns>ObservableCollection representing the Tasks saved in the selected file</returns>
        public static ObservableCollection<Task> OpenFile()
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "JSON files (*.json)|*.json";
                var result = openFileDialog.ShowDialog();
                if (result == true)
                {
                    var file = File.ReadAllText(openFileDialog.FileName);
                    var tasks = JsonSerializer.Deserialize<ObservableCollection<Task>>(file, options);
                    return tasks;
                }
                return new ObservableCollection<Task>();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
                return new ObservableCollection<Task>();
            }
        }
    }
}
