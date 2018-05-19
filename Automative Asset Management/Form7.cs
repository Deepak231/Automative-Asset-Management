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
    public partial class Form7 : Form
    {
        MySqlConnection conn;
        MySqlDataReader dr;
        string val1;
        public Form7()
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

        private void button2_Click(object sender, EventArgs e)
        {
            Form6 newform = new Form6();
            newform.Show();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            //Loads sales persons profile
            connect();
            MySqlCommand cm = new MySqlCommand();
            cm.Connection = conn;
            cm.CommandText = "SELECT * from salesper_profile WHERE username = @username ";
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
                this.Close();
                Form5 newform = new Form5();
                newform.Show();
            }
            //To insert bankers name in the combobox
            dr.Close();
            conn.Close();
            connect();
            MySqlCommand cm1 = new MySqlCommand();
            cm1.Connection = conn;
            cm1.CommandText = "SELECT username from banker";
            dr = cm1.ExecuteReader();
            while (dr.Read())
            {
                    comboBox2.Items.Add(dr["username"].ToString());
            }
            dr.Close();
            conn.Close();
        }

        private void Form7_Activated(object sender, EventArgs e)
        {
            //To load the loan request from the customer
            connect();
            MySqlCommand cm1 = new MySqlCommand();
            cm1.CommandText = "select c_username,loan_amt from loan where s_username = '"+textBox1.Text+"'";
            cm1.CommandType = CommandType.Text;
            MySqlDataAdapter sd = new MySqlDataAdapter(cm1.CommandText, conn);
            DataSet ds = new DataSet();
            sd.Fill(ds, "loan");
            dataGridView1.DataSource = ds.Tables[0];
            conn.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //To send the customers loan request to the banker
            if (val1.Equals(""))
            {
                MessageBox.Show("Select the customer");
            } else
            if (comboBox2.Text.Equals(""))
            {
                MessageBox.Show("Select the Banker");
            }
            else
            {
                try
                {
                    //since sales_person shouldn't send the same customers loan request to 2 or more bankers
                    //each time update happens,trigger will insert customer_name to the 'loan_send'
                    connect();
                    MySqlCommand cm = new MySqlCommand();
                    cm.Connection = conn;
                    cm.CommandText = "update loan set b_username = @b_username where c_username = @c_username ";
                    cm.Parameters.AddWithValue("@b_username", comboBox2.Text);
                    cm.Parameters.AddWithValue("@c_username", val1);
                    cm.CommandType = CommandType.Text;
                    cm.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Request has  been sent");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Request has already been sent");
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //stores customer name
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                val1 = row.Cells[0].Value.ToString();
            }
        }
    }
}
