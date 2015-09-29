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
    public partial class DeleteWindow : Form
    {
        //Variables for the form
        private static DataTable table;
        private DataGridView viewTable;

        private Label abcID;
        private Label title;
        private Label description;
        private Label vendor;
        private Label listPrice;
        private Label cost;
        private Label status;
        private Label location;
        private Label dateCreated;
        private Label dateRecieved;

        private TextBox abcIDBox;
        private TextBox titleBox;
        private TextBox descriptionBox;
        private TextBox vendorBox;
        private TextBox listPriceBox;
        private TextBox costBox;
        private TextBox statusBox;
        private TextBox locationBox;
        private TextBox dateCreatedBox;
        private TextBox dateRecievedBox;

        private Button button;
        private Button cancelButton;
        private Button deleteButton;

        //String variables for the query
        private String abcIDString;
        private String titleString;
        private String descriptionString;
        private String vendorString;
        private String listPriceString;
        private String costString;
        private String statusString;
        private String locationString;
        private String dateCreatedString;
        private String dateRecievedString;

        //Variables to get the information
        //   from the database
        private static SqlConnection con;
        private static SqlCommand cmd;
        private static SqlDataReader reader;

        //Create the windows form
        public DeleteWindow()
        {
            init();
        }

        //Used to get a modified datatable of the rows that will be deleted
        private DataTable GetTable(String name)
        {

            DataTable tempTable = new DataTable();

            try
            {
                //Create a database connection
                con = new System.Data.SqlClient.SqlConnection();
                con.ConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\" + name + ".mdf;Integrated Security=True";

                //Create a query string
                String updateString = "SELECT * FROM MainTable \n WHERE ";

                //Update the string for the things you want to search
                if (!String.IsNullOrEmpty(abcIDString)) { updateString += "abcId='" + abcIDString + "' AND "; }
                if (!String.IsNullOrEmpty(titleString)) { updateString += "title='" + titleString + "' AND "; }
                if (!String.IsNullOrEmpty(descriptionString)) { updateString += "description='" + descriptionString + "' AND "; }
                if (!String.IsNullOrEmpty(vendorString)) { updateString += "vendor='" + vendorString + "' AND "; }
                if (!String.IsNullOrEmpty(listPriceString)) { updateString += "listPrice='" + listPriceString + "' AND "; }
                if (!String.IsNullOrEmpty(costString)) { updateString += "Cost='" + costString + "' AND "; }
                if (!String.IsNullOrEmpty(statusString)) { updateString += "Status='" + statusString + "' AND "; }
                if (!String.IsNullOrEmpty(locationString)) { updateString += "Location='" + locationString + "' AND "; }
                if (!String.IsNullOrEmpty(dateCreatedString)) { updateString += "dateCreated='" + dateCreatedString + "' AND "; }
                if (!String.IsNullOrEmpty(dateRecievedString)) { updateString += "dateRecieved='" + dateRecievedString + "' AND "; }

                //Removes the last space and the AND, needed to properly format
                updateString = updateString.Remove(updateString.Length - 4);

                updateString += ";";

                //Create the new SQL command
                cmd = new SqlCommand(updateString, con);

                //Open the database connection
                con.Open();

                //Create the columns of the table
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

                //Add the information from the query
                //   to the table
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
                //Close the reader
                reader.Close();
            }
            //This usually only happens if the user doesn't enter any information
            //   which in that case the entire table is loaded
            catch (Exception)
            {
            }
            //Close the connection
            finally
            {
                con.Close();
            }
            //Return the table
            return tempTable;
        }

        //If okay is clicked, meaning the user want to search and delete,
        //   this "re-makes" the form so it shows the information that will
        //   be deleted
        private void okayClicked(Object sender, EventArgs e)
        {
            abcIDString = abcIDBox.Text;
            titleString = titleBox.Text;
            descriptionString = descriptionBox.Text;
            vendorString = vendorBox.Text;
            listPriceString = listPriceBox.Text;
            costString = costBox.Text;
            statusString = statusBox.Text;
            locationString = locationBox.Text;
            dateCreatedString = dateCreatedBox.Text;
            dateRecievedString = dateRecievedBox.Text;

            this.ClientSize = new System.Drawing.Size(800, 800);

            this.Controls.Remove(abcID);
            this.Controls.Remove(title);
            this.Controls.Remove(description);
            this.Controls.Remove(vendor);
            this.Controls.Remove(listPrice);
            this.Controls.Remove(cost);
            this.Controls.Remove(status);
            this.Controls.Remove(location);
            this.Controls.Remove(dateCreated);
            this.Controls.Remove(dateRecieved);

            this.Controls.Remove(abcIDBox);
            this.Controls.Remove(titleBox);
            this.Controls.Remove(descriptionBox);
            this.Controls.Remove(vendorBox);
            this.Controls.Remove(listPriceBox);
            this.Controls.Remove(costBox);
            this.Controls.Remove(statusBox);
            this.Controls.Remove(locationBox);
            this.Controls.Remove(dateCreatedBox);
            this.Controls.Remove(dateRecievedBox);

            this.Controls.Remove(button);
            this.Controls.Remove(cancelButton);

            table = GetTable(MainWindow.getDBName());

            this.viewTable = new System.Windows.Forms.DataGridView();
            this.viewTable.DataSource = table;
            this.viewTable.Size = new Size(850, 740);

            this.deleteButton = new Button();
            this.deleteButton.Text = "Delete";
            this.deleteButton.Location = new System.Drawing.Point(10, 760);
            this.deleteButton.Click += new EventHandler(this.deleteClicked);
            this.deleteButton.Font = new Font(deleteButton.Font, FontStyle.Bold);
            this.deleteButton.BackColor = System.Drawing.Color.DeepSkyBlue;

            this.cancelButton = new Button();
            this.cancelButton.Text = "Cancel";
            this.cancelButton.Location = new System.Drawing.Point(150, 760);
            this.cancelButton.Click += new EventHandler(this.cancelClicked);
            this.cancelButton.Font = new Font(cancelButton.Font, FontStyle.Bold);
            this.cancelButton.BackColor = System.Drawing.Color.DeepSkyBlue;

            this.ClientSize = new System.Drawing.Size(850, 800);
            this.BackColor = System.Drawing.Color.LightGray;

            this.Controls.Add(viewTable);
            this.Controls.Add(deleteButton);
            this.Controls.Add(cancelButton);

            this.Name = "SearchWindow";
            this.Location = new Point(500, 100);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        //If this function is called it means the user sees the information that will be deleted and 
        //   has stated that he/she wants it to be deleted
        private void deleteClicked(Object sender, EventArgs e)
        {
            try
            {
                //Connect to the database
                con = new System.Data.SqlClient.SqlConnection();
                con.ConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\" + MainWindow.getDBName() + ".mdf;Integrated Security=True";

                //Create deletion string
                String updateString = "DELETE FROM MainTable \n WHERE ";

                //Add statements to the deletion string
                if (!String.IsNullOrEmpty(abcIDString)) { updateString += "abcId='" + abcIDString + "' AND "; }
                if (!String.IsNullOrEmpty(titleString)) { updateString += "title='" + titleString + "' AND "; }
                if (!String.IsNullOrEmpty(descriptionString)) { updateString += "description='" + descriptionString + "' AND "; }
                if (!String.IsNullOrEmpty(vendorString)) { updateString += "vendor='" + vendorString + "' AND "; }
                if (!String.IsNullOrEmpty(listPriceString)) { updateString += "listPrice='" + listPriceString + "' AND "; }
                if (!String.IsNullOrEmpty(costString)) { updateString += "Cost='" + costString + "' AND "; }
                if (!String.IsNullOrEmpty(statusString)) { updateString += "Status='" + statusString + "',"; }
                if (!String.IsNullOrEmpty(locationString)) { updateString += "Location='" + locationString + "' AND "; }
                if (!String.IsNullOrEmpty(dateCreatedString)) { updateString += "dateCreated='" + dateCreatedString + "' AND "; }
                if (!String.IsNullOrEmpty(dateRecievedString)) { updateString += "dateRecieved='" + dateRecievedString + "' AND "; }

                //Removes the AND from the last string, needed to properly format the query
                updateString = updateString.Remove(updateString.Length - 4);

                updateString += ";";

                //Create the sql command
                cmd = new SqlCommand(updateString, con);

                //Open the connection and execute the query
                con.Open();

                cmd.ExecuteNonQuery();
            }
            //This only throws an exception when trying to delete the all the rows,
            //   as long as the exception is caught it will do is successfully
            catch (Exception)
            {
            }
            //Close the connection
            finally
            {
                con.Close();
            }

            //Remove the rows that were deleted from the datatable
            foreach (DataRow r in table.Rows)
            {
                MainWindow.updateDelete(r[0].ToString());
            }
            //Close the window
            this.Close();
        }

        //If cancel is clicked then close the window
        private void cancelClicked(Object sender, EventArgs e)
        {
            this.Close();
        }

        //This creates the windows
        private void init()
        {
            this.abcID = new Label();
            this.abcID.Text = "ABDID: ";
            this.abcID.Location = new System.Drawing.Point(10, 10);
            this.abcID.Font = new Font(abcID.Font, FontStyle.Bold);

            this.abcIDBox = new TextBox();
            this.abcIDBox.Location = new System.Drawing.Point(150, 10);
            this.abcIDBox.MinimumSize = new Size(30, 30);
            this.abcIDBox.Size = new Size(abcIDBox.Size.Width, 30);


            this.title = new Label();
            this.title.Text = "Title: ";
            this.title.Location = new System.Drawing.Point(10, 40);
            this.title.Font = new Font(title.Font, FontStyle.Bold);

            this.titleBox = new TextBox();
            this.titleBox.Location = new System.Drawing.Point(150, 40);
            this.titleBox.MinimumSize = new Size(30, 30);
            this.titleBox.Size = new Size(titleBox.Size.Width, 30);


            this.description = new Label();
            this.description.Text = "Description: ";
            this.description.Location = new System.Drawing.Point(10, 70);
            this.description.Font = new Font(description.Font, FontStyle.Bold);

            this.descriptionBox = new TextBox();
            this.descriptionBox.Location = new System.Drawing.Point(150, 70);
            this.descriptionBox.MinimumSize = new Size(30, 30);
            this.descriptionBox.Size = new Size(descriptionBox.Size.Width, 30);


            this.vendor = new Label();
            this.vendor.Text = "Vendor: ";
            this.vendor.Location = new System.Drawing.Point(10, 110);
            this.vendor.Font = new Font(vendor.Font, FontStyle.Bold);

            this.vendorBox = new TextBox();
            this.vendorBox.Location = new System.Drawing.Point(150, 110);
            this.vendorBox.MinimumSize = new Size(30, 30);
            this.vendorBox.Size = new Size(vendorBox.Size.Width, 30);


            this.listPrice = new Label();
            this.listPrice.Text = "List Price: ";
            this.listPrice.Location = new System.Drawing.Point(10, 140);
            this.listPrice.Font = new Font(listPrice.Font, FontStyle.Bold);

            this.listPriceBox = new TextBox();
            this.listPriceBox.Location = new System.Drawing.Point(150, 140);
            this.listPriceBox.MinimumSize = new Size(30, 30);
            this.listPriceBox.Size = new Size(listPriceBox.Size.Width, 30);

            this.cost = new Label();
            this.cost.Text = "Cost: ";
            this.cost.Location = new System.Drawing.Point(10, 170);
            this.cost.Font = new Font(cost.Font, FontStyle.Bold);

            this.costBox = new TextBox();
            this.costBox.Location = new System.Drawing.Point(150, 170);
            this.costBox.MinimumSize = new Size(30, 30);
            this.costBox.Size = new Size(costBox.Size.Width, 30);


            this.status = new Label();
            this.status.Text = "Status: ";
            this.status.Location = new System.Drawing.Point(10, 200);
            this.status.Font = new Font(status.Font, FontStyle.Bold);

            this.statusBox = new TextBox();
            this.statusBox.Location = new System.Drawing.Point(150, 200);
            this.statusBox.MinimumSize = new Size(30, 30);
            this.statusBox.Size = new Size(statusBox.Size.Width, 30);


            this.location = new Label();
            this.location.Text = "Location: ";
            this.location.Location = new System.Drawing.Point(10, 230);
            this.location.Font = new Font(location.Font, FontStyle.Bold);

            this.locationBox = new TextBox();
            this.locationBox.Location = new System.Drawing.Point(150, 230);
            this.locationBox.MinimumSize = new Size(30, 30);
            this.locationBox.Size = new Size(locationBox.Size.Width, 30);


            this.dateCreated = new Label();
            this.dateCreated.Text = "Date Created: ";
            this.dateCreated.Location = new System.Drawing.Point(10, 260);
            this.dateCreated.Font = new Font(dateCreated.Font, FontStyle.Bold);

            this.dateCreatedBox = new TextBox();
            this.dateCreatedBox.Location = new System.Drawing.Point(150, 260);
            this.dateCreatedBox.MinimumSize = new Size(30, 30);
            this.dateCreatedBox.Size = new Size(dateCreatedBox.Size.Width, 30);


            this.dateRecieved = new Label();
            this.dateRecieved.Text = "Date Recieved: ";
            this.dateRecieved.Location = new System.Drawing.Point(10, 290);
            this.dateRecieved.Font = new Font(dateRecieved.Font, FontStyle.Bold);

            this.dateRecievedBox = new TextBox();
            this.dateRecievedBox.Location = new System.Drawing.Point(150, 290);
            this.dateRecievedBox.MinimumSize = new Size(30, 30);
            this.dateRecievedBox.Size = new Size(dateRecievedBox.Size.Width, 30);


            this.button = new Button();
            this.button.Text = "Okay";
            this.button.Location = new System.Drawing.Point(50, 320);
            this.button.Click += new EventHandler(this.okayClicked);
            this.button.Font = new Font(button.Font, FontStyle.Bold);
            this.button.BackColor = System.Drawing.Color.DeepSkyBlue;

            this.cancelButton = new Button();
            this.cancelButton.Text = "Cancel";
            this.cancelButton.Location = new System.Drawing.Point(170, 320);
            this.cancelButton.Click += new EventHandler(this.cancelClicked);
            this.cancelButton.Font = new Font(cancelButton.Font, FontStyle.Bold);
            this.cancelButton.BackColor = System.Drawing.Color.DeepSkyBlue;

            this.ClientSize = new System.Drawing.Size(300, 350);
            this.BackColor = System.Drawing.Color.LightGray;

            this.Controls.Add(abcID);
            this.Controls.Add(title);
            this.Controls.Add(description);
            this.Controls.Add(vendor);
            this.Controls.Add(listPrice);
            this.Controls.Add(cost);
            this.Controls.Add(status);
            this.Controls.Add(location);
            this.Controls.Add(dateCreated);
            this.Controls.Add(dateRecieved);

            this.Controls.Add(abcIDBox);
            this.Controls.Add(titleBox);
            this.Controls.Add(descriptionBox);
            this.Controls.Add(vendorBox);
            this.Controls.Add(listPriceBox);
            this.Controls.Add(costBox);
            this.Controls.Add(statusBox);
            this.Controls.Add(locationBox);
            this.Controls.Add(dateCreatedBox);
            this.Controls.Add(dateRecievedBox);

            this.Controls.Add(button);
            this.Controls.Add(cancelButton);

            this.Name = "SearchWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
