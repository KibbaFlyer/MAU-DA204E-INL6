using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Model
{
    public static class FileManager
    {
        public static bool SaveFile(string path, List<Task> taskList)
        {
            try
            {
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
