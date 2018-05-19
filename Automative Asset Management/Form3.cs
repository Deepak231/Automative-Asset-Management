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
    public partial class Form3 : Form
    {
        MySqlConnection conn;
        MySqlDataReader dr;
        string val1;
        public Form3()
        {
            InitializeComponent();
            textBox1.Text = Form1.uname;
        }
        public void connect()
        {
            string MyConnection = "server=localhost;user id=root;password = 12345 ;database=database1;persistsecurityinfo=True";
            conn = new MySqlConnection(MyConnection);
            conn.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form5 newform = new Form5();
            newform.Show();
            this.Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            //Copies all the bankers details to the respective textbox,to view the profile
            connect();
            MySqlCommand cm = new MySqlCommand();
            cm.Connection = conn;
            cm.CommandText = "SELECT * from banker_profile WHERE username = @username ";
            cm.Parameters.AddWithValue("@username", textBox1.Text);
            dr = cm.ExecuteReader();
            if (dr.Read())
            {
                if (!(dr["dob"].ToString().Equals("")))
                {
                    textBox2.Text = dr["fname"].ToString();
                    textBox7.Text = dr["mname"].ToString();
                    textBox8.Text = dr["lname"].ToString();
                    textBox3.Text = dr["email"].ToString();
                    textBox4.Text = dr["mobile"].ToString();
                    textBox6.Text = DateTime.Parse(dr["dob"].ToString()).ToString("yyyy/MM/dd");
                }
            }
            if (textBox2.Text.Equals(""))
            {
                //If banker has not filled his profile,it will take to the profile form
                this.Close();
                Form5 newform = new Form5();
                newform.Show();
            }
            dr.Close();
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //To Update the password, password form
            Form6 newform = new Form6();
            newform.Show();
        }

 

        private void Form3_Activated(object sender, EventArgs e)
        {
            //Displays Customer who has taken the loan with their respective sales person
            connect();
            MySqlCommand cm1 = new MySqlCommand();
            cm1.CommandText = "select c_username,s_username,loan_amt from loan where b_username = '" + textBox1.Text + "'";
            cm1.CommandType = CommandType.Text;
            MySqlDataAdapter sd = new MySqlDataAdapter(cm1.CommandText, conn);
            DataSet ds = new DataSet();
            sd.Fill(ds, "loan");
            dataGridView1.DataSource = ds.Tables[0];
            conn.Close();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //For storing the selected rows Customer name
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                val1 = row.Cells[0].Value.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //For accepting the loan request
            if (val1.Equals(""))
            {
                MessageBox.Show("Select the customer");
            }
            else
            {
                    connect();
                    MySqlCommand cm = new MySqlCommand();
                    cm.Connection = conn;
                    //delete from 'loan_send' since the loan has been sent
                    cm.CommandText = "delete from loan_send where c_username = @c_username ";
                    cm.Parameters.AddWithValue("@c_username", val1);
                    cm.CommandType = CommandType.Text;
                    cm.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show(val1 + "  loan request has been accepted");
                    connect();
                    MySqlCommand cm4 = new MySqlCommand();
                    cm4.Connection = conn;
                    //delete from inventory (Customer payed the loan money to the car company)
                    cm4.CommandText = "delete from inventory where username = @username ";
                    cm4.Parameters.AddWithValue("@username", val1);
                    cm4.CommandType = CommandType.Text;
                    cm4.ExecuteNonQuery();
                    conn.Close();
            }
        }
    }
}
