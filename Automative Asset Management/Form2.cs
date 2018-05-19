using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Automative_Asset_Management
{
    public partial class Form2 : Form
    {
        MySqlConnection conn;
        public Form2()
        {
            InitializeComponent();
        }

        public void connect()
        {
            string MyConnection = "server=localhost;user id=root;password = 12345 ;database=database1;persistsecurityinfo=True";
            conn = new MySqlConnection(MyConnection);
            conn.Open();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //User can create account,only if he accepts terms and condition
            if (checkBox1.Checked.Equals(false))
            {
                button1.Enabled = false;
             
            }
            else
            {
                button1.Enabled = true;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Checking whether the user has entered all the details
            if (comboBox1.Text.Equals("")) {
                    MessageBox.Show("Please Enter All The Details");
            }
            if (comboBox1.Text.Equals("Banker"))
            {
                //Banker sign up
                if (!textBox2.Text.Equals(textBox3.Text))
                {
                    MessageBox.Show("Password doesn't Match");
                    textBox2.Text = "";
                    textBox3.Text = "";
                } else if(textBox1.Text.Equals("") || textBox2.Text.Equals("") || textBox3.Text.Equals("") ) {
                     MessageBox.Show("Please Enter All The Details");
                }
                else
                {
                    try
                    {
                        connect();
                        MySqlCommand cm = new MySqlCommand();
                        cm.Connection = conn;
                        cm.CommandText = "INSERT INTO banker(username,password) values(@username,@password)";
                        cm.Parameters.AddWithValue("@username", textBox1.Text);
                        cm.Parameters.AddWithValue("@password", textBox2.Text);
                        cm.CommandType = CommandType.Text;
                        cm.ExecuteNonQuery();
                        MessageBox.Show("You've Successfully Signed Up");
                        this.Close();
                        conn.Close();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Username is already Taken");
                    }
                }
            }
            else if (comboBox1.Text.Equals("Car Customer"))
            {
                //Car customer sign up
                if (!textBox2.Text.Equals(textBox3.Text))
                {
                    MessageBox.Show("Password doesn't Match");
                    textBox2.Text = "";
                    textBox3.Text = "";
                }
                else if (textBox1.Text.Equals("") || textBox2.Text.Equals("") || textBox3.Text.Equals(""))
                {
                    MessageBox.Show("Please Enter All The Details");
                }
                else
                {
                    try
                    {
                        connect();
                        MySqlCommand cm = new MySqlCommand();
                        cm.Connection = conn;
                        cm.CommandText = "INSERT INTO car_customer(username,password) values(@username,@password)";
                        cm.Parameters.AddWithValue("@username", textBox1.Text);
                        cm.Parameters.AddWithValue("@password", textBox2.Text);
                        cm.CommandType = CommandType.Text;
                        cm.ExecuteNonQuery();
                        MessageBox.Show("You've Successfully Signed Up");
                        this.Close();
                        conn.Close();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Username is already Taken");
                    }
                }
            }
            else if (comboBox1.Text.Equals("Sales Person"))
            {
                //Sales Person Sign up
                if (!textBox2.Text.Equals(textBox3.Text))
                {
                    MessageBox.Show("Password doesn't Match");
                    textBox2.Text = "";
                    textBox3.Text = "";
                }
                else if (textBox1.Text.Equals("") || textBox2.Text.Equals("") || textBox3.Text.Equals(""))
                {
                    MessageBox.Show("Please Enter All The Details");
                }
                else
                {
                    try
                    {
                        connect();
                        MySqlCommand cm = new MySqlCommand();
                        cm.Connection = conn;
                        cm.CommandText = "INSERT INTO sales_person(username,password) values(@username,@password)";
                        cm.Parameters.AddWithValue("@username", textBox1.Text);
                        cm.Parameters.AddWithValue("@password", textBox2.Text);
                        cm.CommandType = CommandType.Text;
                        cm.ExecuteNonQuery();
                        MessageBox.Show("You've Successfully Signed Up");
                        this.Close();
                        conn.Close();

                    }
                    catch (Exception ex)
                    {
                    
                        MessageBox.Show("Username is already Taken");
                    }
                }
            }
            
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Cancel
            this.Close();
        }
    }
}
