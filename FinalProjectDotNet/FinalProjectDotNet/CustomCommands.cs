using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace FinalProjectDotNet
{
    public static class CustomCommands
    {
        static RoutedUICommand addCrs = new RoutedUICommand("Open Add Course Form", "AddCourse", typeof(CustomCommands));

        public static RoutedUICommand AddCourse { get { return addCrs; } }

        static RoutedUICommand addStu = new RoutedUICommand("Open Add Student Form", "AddStudent", typeof(CustomCommands));

        public static RoutedUICommand AddStudent { get { return addStu; } }

    }
}
