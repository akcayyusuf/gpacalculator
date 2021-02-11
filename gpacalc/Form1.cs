using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gpacalc
{
    public partial class Form1 : Form
    {
        string[] grades = { "AA", "BA", "BB", "CB", "CC", "DC", "DD", "FD", "FF" };
        float[] coeff = {4,3.5f,3,2.5f,2,1.5f,1,0.5f,0 };
        List<DataGridView> elements = new List<DataGridView>();
        List<Label> final = new List<Label>();
        List<Label> final2 = new List<Label>();


        public Form1()
        {
            
            InitializeComponent();
      

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

     

        public void button1_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
          
            panel2.HorizontalScroll.Enabled = true;
            panel2.HorizontalScroll.Visible = true;
            panel2.HorizontalScroll.Maximum = 0;
            panel2.AutoScroll = true;
            
            int[] tab = { 2, 3, 4, 5, 6, 7, 9, 8 };
            NumericUpDown[] ects = new NumericUpDown[] {numericUpDown2,numericUpDown3,numericUpDown4,numericUpDown5,numericUpDown6,numericUpDown7,numericUpDown9,numericUpDown8 };
            int x=50;
            int y=10;
            elements.Clear();

            for (int i = 0; i<8; i++) 
            {
                if (i % 2 == 0) 
                {
                    Label info = new Label();
                    info.Width = 1300;
                    info.Height = 25;
                    info.TextAlign=ContentAlignment.MiddleCenter;
                    info.BackColor = Color.Red;
                    info.Text = "Year " + ((i/2+1)).ToString();
                    info.Font = new Font("Arial",10 ,FontStyle.Bold);
                    info.ForeColor = Color.White;
                    info.Location = new Point(50, y);
                    panel2.Controls.Add(info);
                    y += 30;
                }

                if (ects[i].Value != 0)
                {

                    DataGridView dgv = new DataGridView();
                    dgv.CellContentClick += Dgv_CellValueChanged;
                    dgv.Width = 500;
                    dgv.ColumnCount = 2;
                    dgv.AllowUserToAddRows = false;
                    dgv.Columns[0].Name = "Lecture Name";
                    dgv.Columns[0].Width = 125;
                    dgv.Columns[1].Name = "Credit";
                    dgv.Columns[1].Width = 50;
                    dgv.BorderStyle = BorderStyle.None;
                    dgv.BackgroundColor = Color.FromKnownColor(KnownColor.Control);
                    


                    for (int j = 0; j < 9; j++)
                    {
                        DataGridViewCheckBoxColumn check = new DataGridViewCheckBoxColumn();
                        check.HeaderText = grades[j];
                        check.Width = 30;
                        dgv.Columns.Add(check);
                    }

                    dgv.RowCount = (int)ects[i].Value;
                    dgv.MaximumSize =new Size(700, 23 + 24 * 9);
                    dgv.Height = 23 + 24 * (int)ects[i].Value;
                    dgv.Location = new Point(x, y);
                    elements.Add(dgv);
                    panel2.Controls.Add(dgv);
                }
                    if (x == 700)
                    {
                        x = 50;

                        y += 250;

                    }
                    else
                    {
                        x += 650;
                    }

                
               
               
            }


        }

        private void Dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
           

            var dataGridView = (DataGridView)sender;

            if (e.ColumnIndex >1 ) 
            {
                dataGridView.EndEdit();

                bool isChecked = (bool)dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                if (isChecked)
                {
                    for(int i = 2; i < dataGridView.ColumnCount; i++) 
                    {
                        dataGridView.Rows[e.RowIndex].Cells[i].Value = false;
                        
                    }
                    dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = true;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < final.Count; i++)
            {
               
               
                    panel2.Controls.Remove(final[i]);
                    panel2.Controls.Remove(final2[i]);
               
            }





            float []total= { 0, 0, 0, 0, 0, 0, 0, 0 };
            final.Clear();
            final2.Clear();

            float agnototcredit=0;
            float agnocmc = 0;
            float agno = 0;
            float[] terms = new float[8];
            float[] totalcredit = { 0, 0, 0, 0, 0, 0, 0, 0 };
           
           




            for (int n = 0; n < elements.Count; n++)
            {
                Label lbl = new Label();
                lbl.Tag = "info";
                final.Add(lbl);

                Label lbl2 = new Label();
                lbl2.Tag = "info";
                final2.Add(lbl2);

               


                total[n] = 0;
                agnototcredit = 0;
                agnocmc = 0;

                for(int i = 0; i < elements[n].Rows.Count; i++) 
                {
                    
                   totalcredit[n] +=Convert.ToSingle( elements[n].Rows[i].Cells[1].Value);
                    for (int j = 2; j < 11; j++) 
                    {
                        if ((bool)elements[n].Rows[i].Cells[j].Value == true) 
                        {
                            total[n] =total[n] + Convert.ToSingle( elements[n].Rows[i].Cells[1].Value) * coeff[j - 2];
                        }
                    }
                }
                terms[n] = (float)(total[n] / totalcredit[n]);

                
                final[n].Text ="GPAS: " + terms[n].ToString();
                final[n].Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                final[n].Width = 150;
                final[n].Height = 34;
                final[n].Location = new Point(elements[n].Location.X+510, elements[n].Location.Y);
                
                panel2.Controls.Add(final[n]);
                
                for(int var = 0; var <= n; var++) 
                {
                    agnototcredit += totalcredit[var];
                    agnocmc += total[var];
                }
               
                agno = (float)(agnocmc / agnototcredit);

                final2[n].Text = "GPA: " + agno.ToString();
                final2[n].Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                final2[n].Width = 150;
                final2[n].Height = 34;
                final2[n].Location = new Point(elements[n].Location.X + 510, elements[n].Location.Y+40);

                panel2.Controls.Add(final2[n]);

                label10.Text = final2[n].Text;

            }


       
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "(*.tugaykerimoglu)|*.tugaykerimoglu";
            save.ShowDialog();


            int[] year = new int[8] { Convert.ToInt32( numericUpDown2.Value),Convert.ToInt32(numericUpDown3.Value) , Convert.ToInt32(numericUpDown4.Value), Convert.ToInt32(numericUpDown5.Value) , Convert.ToInt32(numericUpDown6.Value), Convert.ToInt32(numericUpDown7.Value) , Convert.ToInt32(numericUpDown9.Value), Convert.ToInt32(numericUpDown8.Value) };
            string[][,] lecture = new string[elements.Count][,];

            for(int n = 0; n < elements.Count; n++) 
            {
                lecture[n] = new string[elements[n].Rows.Count, 3];
                for(int i = 0; i < elements[n].Rows.Count; i++) 
                {
                    for(int j = 0; j < 3; j++) 
                    {
                        
                        if (j<2)
                        {
                            lecture[n][i, j] = elements[n].Rows[i].Cells[j].Value.ToString();
                        }
                        else 
                        {
                            int k = 2;

                            while (elements[n].Rows[i].Cells[k].Value.ToString() != "True") 
                            {
                               
                                k++;
                            }
                            lecture[n][i, 2] = k.ToString();
                            break;
                        }
                    }
                }
            }
            using (StreamWriter writer = new StreamWriter(save.FileName))
            {

                for (int i = 0; i < 8; i++)
                {
                    writer.Write(year[i]+",");
                }
                writer.WriteLine();
                for (int n = 0; n < elements.Count; n++)
                {
                    for (int i = 0; i < elements[n].RowCount; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            writer.Write(lecture[n][i, j]+",");
                        }
                    }
                    writer.WriteLine();
                   

                }

            }

       



        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog load = new OpenFileDialog();
            load.Filter = "(*.tugaykerimoglu)|*.tugaykerimoglu";
            load.ShowDialog();

            string[] lines = File.ReadAllLines(load.FileName);
            string[] num_lec = lines[0].Split(',');

            

            numericUpDown2.Value = Convert.ToInt32(num_lec[0]);
            numericUpDown3.Value = Convert.ToInt32(num_lec[1]);
            numericUpDown4.Value = Convert.ToInt32(num_lec[2]);
            numericUpDown5.Value = Convert.ToInt32(num_lec[3]);
            numericUpDown6.Value = Convert.ToInt32(num_lec[4]);
            numericUpDown7.Value = Convert.ToInt32(num_lec[5]);
            numericUpDown9.Value = Convert.ToInt32(num_lec[6]);
            numericUpDown8.Value = Convert.ToInt32(num_lec[7]);

            button1_Click(sender, e);

            for (int n = 0; n < lines.Length-1; n++) 
            {
                num_lec = lines[n+1].Split(',');
                
                for(int i = 0; i < elements[n].Rows.Count; i++) 
                {
                    for(int j = 0; j < 2; j++) 
                    {
                        elements[n].Rows[i].Cells[j].Value = num_lec[j+(i*3)].ToString();
                    }
                    for (int q = 2; q < 11; q++)
                    {
                        elements[n].Rows[i].Cells[q].Value = false;
                    }


                    elements[n].Rows[i].Cells[Convert.ToInt32(num_lec[(3 * i) + 2])].Value = true;
                }



            }
            
            
            
            

            


        }

        private void button5_Click(object sender, EventArgs e)
        {
            string about = "Powered  by Yusuf Akçay";
            string title = "About";
            MessageBox.Show(about, title);
        }
    }
}
