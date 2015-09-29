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
using System.IO;

/*
 *  Shea Meyers
 */

namespace CompanyProjectManagementSystem
{
    public partial class MainWindow : Form
    {
        //Variables needed for forms
        private static String dbName;
        private DataGridView viewTable;
        private static DataTable table;

        private Button create;
        private Button search;
        private Button export;
        private Button update;
        private Button delete;

        //Variables needed for 
        private static SqlConnection con;
        private static SqlCommand cmd;
        private static SqlDataReader reader;

        //Helper function so other classes can get the
        //   name of the database
        public static String getDBName()
        {
            return dbName;
        }

        //Updates the table (adds the row) after there is an entry added to the database
        public static void updateCreate(String a, String b, String c, String d, String e, String f, String g, String h, String i, String j)
        {
            table.Rows.Add(a, b, c, d, e, f, g, h, i, j);
        }

        //Updates the tables (updates the row) after there is a row that is updated
        public static void updateUpdate(String a, String b, String c, String d, String e, String f, String g, String h, String i, String j)
        {
            DataRow[] row = table.Select("ABCID='" + a + "'");

            try
            {
                if (!String.IsNullOrEmpty(b)) { row[0]["Title"] = b; }
                if (!String.IsNullOrEmpty(c)) { row[0]["Description"] = c; }
                if (!String.IsNullOrEmpty(d)) { row[0]["Vendor"] = d; }
                if (!String.IsNullOrEmpty(e)) { row[0]["ListPrice"] = e; }
                if (!String.IsNullOrEmpty(f)) { row[0]["Cost"] = f; }
                if (!String.IsNullOrEmpty(g)) { row[0]["Status"] = g; }
                if (!String.IsNullOrEmpty(h)) { row[0]["Location"] = h; }
                if (!String.IsNullOrEmpty(i)) { row[0]["Date Created"] = i; }
                if (!String.IsNullOrEmpty(j)) { row[0]["Date Recieved"] = j; }
            }
            catch (Exception)
            {
                MessageBox.Show("Could not find entry with that ID");
            }
        }

        //Delets the row from the table that was deleted in the database
        public static void updateDelete(String a)
        {
            DataRow[] row = table.Select("ABCID='" + a + "'");

            row[0].Delete();
        }

        //Sets the database name variable and creates a new form
        public MainWindow(String name)
        {
            dbName = name;
            init(dbName);
        }

        //Helper function to build the form
        //   Queries the database to get the information from it, 
        //   Then formates the information into a Datatable and
        //   return the datatable
        private static DataTable GetTable(String name)
        {

            DataTable tempTable = new DataTable();

            try
            {
                //Connect to the database and create a new query
                con = new System.Data.SqlClient.SqlConnection();
                con.ConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\" + name + ".mdf;Integrated Security=True";

                cmd = new SqlCommand("SELECT * FROM MainTable;", con);

                con.Open();

                //Add the column headers to our datatable
                tempTable.Columns.Add("ABCID", typeof(string));
                tempTable.Columns.Add("Title", typeof(string));
                tempTable.Columns.Add("Description", typeof(string));
                tempTable.Columns.Add("Vendor", typeof(string));
                tempTable.Columns.Add("List Price", typeof(string));
                tempTable.Columns.Add("Cost", typeof(string));
                tempTable.Columns.Add("Status", typeof(string));
                tempTable.Columns.Add("Location", typeof(string));
                tempTable.Columns.Add("Date Created", typeof(string));
                tempTable.Columns.Add("Date Recieved", typeof(string));

                //Get the information from the query
                reader = cmd.ExecuteReader();

                //Go through the information and add it to the datatable
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tempTable.Rows.Add(reader.GetValue(0),
                                        reader.GetValue(1),
                                        reader.GetValue(2),
                                        reader.GetValue(3),
                                        reader.GetValue(4),
                                        reader.GetValue(5),
                                        reader.GetValue(6),
                                        reader.GetValue(7),
                                        reader.GetValue(8),
                                        reader.GetValue(9)
                                        );
                    }

                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Failed to connect to database: " + e);
            }
            //Close the reader and the connection
            finally
            {
                reader.Close();
                con.Close();
            }
            //Return the datatable
            return tempTable;
        }

        //If create is clicked then open a new form of it
        private void createClicked(Object sender, EventArgs e)
        {
            CreateWindow createWindow = new CreateWindow();
            createWindow.Show();
        }

        //If search is clicked then open a new form of it
        private void searchClicked(Object sender, EventArgs e)
        {
            SearchWindow searchWindow = new SearchWindow();
            searchWindow.Show();
        }

        //If update is clicked open a new window of it
        private void updateClicked(Object sender, EventArgs e)
        {
            UpdateWindow updateWindow = new UpdateWindow();
            updateWindow.Show();
        }

        //If delete is clicked open a new window of it
        private void deleteClicked(Object sender, EventArgs e)
        {
            DeleteWindow deleteWindow = new DeleteWindow();
            deleteWindow.Show();
        }

        //Build the windows form
        private void init(String name)
        {
            table = GetTable(name);

            this.viewTable = new System.Windows.Forms.DataGridView();
            this.viewTable.DataSource = table;
            this.viewTable.Size = new Size(850, 750);

            this.create = new System.Windows.Forms.Button();
            this.create.Location = new System.Drawing.Point(130, 760);
            this.create.Text = "Create";
            this.create.Click += new EventHandler(this.createClicked);
            this.create.Font = new System.Drawing.Font(create.Font, FontStyle.Bold);
            this.create.BackColor = System.Drawing.Color.DeepSkyBlue;

            this.search = new System.Windows.Forms.Button();
            this.search.Location = new System.Drawing.Point(306, 760);
            this.search.Text = "Search";
            this.search.Click += new EventHandler(this.searchClicked);
            this.search.Font = new System.Drawing.Font(search.Font, FontStyle.Bold);
            this.search.BackColor = System.Drawing.Color.DeepSkyBlue;

            this.update = new System.Windows.Forms.Button();
            this.update.Location = new System.Drawing.Point(472, 760);
            this.update.Text = "Update";
            this.update.Click += new EventHandler(this.updateClicked);
            this.update.Font = new System.Drawing.Font(update.Font, FontStyle.Bold);
            this.update.BackColor = System.Drawing.Color.DeepSkyBlue;

            this.delete = new System.Windows.Forms.Button();
            this.delete.Location = new System.Drawing.Point(648, 760);
            this.delete.Text = "Delete";
            this.delete.Click += new EventHandler(this.deleteClicked);
            this.delete.Font = new System.Drawing.Font(delete.Font, FontStyle.Bold);
            this.delete.BackColor = System.Drawing.Color.DeepSkyBlue;

            this.ClientSize = new System.Drawing.Size(850, 800);
            this.BackColor = System.Drawing.Color.LightGray;

            this.Controls.Add(this.viewTable);
            this.Controls.Add(this.create);
            this.Controls.Add(this.search);
            this.Controls.Add(this.export);
            this.Controls.Add(this.update);
            this.Controls.Add(this.delete);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

    }
}
