using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Data.SqlClient;

/* 
 *  Shea Meyers 
 */

namespace CompanyProjectManagementSystem
{
    class Boot
    {
        private static string dbName;

        public static void setDBName(string name)
        {
            dbName = name;
        }

        private static string getDBName()
        {
            return dbName;
        }

        //Creates a new instance or our window and runs it
        public static void Start()
        {
            StartWindow start = new StartWindow();
            Application.Run(start);
        }

        public static void MainWindow()
        {
            MainWindow mainWindow = new MainWindow(getDBName());
            Application.Run(mainWindow);
        }

        //Main function of the program, used to start the application
        static int Main()
        {
            Start();
            MainWindow();
            return 0;
        }
    }
}
