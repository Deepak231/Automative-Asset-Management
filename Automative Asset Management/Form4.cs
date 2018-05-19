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
    public partial class Form4 : Form
    {
        MySqlConnection conn;
        MySqlDataReader dr;
        string val1, val2, val3,val4;
        long pay,x;
        public Form4()
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
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            //To update the profile
            Form5 newform = new Form5();
            newform.Show();
            this.Close();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            //Copies the customer profile from database table to the corresponding textbox
            connect();
            MySqlCommand cm = new MySqlCommand();
            cm.Connection = conn;
            cm.CommandText = "SELECT * from customer_profile WHERE username = @username ";
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
                //If the user has not filled the profile
                this.Close();
                Form5 newform = new Form5();
                newform.Show();
            }
            dr.Close();
            conn.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form6 newform = new Form6();
            newform.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                //Insert the selected row from the store to the inventory
                connect();
                MySqlCommand cm = new MySqlCommand();
                cm.Connection = conn;
                cm.CommandText = "INSERT INTO inventory(username,car_id,car_name,price) values(@username,@car_id,@car_name,@price)";
                cm.Parameters.AddWithValue("@username", textBox1.Text);
                cm.Parameters.AddWithValue("@car_id", val1);
                cm.Parameters.AddWithValue("@car_name", val2);
                cm.Parameters.AddWithValue("@price", val3);
                cm.CommandType = CommandType.Text;
                cm.ExecuteNonQuery();
                MessageBox.Show(val1 +" "+val2+" added to the inventory");
                conn.Close();
                Form4_Activated(sender, e);

            }
            catch (Exception ex)
            {
                //To prevent inserting the same item again
                MessageBox.Show(val1+" "+ val2 +" is already in the inventory");
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //To store the selected rows values
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                val1 = row.Cells[0].Value.ToString();
                val2 = row.Cells[1].Value.ToString();
                val3 = row.Cells[2].Value.ToString();
            }
        }

        private void Form4_Activated(object sender, EventArgs e)
        {
            //inserts 'Store' table data to the  Store Datatable
            connect();
            MySqlCommand cm1 = new MySqlCommand();
            cm1.CommandText = "select * from store";
            cm1.CommandType = CommandType.Text;
            MySqlDataAdapter sd = new MySqlDataAdapter(cm1.CommandText, conn);
            DataSet ds = new DataSet();
            sd.Fill(ds, "store");
            dataGridView1.DataSource = ds.Tables[0];
            conn.Close();
            //inserts 'Inventory' table data to the Inventory Datatable
            connect();
            MySqlCommand cm2 = new MySqlCommand();
            cm2.CommandText = "select car_id,car_name,price from inventory where username =  '"+textBox1.Text+"'";
            cm2.CommandType = CommandType.Text;
            MySqlDataAdapter sd1 = new MySqlDataAdapter(cm2.CommandText, conn);
            DataSet ds1 = new DataSet();
            sd1.Fill(ds1, "inventory");
            dataGridView2.DataSource = ds1.Tables[0];
            conn.Close();
            //To find the sum of total item purchased
            connect();
            MySqlCommand cm = new MySqlCommand();
            cm.Connection = conn;
            cm.CommandText = "SELECT sum(price) from inventory where username = @username";
            cm.Parameters.AddWithValue("@username", textBox1.Text);
            cm.CommandType = CommandType.Text;
            cm.ExecuteNonQuery();
            var sum = cm.ExecuteScalar();
            conn.Close();
            if (sum.ToString().Equals(""))
            {
                textBox5.Text = "0";
            }
            else
            {
                textBox5.Text = sum.ToString();
            }

            textBox9.Text = textBox5.Text;
            connect();
            MySqlCommand cm3 = new MySqlCommand();
            cm3.Connection = conn;
            //Checking whether the banker has sent the loan
            cm3.CommandText = "select loan_amt from loan where c_username = @c_username and c_username not in"+
            "(select c_username from loan_send) and b_username is not null";
            cm3.Parameters.AddWithValue("@c_username", textBox1.Text);
            dr = cm3.ExecuteReader();
            if (dr.Read())
            {
                textBox12.Text = dr["loan_amt"].ToString();
                checkBox1.Checked = false;
            }
            dr.Close();
            conn.Close();
            if (textBox12.Text.Equals(""))
                textBox12.Text = "0";

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //To remove from the inventory
            connect();
            MySqlCommand cm = new MySqlCommand();
            cm.Connection = conn;
            cm.CommandText = "delete from inventory where username = @username and car_id = @car_id";
            cm.Parameters.AddWithValue("@username", textBox1.Text);
            cm.Parameters.AddWithValue("@car_id", val4);
            cm.CommandType = CommandType.Text;
            cm.ExecuteNonQuery();
            conn.Close();
            Form4_Activated(sender, e);
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            //Stores Inventory item id
            foreach (DataGridViewRow row in dataGridView2.SelectedRows)
            {
                val4 = row.Cells[0].Value.ToString();
            }
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            //To calculate pay due
            try
            {
                if (textBox10.Text.Equals(""))
                    pay = 0;
                else
                    pay = Int64.Parse(textBox10.Text);
                x = Int64.Parse(textBox9.Text);
                if (pay > x)
                {
                    MessageBox.Show("Please enter a valid amount");
                    textBox10.Text = "";
                }
            }
            catch (Exception ex){
                MessageBox.Show("Invalid input");
                textBox10.Text = "";
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox11.Enabled = true;
                comboBox2.Enabled = true;
                textBox10.Text = "";
                textBox10.Enabled = false;
                button6.Enabled = false;
                button5.Enabled = true;
                textBox11.Text = textBox9.Text;
                connect();
                MySqlCommand cm = new MySqlCommand();
                cm.Connection = conn;
                cm.CommandText = "SELECT username from sales_person";
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    if(!comboBox2.Items.Contains(dr["username"].ToString()))
                        comboBox2.Items.Add(dr["username"].ToString());
                }
                dr.Close();
                conn.Close();
            }
            else
            {
                textBox11.Text = "";
                comboBox2.Text = "";
                textBox11.Enabled = false;
                comboBox2.Enabled = false;
                textBox10.Enabled = true;
                button5.Enabled = false;
                button6.Enabled = true;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //If the user gets the loan,he will pay the car company
            //And takes his items from the inventory
            if (textBox10.Text.Equals(textBox9.Text))
            {
                connect();
                MySqlCommand cm4 = new MySqlCommand();
                cm4.Connection = conn;
                cm4.CommandText = "delete from inventory where username = @username ";
                cm4.Parameters.AddWithValue("@username", textBox1.Text);
                cm4.CommandType = CommandType.Text;
                cm4.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Payment Done");
                textBox10.Text = "";
                Form4_Activated(sender, e);
            }
            else
            {
                MessageBox.Show("Payment is incomplete");
                textBox10.Text = "";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //To check whether the user can take loan or not based on the due payment
            if (textBox11.Text.Equals("0"))
            {
                MessageBox.Show("There is no due payment for you to take loan");
            }
            else if (comboBox2.Text.Equals(""))
            {
                MessageBox.Show("select a salesperson");
            } else
            {
                try
                {
                    connect();
                    MySqlCommand cm = new MySqlCommand();
                    cm.Connection = conn;
                    cm.CommandText = "INSERT INTO loan(c_username,s_username,b_username,loan_amt) values(@c_username,@s_username,null,@loan_amt)";
                    cm.Parameters.AddWithValue("@c_username", textBox1.Text);
                    cm.Parameters.AddWithValue("@s_username", comboBox2.Text);
                    cm.Parameters.AddWithValue("@loan_amt", textBox11.Text);
                    cm.CommandType = CommandType.Text;
                    cm.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Your Loan Request has been recorded");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Your Loan request has already been recorded");
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //If the user repays the loan to the banker,he is removed from the loan table 
            if (textBox12.Text.Equals("0"))
            {
                MessageBox.Show("There is no loan due to return");
            }
            else
            {
                
                MessageBox.Show("Loan has been returned");
                connect();
                MySqlCommand cm = new MySqlCommand();
                cm.Connection = conn;
                cm.CommandText = "delete from loan where c_username = @c_username ";
                cm.Parameters.AddWithValue("@c_username", textBox1.Text);
                cm.CommandType = CommandType.Text;
                cm.ExecuteNonQuery();
                textBox12.Text = "0";
                conn.Close();
            }
        }
    }
}
