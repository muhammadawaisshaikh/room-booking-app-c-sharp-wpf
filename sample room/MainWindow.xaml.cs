using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace sample_room
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        
        


        public MainWindow()
        {
    
            InitializeComponent();

            

             DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();

            // for Reporting combo-box
            rcombo.Items.Add("Search Reports ");
            rcombo.Items.Add("Total Revenue");
            rcombo.Items.Add("Daily Report");
            rcombo.Items.Add("Weekly Report");
            rcombo.Items.Add("Monthly Report");
            rcombo.Items.Add("Yearly Report");
            rcombo.SelectedIndex = 0;

            // for gender combo-box
            t2.Items.Add("Male");
            t2.Items.Add("Female");
            t2.SelectedIndex = 0;

            // for room type combo-box
            t9.Items.Add("Single Bed Room");
            t9.Items.Add("Double Bed Room");
            t9.Items.Add("Standard Bed Room");
            t9.Items.Add("Premium Bed Room");
            t9.SelectedIndex = 0;

            // for amount status combo-box
            t12.Items.Add("Amount Paid");
            t12.Items.Add("Amount UnPaid");
            t12.SelectedIndex = 0;

         

        }

        void timer_Tick(object sender, EventArgs e)
        {
            timelabel.Content = DateTime.Now.ToLongTimeString();
        }
        private void _new_Click(object sender, RoutedEventArgs e)
        {
            t1.Text = t2.Text = t3.Text = t4.Text = t5.Text = t6.Text = t7.Text = t8.Text  = t10.Text = t11.Text  = "";
        }

        private void submit_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                SqlConnection conn = new SqlConnection("Data Source=AWAIS;Initial Catalog=guesthouse;Integrated Security=True");
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO customer (name,gender,address,mobile,nic,date,stay,rno,rtype,amountday,totalamount,status) VALUES('" + t1.Text + "','" + t2.SelectedValue + "','" + t3.Text + "','" + t4.Text + "','" + t5.Text + "','" + t6.DisplayDate + "','" + t7.Text + "','" + t8.Text + "','" + t9.SelectedValue + "','" + t10.Text + "','" + t11.Text + "','" + t12.SelectedValue + "')", conn);
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("New Customer Record Added Successfully");

                slip s = new slip();
                s.Show();

            }

            catch 
            {
                MessageBox.Show(" Enter the Complete information of customer to continue further");
            }
            
            

        }

        private void t4_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (!t4.Text.All(c => Char.IsNumber(c)))
            {
                MessageBox.Show("Enter a valid mobile number to continue further");
            }
            

        }

        private void t5_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!t5.Text.All(c => Char.IsNumber(c)))
            {
                MessageBox.Show("Enter a valid nic# number to continue further");
            }
        }

        private void t7_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!t7.Text.All(c => Char.IsNumber(c)))
            {
                MessageBox.Show("Enter a valid numeric value for days to continue further");
            }
        }

        private void t8_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!t8.Text.All(c => Char.IsNumber(c)))
            {
                MessageBox.Show("Enter a valid numeric value for room no# to continue further");
            }
        }

        private void t10_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!t10.Text.All(c => Char.IsNumber(c)))
            {
                MessageBox.Show("Enter a valid numeric value for room's amount per-day to continue further");
            }
        }

        private void t11_TextChanged(object sender, TextChangedEventArgs e)
        {
           

                
            
        }

        private void count_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Int32 val1 = Convert.ToInt32(t7.Text);
                Int32 val2 = Convert.ToInt32(t10.Text);
                Int32 val3 = val1 * val2;
                t11.Text = val3.ToString();
            }
            catch
            {
                MessageBox.Show("Enter Amount per-day and Staying days of Customer  to continue further !");
            }
        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = "Data Source=AWAIS;Initial Catalog=guesthouse;Integrated Security=True";

                DataSet ds = new DataSet();

                SqlDataAdapter adapt = new SqlDataAdapter("Select * from customer Where mobile = " + this.t4.Text, conn);

                adapt.Fill(ds, "customer");

                t1.Text = ds.Tables["customer"].Rows[0]["name"].ToString();
                t2.Text = ds.Tables["customer"].Rows[0]["gender"].ToString();
                t3.Text = ds.Tables["customer"].Rows[0]["address"].ToString();
                t4.Text = ds.Tables["customer"].Rows[0]["mobile"].ToString();
                t5.Text = ds.Tables["customer"].Rows[0]["nic"].ToString();
                t6.Text = ds.Tables["customer"].Rows[0]["date"].ToString();
                t7.Text = ds.Tables["customer"].Rows[0]["stay"].ToString();
                t8.Text = ds.Tables["customer"].Rows[0]["rno"].ToString();
                t9.Text = ds.Tables["customer"].Rows[0]["rtype"].ToString();
                t10.Text = ds.Tables["customer"].Rows[0]["amountday"].ToString();
                t11.Text = ds.Tables["customer"].Rows[0]["totalamount"].ToString();
                t12.Text = ds.Tables["customer"].Rows[0]["status"].ToString();

            }

            catch
            {
                MessageBox.Show(" Search the Customer Details by Mobile No# ");
            }
        }

        

        private void vanish_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                SqlConnection conn = new SqlConnection("Data Source=AWAIS;Initial Catalog=guesthouse;Integrated Security=True");
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"delete from customer WHERE(mobile='" + t4.Text + "')", conn);
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Customer Profile Vanished Successfully");
            }

            catch
            {
                MessageBox.Show("Search the Customer Firstly with his/her Mobile Number !");
            }
        }

        private void rcombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (rcombo.SelectedIndex == 1)
            {
                report r = new report();
                r.Show();
            }
        }

        private void mainframe_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void slip_Click(object sender, RoutedEventArgs e)
        {
             
        }

        private void slipb_Click(object sender, RoutedEventArgs e)
        {
            slip s = new slip();
            s.Show();
        }

        
    }
}
