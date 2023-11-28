using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace DatabasePRO
{
    public partial class Form1 : Form
    {
        DataBase conn;
        string name_dir = "Direct_table";
        string name_pref = "pref_table";
        string name_rest = "rest_table";
        int []arr_id;
        string []arr_name;
        public Form1()
        {
            InitializeComponent();
            date_text_name = DateTime.Now.ToString();
            conn = new DataBase(@"DESKTOP-NBDK7UV", "DB");
            conn.openConnection();
            C.conn = conn;
            //
            Show_direct_table(name_dir);
            Show_pref_table(name_pref);
            Show_rest_table();
            update_Box();
            
          
        }
        int select_list = 0;
        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text.Trim() != "" && textBox1.Text.Length < 50)
            {
                string Quer_add = $"insert into {name_dir}(Direct)values('{textBox1.Text}')";
                using (SqlCommand command = new SqlCommand(Quer_add, C.conn.GetConnection()))
                {
                    command?.ExecuteNonQuery();
                    textBox1.Text = "";
                }
            }
            else if (textBox1.Text.Length > 50)
                MessageBox.Show("textBox > 50");
            else
            {
                MessageBox.Show("textbox == null");
            }
            Show_direct_table(name_dir);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count > 0 && textBox1.Text.Trim() != "" && textBox1.Text.Length < 50)
            {
              
                
                string Quer_delet = $"update DB.dbo.{name_dir} Set Direct = '{textBox1.Text}' Where id = {arr_id[select_list]}";
                using (SqlCommand command = new SqlCommand(Quer_delet, C.conn.GetConnection()))
                {
                    command.ExecuteNonQuery();
                }
                textBox1.Text = "";
                select_list = 0;
                Show_direct_table(name_dir);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Show_direct_table(name_dir);
            if (listBox1.SelectedIndex < 0 ) 
            {
                select_list = listBox1.Items.Count - 1;
            }
            if (listBox1.Items.Count>0) {
                
                
                string Quer_delet = $"DELETE FROM {name_dir} WHERE id = {arr_id[select_list]}"; ;
                using (SqlCommand command = new SqlCommand(Quer_delet, C.conn.GetConnection()))
                {
                    command?.ExecuteNonQuery();
                }
                select_list = 0;
                Show_direct_table(name_dir);
            }
        }
        
        private void Show_direct_table(string name_table) 
        {
            listBox1.Items.Clear();
            string Quer = $"select * from {name_table}";
            int index_size = 0;
            using (SqlCommand command = new SqlCommand(Quer, C.conn.GetConnection())) 
            {
               
                using (SqlDataReader read = command.ExecuteReader())
                {
                   
                    while (read.Read())
                    {
                        index_size++;
                    }
                    arr_name = new string[index_size];
                    arr_id = new int[index_size];
                    
                }
                using (SqlDataReader read = command.ExecuteReader())
                {
                    int i = 0;
                    while (read.Read()) 
                    {
                        arr_name[i] = read.GetString(1);
                        arr_id[i] = read.GetInt32(0);
                        i++;
                    }
                }
            }
            for (int i=0;i<index_size;i++) 
            {
                listBox1.Items.Add($"id = {arr_id[i]} direct = {arr_name[i]}");
            }
            index_size = 0;
        }
        //
        int[] arr_pref_id;
        string[,] info_pref;
        int id_f_name = 1;
        int id_i_name = 2;
        int id_o_name = 3;
        int id_number_name = 4;
        int id_pot_name = 5;
        private void Show_pref_table(string name_table)
        {
            listBox2.Items.Clear();
            string Quer = $"select * from {name_table}";
            int index_size = 0;
            using (SqlCommand command = new SqlCommand(Quer, C.conn.GetConnection()))
            {

                using (SqlDataReader read = command.ExecuteReader())
                {

                    while (read.Read())
                    {
                        index_size++;
                    }
                    arr_pref_id = new int[index_size];
                    info_pref = new string[read.FieldCount, index_size];
                    

                }
                using (SqlDataReader read = command.ExecuteReader())
                {
                    int i = 0;
                    while (read.Read())
                    {

                        info_pref[id_f_name-1,i] = read.GetString(id_f_name).Trim();
                        info_pref[id_i_name-1, i] = read.GetString(id_i_name).Trim();
                        info_pref[id_o_name-1, i] = read.GetString(id_o_name).Trim();
                        info_pref[id_number_name-1, i] = read.GetString(id_number_name).Trim();
                        info_pref[id_pot_name - 1, i] = read.GetString(id_pot_name).Trim();
                        //id get
                        arr_pref_id[i] = read.GetInt32(0);
                        i++;
                    }
                }
            }
            for (int i = 0; i < index_size; i++)
            {
                listBox2.Items.Add($" [id] = {arr_pref_id[i]} [f_name] = {info_pref[id_f_name-1,i]} [i_name] = {info_pref[id_i_name-1, i]} [o_name] = {info_pref[id_o_name-1, i]} [number_name] = {info_pref[id_number_name-1, i]} [pot_name] = {info_pref[id_pot_name-1, i]}");
            }
            index_size = 0;
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            button2.Enabled = checkBox1.Checked;

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                select_list = listBox1.SelectedIndex;
                textBox1.Text = arr_name[select_list].Trim();
            }
            else 
            {
                MessageBox.Show("выберите элемент");
            }
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            update_Box();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            //add
           
                string Quer_add = $"insert into {name_pref}([f_name],[i_name],[o_name],[number_name],[pot_name])values('{f_name.Text.Trim()}','{i_name.Text.Trim()}','{o_name.Text.Trim()}','{number_name.Text.Trim()}','{pot_name.Text.Trim()}')";
                using (SqlCommand command = new SqlCommand(Quer_add, C.conn.GetConnection()))
                {
                    command?.ExecuteNonQuery();
                f_name.Text = "";
                i_name.Text = "";
                o_name.Text = "";
                number_name.Text = "";
                pot_name.Text = "";
                }
           
            Show_pref_table(name_pref);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //updaet
           
            string Quer_add = $"update DB.dbo.{name_pref} SET [f_name] = '{f_name.Text.Trim()}',[i_name] = '{i_name.Text.Trim()}',[o_name] = '{o_name.Text.Trim()}' ,[number_name] = '{number_name.Text.Trim()}',[pot_name] = '{pot_name.Text.Trim()}' Where id = {arr_pref_id[select_list2]}";
            using (SqlCommand command = new SqlCommand(Quer_add, C.conn.GetConnection()))
            {
                command?.ExecuteNonQuery();
                checkBox2.Checked = false;
            }
            Show_pref_table(name_pref);
            f_name.Text = "";
            i_name.Text = "";
            o_name.Text = "";
            number_name.Text = "";
            pot_name.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //delete
            if (listBox2.Items.Count > 0)
            {
                Show_pref_table(name_pref);

                string Quer_delet = $"DELETE FROM {name_pref} WHERE id = {arr_pref_id[select_list2]}"; ;
                using (SqlCommand command = new SqlCommand(Quer_delet, C.conn.GetConnection()))
                {
                    command?.ExecuteNonQuery();
                }
                select_list2 = 0;
                Show_pref_table(name_pref);
                f_name.Text = "";
                i_name.Text = "";
                o_name.Text = "";
                number_name.Text = "";
                pot_name.Text = "";
            }

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

            button5.Enabled = checkBox2.Checked;
            
        }
        int select_list2 = 0;
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex >= 0)
            {
                select_list2 = listBox2.SelectedIndex;
                f_name.Text = info_pref[id_f_name - 1, select_list2];
                i_name.Text = info_pref[id_i_name - 1, select_list2];
                o_name.Text = info_pref[id_o_name - 1, select_list2];
                number_name.Text = info_pref[id_number_name - 1, select_list2];
                pot_name.Text = info_pref[id_pot_name - 1, select_list2];
            }
            else
            {
                MessageBox.Show("выберите элемент");
            }
        }

        private void f_name_TextChanged(object sender, EventArgs e)
        {

        }
        void Show_rest_table() 
        {
            listBox3.Items.Clear();
            int index_info_rest = 0;
            string Quer_select = $"select * from {name_rest}";
            using (SqlCommand command = new SqlCommand(Quer_select, C.conn.GetConnection())) 
            {
                using (SqlDataReader read = command.ExecuteReader()) {
                    
                    while (read.Read())
                    {
                        index_info_rest++;
                    }
                    info_rest = new object[read.FieldCount, index_info_rest];
                }
               
                using (SqlDataReader read = command.ExecuteReader()) 
                {
                    int i = 0;
                    while (read.Read()) 
                    {
                        info_rest[id_index_rest, i] = read.GetInt32(0);
                        info_rest[id_type_box_rest, i] = read.GetString(1).Trim();
                        info_rest[id_pref_box_rest, i] = read.GetString(2).Trim();
                        info_rest[id_groupe_box_rest, i] = read.GetString(3).Trim();
                        info_rest[id_date_name_rest, i] = read.GetString(4).Trim();
                        info_rest[id_time_name_rest, i] = read.GetString(5).Trim();
                        info_rest[id_audio_box_rest, i] = read.GetString(6).Trim();
                        info_rest[id_direct_box_rest, i] = read.GetString(7).Trim();
                        i++;
                    }
                }
                for (int i = 0; i < index_info_rest; i++)  
                {
                    listBox3.Items.Add($"id = {info_rest[id_index_rest, i]} type_box = {info_rest[id_type_box_rest, i]} pref_box = {info_rest[id_pref_box_rest, i]} groupe_box = {info_rest[id_groupe_box_rest, i]} date = {info_rest[id_date_name_rest, i]} time = {info_rest[id_time_name_rest, i]} audio = {info_rest[id_audio_box_rest, i]} direct = {info_rest[id_direct_box_rest, i]}");
                }
                index_info_rest = 0;
            }
        }
        int select_list3 = 0;
        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox3.SelectedIndex >= 0)
            {
                
                select_list3 = listBox3.SelectedIndex;
                type_box.Text = info_rest[id_type_box_rest, select_list3].ToString();
                pref_box.Text = info_rest[id_pref_box_rest, select_list3].ToString();
                time_text.Text = info_rest[id_time_name_rest, select_list3].ToString();
                groupe_box.Text = info_rest[id_groupe_box_rest, select_list3].ToString();
                audio_box.Text = info_rest[id_audio_box_rest, select_list3].ToString();
                direct_box.Text = info_rest[id_direct_box_rest, select_list3].ToString();
                date_name.Value = DateTime.Parse(info_rest[id_date_name_rest,select_list3].ToString());

            }
        }
        int id_index_rest = 0;
        int id_type_box_rest = 1;
        int id_pref_box_rest = 2;
        int id_groupe_box_rest = 3;
        int id_date_name_rest = 4;
        int id_time_name_rest = 5;
        int id_audio_box_rest = 6;
        int id_direct_box_rest = 7;
        object[,] info_rest;
        
        private void button9_Click(object sender, EventArgs e)
        {
            //add 3
            string Quer_add = $"insert into {name_rest}([type_box],[pref_box],[groupe_box],[date_name],[time_text],[audio_box],[direct_box])values('{type_box_name}','{pref_box_name}','{groupe_box_name}','{date_text_name}','{time_text_name}','{audio_box_name}','{direct_box_name}')";
            using (SqlCommand command = new SqlCommand(Quer_add, C.conn.GetConnection()))
            {
                command?.ExecuteNonQuery();
                null_text_rest();
            }
            Show_rest_table();

        }
        void null_text_rest() 
        {
            type_box.Text = "";
            pref_box.Text = "";
            direct_box.Text = "";
            groupe_box.Text = "";
            audio_box.Text = "";
            time_text.Text = "";
        }
        private void button8_Click(object sender, EventArgs e)
        {
            //rename 3
            string Quer_add = $"update  {name_rest} set type_box = '{type_box_name}', pref_box = '{pref_box_name}', groupe_box = '{groupe_box_name}', date_name =  '{date_text_name}',time_text = '{time_text_name}', audio_box = '{audio_box_name}', direct_box ='{direct_box_name}' where id = {info_rest[id_index_rest,select_list3]}";
            using (SqlCommand command = new SqlCommand(Quer_add, C.conn.GetConnection()))
            {
                command?.ExecuteNonQuery();
                null_text_rest();
                checkBox3.Checked = false;
            }
            Show_rest_table();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //delete
            //3
            Show_rest_table();
            if (listBox3.SelectedIndex < 0)
            {
                select_list3 = listBox3.Items.Count - 1;
            }
            string Quer_delet = $"DELETE FROM {name_rest} WHERE id = {info_rest[id_index_rest, select_list3]}";
            using (SqlCommand command = new SqlCommand(Quer_delet, C.conn.GetConnection()))
            {
                command?.ExecuteNonQuery();
            }
            
            Show_rest_table();
            null_text_rest();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            button8.Enabled = checkBox3.Checked;
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }
        void update_Box() 
        {
            pref_box.Items.Clear();
            direct_box.Items.Clear();
            Show_direct_table(name_dir);
            Show_pref_table(name_pref);
            direct_box.Items.AddRange(arr_name);
            for (int i = 0; i < info_pref.GetLength(1); i++)
            {
                pref_box.Items.Add(info_pref[id_i_name - 1, i]);
            }
        }
        //
        string type_box_name, pref_box_name, groupe_box_name, date_text_name, time_text_name, audio_box_name, direct_box_name;
        
        private void groupe_box_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (groupe_box.SelectedIndex >= 0)
                groupe_box_name = groupe_box.Items[groupe_box.SelectedIndex].ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
         
        }

        private void pref_box_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pref_box.SelectedIndex >= 0)
                pref_box_name = pref_box.Items[pref_box.SelectedIndex].ToString();
        }
       
        private void type_box_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (type_box.SelectedIndex >= 0)
                type_box_name = type_box.Items[type_box.SelectedIndex].ToString();
        }

        private void date_name_ValueChanged(object sender, EventArgs e)
        {
            
                date_text_name = date_name.Value.ToString();
        }

        private void time_text_TextChanged(object sender, EventArgs e)
        {
            time_text.Text=time_text.Text.Trim();
            if (time_text.Text.Length > 0)
                time_text_name = time_text.Text;
        }

        private void audio_box_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (audio_box.SelectedIndex >= 0)
                audio_box_name = audio_box.Items[audio_box.SelectedIndex].ToString();
        }

        private void direct_box_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (direct_box.SelectedIndex >= 0)
                direct_box_name = direct_box.Items[direct_box.SelectedIndex].ToString();
        }
    }
}
