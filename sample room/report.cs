using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sample_room
{
    public partial class report : Form
    {
        public report()
        {
            InitializeComponent();
        }

        private void report_Load(object sender, EventArgs e )
        {
            // TODO: This line of code loads data into the 'guesthouseDataSet.customer' table. You can move, or remove it, as needed.
            this.customerTableAdapter.Fill(this.guesthouseDataSet.customer);

            this.reportViewer1.RefreshReport();


        }
    }
}
