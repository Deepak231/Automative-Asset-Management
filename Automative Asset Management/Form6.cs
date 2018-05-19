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
    public partial class Form6 : Form
    {
        MySqlConnection conn;
        public Form6()
        {
            InitializeComponent();
        }
        public void connect()
        {
            string MyConnection = "server=localhost;user id=root;password = 12345 ;database=database1;persistsecurityinfo=True";
            conn = new MySqlConnection(MyConnection);
            conn.Open();
        }
        private void Form6_Load(object sender, EventArgs e)
        {
            textBox1.Text = Form1.uname;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //To change the passsword
            //To check whether the old password matches the entered
                connect();
                MySqlCommand cm = new MySqlCommand();
                cm.Connection = conn;
                if (Form1.utype.Equals("Car Customer"))
                    cm.CommandText = "SELECT password from car_customer WHERE username = @username";
                else if (Form1.utype.Equals("Banker"))
                    cm.CommandText = "SELECT password from banker WHERE username = @username";
                else if (Form1.utype.Equals("Sales Person"))
                    cm.CommandText = "SELECT password from sales_person WHERE username = @username";
                cm.Parameters.AddWithValue("@username", textBox1.Text);
                MySqlDataReader dr = cm.ExecuteReader();
                string pass = "";
                if (dr.Read())
                {
                    pass = dr["password"].ToString();
                }
                conn.Close();
                if (textBox2.Text.Equals("") || textBox3.Text.Equals("") || textBox4.Text.Equals(""))
                {
                    MessageBox.Show("Enter all details");
                }
                else if (!(pass.Equals(textBox2.Text)))
                {
                    MessageBox.Show("Incorrect password");
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                }
                else if (!(textBox3.Text.Equals(textBox4.Text)))
                {
                    MessageBox.Show("Password Doesn't Match");
                    textBox3.Text = "";
                    textBox4.Text = "";
                }
                else
                {
                    //Updates the current password to the new password
                    connect();
                    MySqlCommand cm1 = new MySqlCommand();
                    cm1.Connection = conn;
                    if (Form1.utype.Equals("Car Customer"))
                        cm1.CommandText = "update car_customer set password = @password where username = @username ";
                    else if (Form1.utype.Equals("Banker"))
                        cm1.CommandText = "update banker set password = @password where username = @username ";
                    else if (Form1.utype.Equals("Sales Person"))
                        cm1.CommandText = "update sales_person set password = @password where username = @username ";
                    cm1.Parameters.AddWithValue("@username", textBox1.Text);
                    cm1.Parameters.AddWithValue("@password", textBox3.Text);
                    cm1.CommandType = CommandType.Text;
                    cm1.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Password changed");
                    this.Close();
                }
        }
    }
}
