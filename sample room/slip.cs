using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sample_room
{
    public partial class slip : Form
    {
        public slip()
        {
            InitializeComponent();

           


            

        }

        private void slip_Load(object sender, EventArgs e)
        {

            
        }

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern long BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);
        private Bitmap memoryImage;  

        private void PrintScreen()
        { 
        
            
            Graphics mygraphics = this.CreateGraphics();  
            Size s = this.Size;  
            memoryImage = new Bitmap(s.Width, s.Height, mygraphics);  
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);  
            IntPtr dc1 = mygraphics.GetHdc();  
            IntPtr dc2 = memoryGraphics.GetHdc();  
            BitBlt(dc2, 0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height, dc1, 0, 0, 13369376);  
            mygraphics.ReleaseHdc(dc1);  
            memoryGraphics.ReleaseHdc(dc2);  
       
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(memoryImage, 0, 0);  
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PrintScreen();
            printPreviewDialog1.ShowDialog();  
        }

        private void search_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = "Data Source=AWAIS;Initial Catalog=guesthouse;Integrated Security=True";

                DataSet ds = new DataSet();

                SqlDataAdapter adapt = new SqlDataAdapter("Select * from customer Where mobile = " + this.textBox3.Text, conn);

                adapt.Fill(ds, "customer");

                textBox1.Text = ds.Tables["customer"].Rows[0]["name"].ToString();
                textBox2.Text = ds.Tables["customer"].Rows[0]["address"].ToString();
                textBox3.Text = ds.Tables["customer"].Rows[0]["mobile"].ToString();
                textBox4.Text = ds.Tables["customer"].Rows[0]["date"].ToString();
                textBox5.Text = ds.Tables["customer"].Rows[0]["stay"].ToString();
                textBox6.Text = ds.Tables["customer"].Rows[0]["rno"].ToString();
                textBox7.Text = ds.Tables["customer"].Rows[0]["rtype"].ToString();
                textBox8.Text = ds.Tables["customer"].Rows[0]["amountday"].ToString();
                textBox9.Text = ds.Tables["customer"].Rows[0]["totalamount"].ToString();
                textBox10.Text = ds.Tables["customer"].Rows[0]["status"].ToString();
            }

            catch
            {
                MessageBox.Show("Enter Valid Mobile# to Search Customer !");
            }
        }

        

        
    }
}
