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
    public partial class Form5 : Form
    {
        MySqlConnection conn;
        public Form5()
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
            //To check whether the user has entered all the mandatory details
            try
            {
                if (textBox2.Text.Equals("First Name *")  || textBox4.Text.Equals("") || textBox6.Text.Equals("yyyy/mm/dd"))
                {
                    MessageBox.Show("Enter All Details");
                }else if(!(textBox3.Text.EndsWith("@gmail.com")) && !textBox3.Text.Equals("")) {
                    MessageBox.Show("Invalid Email");
                }
                else if (textBox4.Text.Length != 10)
                {
                    MessageBox.Show("Mobile Number must have 10 digits");
                }
                else
                {
                    //Updates the profile
                    connect();
                    MySqlCommand cm = new MySqlCommand();
                    cm.Connection = conn;
                    if(Form1.utype.Equals("Car Customer"))
                        cm.CommandText = "update customer_profile set fname = @fname,mname = @mname,lname = @lname,email = @email,mobile = @mobile,dob = @dob where username = @username ";
                    else if (Form1.utype.Equals("Banker"))
                            cm.CommandText = "update banker_profile set fname = @fname,mname = @mname,lname = @lname,email = @email,mobile = @mobile,dob = @dob where username = @username ";
                    else if (Form1.utype.Equals("Sales Person"))
                                cm.CommandText = "update salesper_profile set fname = @fname,mname = @mname,lname = @lname,email = @email,mobile = @mobile,dob = @dob where username = @username ";
                    cm.Parameters.AddWithValue("@username", textBox1.Text);
                    cm.Parameters.AddWithValue("@fname", textBox2.Text);
                    if (textBox7.Text.Equals("Middle Name"))
                    {
                        textBox7.Text = null;
                    }
                    cm.Parameters.AddWithValue("@mname", textBox7.Text);
                    if (textBox8.Text.Equals("Last Name"))
                    {
                        
                        textBox8.Text = null;
                    }
                    cm.Parameters.AddWithValue("@lname", textBox8.Text);
                    if (textBox3.Text.Equals(""))
                    {

                        textBox3.Text = null;
                    }
                    cm.Parameters.AddWithValue("@email", textBox3.Text);
                    cm.Parameters.AddWithValue("@mobile", textBox4.Text);
                    cm.Parameters.AddWithValue("@dob", textBox6.Text);
                    cm.CommandType = CommandType.Text;
                    cm.ExecuteNonQuery();
                    this.Close();
                    //Open respective users profile
                    if (Form1.utype.Equals("Car Customer"))
                    {
                        
                        Form4 newform = new Form4();
                        newform.Show();
                    }
                    else if (Form1.utype.Equals("Banker"))
                    {
                        Form3 newform = new Form3();
                        newform.Show();
                    }
                    else if (Form1.utype.Equals("Sales Person"))
                    {
                        Form7 newform = new Form7();
                        newform.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Date Format or Mobile number");
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text.Equals("First Name *"))
            {
                textBox2.Text = "";
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text.Equals(""))
            {
                textBox2.Text = "First Name *";
            }

        }

        private void textBox7_Enter(object sender, EventArgs e)
        {
            if (textBox7.Text.Equals("Middle Name"))
            {
                textBox7.Text = "";
            }
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            if (textBox7.Text.Equals(""))
            {
                textBox7.Text = "Middle Name";
            }
        }

        private void textBox8_Leave(object sender, EventArgs e)
        {
            if (textBox8.Text.Equals(""))
            {
                textBox8.Text = "Last Name";
            }
        }

        private void textBox8_Enter(object sender, EventArgs e)
        {
            if (textBox8.Text.Equals("Last Name"))
            {
                textBox8.Text = "";
            }
        }

        private void textBox6_Enter(object sender, EventArgs e)
        {
            if (textBox6.Text.Equals("yyyy/mm/dd"))
            {
                textBox6.Text = "";
            }
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            if (textBox6.Text.Equals(""))
            {
                textBox6.Text = "yyyy/mm/dd";
            }
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            //Copies the profile data,if exists 
            connect();
            textBox1.Text = Form1.uname;
            MySqlCommand cm1 = new MySqlCommand();
            cm1.Connection = conn;
            if (Form1.utype.Equals("Car Customer"))
                cm1.CommandText = "SELECT * from customer_profile WHERE username = @username ";
            else if (Form1.utype.Equals("Banker"))
                cm1.CommandText = "SELECT * from banker_profile WHERE username = @username ";
            else if (Form1.utype.Equals("Sales Person"))
                cm1.CommandText = "SELECT * from salesper_profile WHERE username = @username ";
            cm1.Parameters.AddWithValue("@username", textBox1.Text);
            MySqlDataReader dr = cm1.ExecuteReader();
            if (dr.Read())
            {
                if (!(dr["fname"].ToString().Equals("")))
                {
                    textBox2.Text = dr["fname"].ToString();
                    textBox7.Text = dr["mname"].ToString();
                    textBox8.Text = dr["lname"].ToString();
                    textBox3.Text = dr["email"].ToString();
                    textBox4.Text = dr["mobile"].ToString();
                    textBox6.Text = DateTime.Parse(dr["dob"].ToString()).ToString("yyyy/MM/dd");
                }
            }
            dr.Close();
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Cancel
            if (textBox2.Text.Equals("First Name *") || textBox4.Text.Equals("") || textBox6.Text.Equals(""))
            {
                this.Close();
            }
            else
            {
                if (Form1.utype.Equals("Car Customer"))
                {
                    this.Close();
                    Form4 newform = new Form4();
                    newform.Show();
                }
                else if (Form1.utype.Equals("Banker"))
                {
                    this.Close();
                    Form3 newform = new Form3();
                    newform.Show();
                }
                else if (Form1.utype.Equals("Sales Person"))
                {
                    this.Close();
                    Form7 newform = new Form7();
                    newform.Show();
                }
            }
        }
    }
}
