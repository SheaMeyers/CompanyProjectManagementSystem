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
    public partial class UpdateWindow : Form
    {
        //Variables for the forms
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

        //Holds the input from the textboxes to 
        //   put into the database query
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

        //Call to create the form
        public UpdateWindow()
        {
            init();
        }

        //When okay is clicked, meaning the user input their information
        //   and wants to update, this function is called
        private void okayClicked(Object sender, EventArgs e)
        {
            //Get information from textboxes
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

            //Create string for database call
            String updateString = "UPDATE MainTable \n SET ";

            //Add the information to the database query
            if (!String.IsNullOrEmpty(titleString)) { updateString += "title='" + titleString + "',"; }
            if (!String.IsNullOrEmpty(descriptionString)) { updateString += "description='" + descriptionString + "',"; }
            if (!String.IsNullOrEmpty(vendorString)) { updateString += "vendor='" + vendorString + "',"; }
            if (!String.IsNullOrEmpty(listPriceString)) { updateString += "listPrice='" + listPriceString + "',"; }
            if (!String.IsNullOrEmpty(costString)) { updateString += "Cost='" + costString + "',"; }
            if (!String.IsNullOrEmpty(statusString)) { updateString += "Status='" + statusString + "',"; }
            if (!String.IsNullOrEmpty(locationString)) { updateString += "Location='" + locationString + "',"; }
            if (!String.IsNullOrEmpty(dateCreatedString)) { updateString += "dateCreated='" + dateCreatedString + "',"; }
            if (!String.IsNullOrEmpty(dateRecievedString)) { updateString += "dateRecieved='" + dateRecievedString + "',"; }

            //Remove the last , needed to format the string properly
            updateString = updateString.Remove(updateString.Length - 1);

            //Add the where clause to update the correct row
            updateString += "\nWHERE abcId='" + abcIDString + "';";

            //Connect to the database
            using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\" + MainWindow.getDBName() + ".mdf;Integrated Security=True"))
            {
                //Create the database call
                SqlCommand cmd = new SqlCommand(updateString, con);

                try
                {
                    //Open the connection to the database, execute the update statement, and update the data table
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MainWindow.updateUpdate(abcIDString, titleString, descriptionString, vendorString, listPriceString, costString, statusString, locationString, dateCreatedString, dateRecievedString);
                }
                //If the user entered an ID that is not in the database this lets the user know
                catch (SqlException)
                {
                    MessageBox.Show("Could not find entry, update not completed");
                }
                //Close the connection
                finally
                {
                    con.Close();
                }
            }
            //Close the window, gets back to the main window
            this.Close();
        }

        //Closes the window if cancel is called
        private void cancelClicked(Object sender, EventArgs e)
        {
            this.Close();
        }

        //Function to create the windows form
        private void init()
        {
            this.abcID = new Label();
            this.abcID.Text = "ABDID: (required)";
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

            this.Name = "UpdateWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
