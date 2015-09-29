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
    public partial class CreateWindow : Form
    {
        //Variables needed to create the form
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

        //String to hold the data that is input
        //   into the textboxes
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

        //Create the windows form
        public CreateWindow()
        {
            init();
        }

        //If cancel is clicked then this closes the window
        private void cancelClicked(Object sender, EventArgs e)
        {
            this.Close();
        }

        //If okay is clicked this adds the information the user input 
        //   and updates the data table
        private void okayClicked(Object sender, EventArgs e)
        {
           //Getting the information from the textboxes
            abcIDString = abcIDBox.Text;
            titleString = titleBox.Text;
            descriptionString = descriptionBox.Text; 
            vendorString = vendorBox.Text; 
            listPriceString = listPriceBox.Text;
            costString =  costBox.Text;
            statusString = statusBox.Text; 
            locationString = locationBox.Text; 
            dateCreatedString = dateCreatedBox.Text;
            dateRecievedString = dateRecievedBox.Text;

            //Connect to the database
            using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\" + MainWindow.getDBName() + ".mdf;Integrated Security=True"))
            {
                
                //Create the command to enter the information into the database
                 SqlCommand cmd = new SqlCommand("INSERT into MainTable (abcId,title,description,vendor,listPrice,Cost,Status,Location,dateCreated,dateRecieved) VALUES " +
                     "(@abcId,@title,@description,@vendor,@listPrice,@Cost,@Status,@Location,@dateCreated,@dateRecieved)",con);
                 cmd.CommandType = CommandType.Text;
                 cmd.Connection = con;

                //If the user didn't enter any information we just add null to that index
                 if (String.IsNullOrEmpty(abcIDString)) { abcIDString = "null"; }
                 if (String.IsNullOrEmpty(titleString)) { titleString = "null"; }
                 if (String.IsNullOrEmpty(descriptionString)) { descriptionString ="null"; }
                 if (String.IsNullOrEmpty(vendorString)) { vendorString = "null"; }
                 if (String.IsNullOrEmpty(listPriceString)) { listPriceString = "null"; }
                 if (String.IsNullOrEmpty(costString)) { costString = "null"; }
                 if (String.IsNullOrEmpty(statusString)) { statusString = "null"; }
                 if (String.IsNullOrEmpty(locationString)) { locationString = "null"; }
                 if (String.IsNullOrEmpty(dateCreatedString)) { dateCreatedString = "null"; }
                 if (String.IsNullOrEmpty(dateRecievedString)) { dateRecievedString = "null"; }

                //Add the values
                 cmd.Parameters.AddWithValue("@abcId",abcIDString);
                 cmd.Parameters.AddWithValue("@title", titleString);
                 cmd.Parameters.AddWithValue("@description",descriptionString);
                 cmd.Parameters.AddWithValue("@vendor", vendorString);
                 cmd.Parameters.AddWithValue("@listPrice", listPriceString);
                 cmd.Parameters.AddWithValue("@Cost", costString);
                 cmd.Parameters.AddWithValue("@Status", statusString);
                 cmd.Parameters.AddWithValue("@Location", locationString);
                 cmd.Parameters.AddWithValue("@dateCreated", dateCreatedString);
                 cmd.Parameters.AddWithValue("@dateRecieved", dateRecievedString);
                 
                try
                {
                   //The primary key can not be null, so if it is then we throw an exception
                   if (String.IsNullOrEmpty(abcIDString) || abcIDString.Equals("null")) { throw new Exception("Primary Key is NULL");}
                   //Open the connection, execute the query, update the datatable
                    con.Open();
                   cmd.ExecuteNonQuery();
                   MainWindow.updateCreate(abcIDString, titleString, descriptionString, vendorString, listPriceString, costString, statusString, locationString, dateCreatedString, dateRecievedString);
                   MessageBox.Show("Successfully created entry in database");
                }
                //Exception if the ID is already in the datatable
                catch (Exception)
                {
                    if (abcIDString.Equals("null")) { MessageBox.Show("Cannot have a blank ID"); }
                    else { MessageBox.Show("Entry already exists with that ID"); }
                }
                //Close the connection
                finally
                {
                    con.Close();
                }
             
            }
            //Close the window
            this.Close();
        }

        //Creates the windows form
        private void init()
        {
            this.abcID = new Label();
            this.abcID.Text = "ABDID: (required)";
            this.abcID.Location = new System.Drawing.Point(10, 10);
            this.abcID.Font = new System.Drawing.Font(abcID.Font, FontStyle.Bold);

            this.abcIDBox = new TextBox();
            this.abcIDBox.Location = new System.Drawing.Point(150, 10);
            this.abcIDBox.MinimumSize = new Size(30, 30);
            this.abcIDBox.Size = new Size(abcIDBox.Size.Width, 30);


            this.title = new Label();
            this.title.Text = "Title: ";
            this.title.Location = new System.Drawing.Point(10, 40);
            this.title.Font = new System.Drawing.Font(title.Font, FontStyle.Bold);

            this.titleBox = new TextBox();
            this.titleBox.Location = new System.Drawing.Point(150, 40);
            this.titleBox.MinimumSize = new Size(30, 30);
            this.titleBox.Size = new Size(titleBox.Size.Width, 30);


            this.description = new Label();
            this.description.Text = "Description: ";
            this.description.Location = new System.Drawing.Point(10, 70);
            this.description.Font = new System.Drawing.Font(description.Font, FontStyle.Bold);

            this.descriptionBox = new TextBox();
            this.descriptionBox.Location = new System.Drawing.Point(150,70);
            this.descriptionBox.MinimumSize = new Size(30, 30);
            this.descriptionBox.Size = new Size(descriptionBox.Size.Width, 30);


            this.vendor = new Label();
            this.vendor.Text = "Vendor: ";
            this.vendor.Location = new System.Drawing.Point(10, 110);
            this.vendor.Font = new System.Drawing.Font(vendor.Font, FontStyle.Bold);

            this.vendorBox = new TextBox();
            this.vendorBox.Location = new System.Drawing.Point(150, 110);
            this.vendorBox.MinimumSize = new Size(30, 30);
            this.vendorBox.Size = new Size(vendorBox.Size.Width, 30);


            this.listPrice = new Label();
            this.listPrice.Text = "List Price: ";
            this.listPrice.Location = new System.Drawing.Point(10, 140);
            this.listPrice.Font = new System.Drawing.Font(listPrice.Font, FontStyle.Bold);

            this.listPriceBox = new TextBox();
            this.listPriceBox.Location = new System.Drawing.Point(150, 140);
            this.listPriceBox.MinimumSize = new Size(30, 30);
            this.listPriceBox.Size = new Size(listPriceBox.Size.Width, 30);


            this.cost = new Label();
            this.cost.Text = "Cost: ";
            this.cost.Location = new System.Drawing.Point(10, 170);
            this.cost.Font = new System.Drawing.Font(cost.Font, FontStyle.Bold);

            this.costBox = new TextBox();
            this.costBox.Location = new System.Drawing.Point(150, 170);
            this.costBox.MinimumSize = new Size(30, 30);
            this.costBox.Size = new Size(costBox.Size.Width, 30);


            this.status = new Label();
            this.status.Text = "Status: ";
            this.status.Location = new System.Drawing.Point(10, 200);
            this.status.Font = new System.Drawing.Font(status.Font, FontStyle.Bold);

            this.statusBox = new TextBox();
            this.statusBox.Location = new System.Drawing.Point(150, 200);
            this.statusBox.MinimumSize = new Size(30, 30);
            this.statusBox.Size = new Size(statusBox.Size.Width, 30);


            this.location = new Label();
            this.location.Text = "Location: ";
            this.location.Location = new System.Drawing.Point(10, 230);
            this.location.Font = new System.Drawing.Font(location.Font, FontStyle.Bold);

            this.locationBox = new TextBox();
            this.locationBox.Location = new System.Drawing.Point(150, 230);
            this.locationBox.MinimumSize = new Size(30, 30);
            this.locationBox.Size = new Size(locationBox.Size.Width, 30);


            this.dateCreated = new Label();
            this.dateCreated.Text = "Date Created: ";
            this.dateCreated.Location = new System.Drawing.Point(10, 260);
            this.dateCreated.Font = new System.Drawing.Font(dateCreated.Font, FontStyle.Bold);

            this.dateCreatedBox = new TextBox();
            this.dateCreatedBox.Location = new System.Drawing.Point(150, 260);
            this.dateCreatedBox.MinimumSize = new Size(30, 30);
            this.dateCreatedBox.Size = new Size(dateCreatedBox.Size.Width, 30);

            this.dateRecieved = new Label();
            this.dateRecieved.Text = "Date Recieved: ";
            this.dateRecieved.Location = new System.Drawing.Point(10, 290);
            this.dateRecieved.Font = new System.Drawing.Font(dateRecieved.Font, FontStyle.Bold);

            this.dateRecievedBox = new TextBox();
            this.dateRecievedBox.Location = new System.Drawing.Point(150, 290);
            this.dateRecievedBox.MinimumSize = new Size(30, 30);
            this.dateRecievedBox.Size = new Size(dateRecievedBox.Size.Width, 30);


            this.button = new Button();
            this.button.Text = "Okay";
            this.button.Location = new System.Drawing.Point(50, 320);
            this.button.Click += new EventHandler(this.okayClicked);
            this.button.Font = new System.Drawing.Font(button.Font, FontStyle.Bold);
            this.button.BackColor = System.Drawing.Color.DeepSkyBlue;

            this.cancelButton = new Button();
            this.cancelButton.Text = "Cancel";
            this.cancelButton.Location = new System.Drawing.Point(170, 320);
            this.cancelButton.Click += new EventHandler(this.cancelClicked);
            this.cancelButton.Font = new System.Drawing.Font(cancelButton.Font, FontStyle.Bold);
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

            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

    }
}
