using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    public partial class StartWindow : Form
    {
        //Create our variables needed in the form
        private Button button2;
        private Label label, label2;
        private TextBox textBox;
        private String name;

        //Needed to test the connection to the database
        private static SqlConnection con;

        //Call to build the form
        public StartWindow()
        {
            init();
        }

        //Function to test the connection to the database
        //   Returns:  True if it can connect
        //              False if it failed to connect
        private bool testConnecion()
        {
            //Try to open a connection to the database
            try
            {
                con = new System.Data.SqlClient.SqlConnection();
                con.ConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\" + name + ".mdf;Integrated Security=True";
                con.Open();
            }
            //If it fails then make sure the exception is close and return false
            catch (Exception)
            {
                con.Close();
                return false;
            }
            //If it's succesful we want to close the connnection
            finally
            {
                con.Close();
            }
            //Return true, meaning it was able to open the connection
            return true;
        }

        //Once the button is clicked, meaning the user wants to connect/log-in 
        //   then we test the connection, if it works then open an instance
        //   of the main window, otherwise show an error message
        private void button2Clicked(Object sender, EventArgs e)
        {
            name = textBox.Text;

            //Used so the user can leave the name blank and it'll connect to 
            //   the local database in this project
            if (String.IsNullOrEmpty(name)) { name = "Database1"; }

            //If the testConnection is true, meaning it succesfully connected,
            //   then open our main window
            if (testConnecion() == true)
            {
                Boot.setDBName(name);
                this.Close();
            }
            //If testConnection returns false it means it could not open a 
            //   connection for the user, so it shows an error message
            else
            {
                MessageBox.Show("Could not connect to the database");
            }
        }

        //Used to create the windows form
        private void init()
        {

            this.button2 = new System.Windows.Forms.Button();
            this.button2.Location = new System.Drawing.Point(40, 10);
            this.button2.MinimumSize = new Size(110, 30);
            this.button2.Size = new Size(button2.Size.Width, 30);
            this.button2.Text = "Log In/Connect";
            this.button2.Click += new EventHandler(this.button2Clicked);
            this.button2.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.button2.Font = new Font(button2.Font, FontStyle.Bold);

            this.label = new System.Windows.Forms.Label();
            this.label.Text = "Name: ";
            this.label.Location = new System.Drawing.Point(10, 70);
            this.label.Font = new Font(label.Font, FontStyle.Bold);

            this.textBox = new System.Windows.Forms.TextBox();
            this.textBox.MinimumSize = new Size(180, 30);
            this.textBox.Size = new Size(textBox.Size.Width, 30);
            this.textBox.Location = new System.Drawing.Point(10, 100);

            this.label2 = new Label();
            this.label2.Text = "*Leave blank to use database\n within this project";
            this.label2.Font = new Font(label2.Font.FontFamily, 8);
            this.label2.Location = new System.Drawing.Point(10, 135);
            this.label2.AutoSize = true;

            this.ClientSize = new System.Drawing.Size(200, 170);
            this.BackColor = System.Drawing.Color.LightGray;

            this.Controls.Add(this.button2);
            this.Controls.Add(this.label);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.label2);
            this.Name = "StartWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
