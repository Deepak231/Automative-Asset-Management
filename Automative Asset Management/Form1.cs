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
    public partial class Form1 : Form
    {
        public static string uname,utype;
        MySqlConnection conn;
        public Form1()
        {
            InitializeComponent();
        }
        public void connect()
        {
            string MyConnection = "server=localhost;user id=root;password = 12345 ;database=database1;persistsecurityinfo=True";
            conn = new MySqlConnection(MyConnection);
            conn.Open();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //checking whether the user has entered all the details
            if (textBox1.Text.Equals("") || textBox2.Text.Equals("")  || comboBox1.Text.Equals(""))
            {
                MessageBox.Show("Please Enter All The Details");
            } else
            if (comboBox1.Text.Equals("Banker"))
            {
                try
                {
                    // Banker Sign in 
                    connect();
                    MySqlCommand cm = new MySqlCommand();
                    cm.Connection = conn;
                    //Checking whether the account exist having the enetered username and password
                    cm.CommandText = "SELECT count(*) from banker WHERE username = @username and password=@password";
                    cm.Parameters.AddWithValue("@username", textBox1.Text);
                    cm.Parameters.AddWithValue("@password", textBox2.Text);
                    cm.CommandType = CommandType.Text;
                    cm.ExecuteNonQuery();
                    var count = cm.ExecuteScalar();
                    if (count.ToString().Equals("1"))
                    {
                        uname = textBox1.Text;
                        utype = comboBox1.Text;
                        comboBox1.Text = "";
                        textBox1.Text = "";
                        textBox2.Text = "";
                        Form3 newForm = new Form3();
                        newForm.Show();

                    }
                    else
                    {
                        MessageBox.Show("Username or Password is Incorrect");
                        textBox1.Text = "";
                        textBox2.Text = "";
                    }
                    conn.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unknown Error");
                }
            }
            else if (comboBox1.Text.Equals("Car Customer"))
            {
                try
                {
                    // Car Customer Sign in 
                    connect();
                    MySqlCommand cm = new MySqlCommand();
                    cm.Connection = conn;
                    //Checking whether the account exist having the enetered username and password
                    cm.CommandText = "SELECT count(*) from car_customer WHERE username = @username and password=@password";
                    cm.Parameters.AddWithValue("@username", textBox1.Text);
                    cm.Parameters.AddWithValue("@password", textBox2.Text);
                    cm.CommandType = CommandType.Text;
                    cm.ExecuteNonQuery();
                    var count = cm.ExecuteScalar();
                    if (count.ToString().Equals("1"))
                    {
                        uname = textBox1.Text;
                        utype = comboBox1.Text;
                        comboBox1.Text = "";
                        textBox1.Text = "";
                        textBox2.Text = "";
                        Form4 newForm = new Form4();
                        newForm.Show();

                    }
                    else
                    {
                        MessageBox.Show("Username or Password is Incorrect");
                        textBox1.Text = "";
                        textBox2.Text = "";
                    }
                    conn.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unknown Error");
                }
            }
            else if (comboBox1.Text.Equals("Sales Person"))
            {
                try
                {
                    // Sales Person Sign in 
                    connect();
                    MySqlCommand cm = new MySqlCommand();
                    cm.Connection = conn;
                    //Checking whether the account exist having the entered username and password
                    cm.CommandText = "SELECT count(*) from sales_person WHERE username = @username and password=@password";
                    cm.Parameters.AddWithValue("@username", textBox1.Text);
                    cm.Parameters.AddWithValue("@password", textBox2.Text);
                    cm.CommandType = CommandType.Text;
                    cm.ExecuteNonQuery();
                    var count = cm.ExecuteScalar();
                    if (count.ToString().Equals("1"))
                    {
                        uname = textBox1.Text;
                        utype = comboBox1.Text;
                        comboBox1.Text = "";
                        textBox1.Text = "";
                        textBox2.Text = "";
                        Form7 newForm = new Form7();
                        newForm.Show();

                    }
                    else
                    {
                        MessageBox.Show("Username or Password is Incorrect");
                        textBox1.Text = "";
                        textBox2.Text = "";
                    }
                    conn.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unknown Error");
                }
            }
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Signing up link
            Form2 newForm = new Form2();
            newForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Cancel
            this.Close();
        }
    }
}
