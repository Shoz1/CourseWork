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
using System.Data.OleDb;

namespace CourseWork
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Курсовая\CourseWork\CourseWork\DatabaseCourse.mdf;Integrated Security=True");

        SqlDataAdapter adapter = new SqlDataAdapter();

        public string reload = "SELECT * FROM [Students]";

        public Form1()
        {
            InitializeComponent();

            LoadData(reload);

            Form2 PF = new Form2();
            if (PF.ShowDialog() == DialogResult.Cancel)
                Application.Exit();

                        
        }
        
        private async void LoadData(string reload)
        {           

            await sqlConnection.OpenAsync();

            string reloading = reload;

            SqlCommand command = new SqlCommand(reloading, sqlConnection);
            

            SqlDataReader reader = await command.ExecuteReaderAsync();


            List<string[]> data = new List<string[]>();

            while (reader.Read())
            {
                data.Add(new string[16]);

                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
                data[data.Count - 1][3] = reader[3].ToString();
                data[data.Count - 1][4] = reader[4].ToString();
                data[data.Count - 1][5] = reader[5].ToString();
                data[data.Count - 1][6] = reader[6].ToString();
                data[data.Count - 1][7] = reader[7].ToString();
                data[data.Count - 1][8] = reader[8].ToString();
                data[data.Count - 1][9] = reader[9].ToString();
                data[data.Count - 1][10] = reader[10].ToString();
                data[data.Count - 1][11] = reader[11].ToString();
                data[data.Count - 1][12] = reader[12].ToString();
                data[data.Count - 1][13] = reader[13].ToString();
                data[data.Count - 1][14] = reader[14].ToString();
                data[data.Count - 1][15] = reader[15].ToString();
                
                

            }

            reader.Close();

            sqlConnection.Close();

            foreach (string[] s in data)
            {
                dataGridView1.Rows.Add(s);
                dataGridView2.Rows.Add(s);
                dataGridView3.Rows.Add(s);
            }
        }
       
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
            {
                sqlConnection.Close();
            }
        }

        private void botton_insert_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();

            if (labelInsertError.Visible)
            {
                labelInsertError.Visible = false;
            }

            if (!string.IsNullOrEmpty(textBoxName.Text) && !string.IsNullOrWhiteSpace(textBoxName.Text) &&
                !string.IsNullOrEmpty(textBoxSecondName.Text) && !string.IsNullOrWhiteSpace(textBoxSecondName.Text) &&
                !string.IsNullOrEmpty(textBoxThridName.Text) && !string.IsNullOrWhiteSpace(textBoxThridName.Text) &&
                !string.IsNullOrEmpty(textBoxRecordBook.Text) && !string.IsNullOrWhiteSpace(textBoxRecordBook.Text) &&
                !string.IsNullOrEmpty(textBoxFaculty.Text) && !string.IsNullOrWhiteSpace(textBoxFaculty.Text) &&
                !string.IsNullOrEmpty(textBoxCourse.Text) && !string.IsNullOrWhiteSpace(textBoxCourse.Text))
            {
                
                SqlCommand command = new SqlCommand("INSERT INTO [Students] (Name, SName,TName, RecordBookNumber, Faculty, Course,SubjectName1,TheacherSurName1,Mark1,SubjectName2,TheacherSurName2,Mark2,SubjectName3,TheacherSurName3,Mark3)" +
                    "VALUES(@Name, @SName, @TName, @RecordBookNumber, @Faculty, @Course ,@SubjectName1,@TheacherSurName1,@Mark1,@SubjectName2,@TheacherSurName2,@Mark2,@SubjectName3,@TheacherSurName3,@Mark3)", sqlConnection);


                command.Parameters.AddWithValue("SubjectName1", textBoxSubject1.Text);
                command.Parameters.AddWithValue("TheacherSurName1", textBoxTeacherSurname1.Text);
                command.Parameters.AddWithValue("Mark1", textBoxMark1.Text);
                command.Parameters.AddWithValue("SubjectName2", textBoxSubject2.Text);
                command.Parameters.AddWithValue("TheacherSurName2", textBoxTeacherSurname2.Text);
                command.Parameters.AddWithValue("Mark2", textBoxMark2.Text);
                command.Parameters.AddWithValue("SubjectName3", textBoxSubject3.Text);
                command.Parameters.AddWithValue("TheacherSurName3", textBoxTeacherSurname3.Text);
                command.Parameters.AddWithValue("Mark3", textBoxMark3.Text);
                command.Parameters.AddWithValue("Name", textBoxName.Text);
                command.Parameters.AddWithValue("SName", textBoxSecondName.Text);
                command.Parameters.AddWithValue("TName", textBoxThridName.Text);
                command.Parameters.AddWithValue("RecordBookNumber", textBoxRecordBook.Text);
                command.Parameters.AddWithValue("Faculty", textBoxFaculty.Text);
                command.Parameters.AddWithValue("Course", textBoxCourse.Text);                
                try
                {
                    command.ExecuteNonQuery();
                    sqlConnection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    sqlConnection.Close();
                }
              
            }
            else
            {
                labelInsertError.Visible = true;

                labelInsertError.Text = "Ошибка Вы не правильно заполнили поля";

                sqlConnection.Close();
            }



        }

        
       
        private void button_update_Click(object sender, EventArgs e)
        {

            sqlConnection.Open();

            if (labelUpdError.Visible)
            {
                labelUpdError.Visible = false;
            }

            if (!string.IsNullOrEmpty(textBoxUpdName.Text) && !string.IsNullOrWhiteSpace(textBoxUpdName.Text) &&
                    !string.IsNullOrEmpty(textBoxUpdSecondName.Text) && !string.IsNullOrWhiteSpace(textBoxUpdSecondName.Text) &&
                    !string.IsNullOrEmpty(textBoxUpdThridName.Text) && !string.IsNullOrWhiteSpace(textBoxUpdThridName.Text) &&
                    !string.IsNullOrEmpty(textBoxUpdRecordBook.Text) && !string.IsNullOrWhiteSpace(textBoxUpdRecordBook.Text) &&
                    !string.IsNullOrEmpty(textBoxUpdFaculty.Text) && !string.IsNullOrWhiteSpace(textBoxUpdFaculty.Text) &&
                    !string.IsNullOrEmpty(textBoxUpdCourse.Text) && !string.IsNullOrWhiteSpace(textBoxUpdCourse.Text))
            {
                SqlCommand command = new SqlCommand("UPDATE [Students] SET [Name] = @Name,[SName] = @SName,[TName] = @TName,[RecordBookNumber] = @RecordBookNumber,[Faculty] = @Faculty,[Course] = @Course WHERE [Id] = @Id"
                    + "[SubjectName1] = SubjectName1,[TheacherSurName1] = TheacherSurName1,[Mark1] = Mark1 ,[SubjectName2] = SubjectName2,[TheacherSurName2] = TheacherSurName2,[Mark2] = Mark2 ,[SubjectName3] = SubjectName3,[TheacherSurName3] = TheacherSurName3,[Mark3] = Mark3 , ", sqlConnection);


                command.Parameters.AddWithValue("Id", numericUpDownId.Value);

                command.Parameters.AddWithValue("SubjectName1", textBoxUpdSubjectName1.Text);
                command.Parameters.AddWithValue("TheacherSurName1", textBoxUpdTeacherSurname1.Text);
                command.Parameters.AddWithValue("Mark1", textBoxUpdMark1.Text);
                command.Parameters.AddWithValue("SubjectName2", textBoxUpdSubjectName2.Text);
                command.Parameters.AddWithValue("TheacherSurName2", textBoxUpdTeacherSurname2.Text);
                command.Parameters.AddWithValue("Mark2", textBoxUpdMark3.Text);
                command.Parameters.AddWithValue("SubjectName3", textBoxUpdSubjectName3.Text);
                command.Parameters.AddWithValue("TheacherSurName3", textBoxUpdTeacherSurname3.Text);
                command.Parameters.AddWithValue("Mark3", textBoxUpdMark3.Text);
                command.Parameters.AddWithValue("Name", textBoxUpdName.Text);
                command.Parameters.AddWithValue("SName", textBoxUpdSecondName.Text);
                command.Parameters.AddWithValue("TName", textBoxUpdThridName.Text);
                command.Parameters.AddWithValue("RecordBookNumber", textBoxUpdRecordBook.Text);
                command.Parameters.AddWithValue("Faculty", textBoxUpdFaculty.Text);
                command.Parameters.AddWithValue("Course", textBoxUpdCourse.Text);

                try
                {
                    command.ExecuteNonQuery();
                    sqlConnection.Close();
                }
                catch
                {
                    MessageBox.Show("Не удалось обновить запись");
                    sqlConnection.Close();
                }
            }
            else
            {
                labelUpdError.Visible = true;

                labelUpdError.Text = "Ошибка Вы не правильно заполнили поля";
                sqlConnection.Close();
            }


        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            if (labelDelError.Visible)
            {
                labelDelError.Visible = false;
            }


            SqlCommand command = new SqlCommand("DELETE FROM [Students] WHERE [Id] = @Id", sqlConnection);

            command.Parameters.AddWithValue("Id", numericUpDownDelId.Value);


            try
            {
                command.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch
            {
                MessageBox.Show("Не удалось удалить запись");
                sqlConnection.Close();
            }
        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {

            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            dataGridView3.Rows.Clear();

            SqlDataReader sqlReader = null;

            reload = "SELECT * FROM [Students]";

            SqlCommand command = new SqlCommand(reload, sqlConnection);
            try
            {
                LoadData(reload);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }

        }
      
        private void отключитьSqlConnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sqlConnection.Close();
        }

        private void buttonFind_Click(object sender, EventArgs e)
        {
            dataGridView3.Rows.Clear();
            sqlConnection.Open();
            
            string reloading = reload;

            SqlCommand command = new SqlCommand("SELECT * FROM [Students] WHERE (Mark1 LIKE '%' + @search + '%')", sqlConnection);
            command.Parameters.AddWithValue("search", textBoxFind.Text);

            SqlDataReader reader =  command.ExecuteReader();


            List<string[]> data = new List<string[]>();

            while (reader.Read())
            {
                data.Add(new string[16]);

                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
                data[data.Count - 1][3] = reader[3].ToString();
                data[data.Count - 1][4] = reader[4].ToString();
                data[data.Count - 1][5] = reader[5].ToString();
                data[data.Count - 1][6] = reader[6].ToString();
                data[data.Count - 1][7] = reader[7].ToString();
                data[data.Count - 1][8] = reader[8].ToString();
                data[data.Count - 1][9] = reader[9].ToString();
                data[data.Count - 1][10] = reader[10].ToString();
                data[data.Count - 1][11] = reader[11].ToString();
                data[data.Count - 1][12] = reader[12].ToString();
                data[data.Count - 1][13] = reader[13].ToString();
                data[data.Count - 1][14] = reader[14].ToString();
                data[data.Count - 1][15] = reader[15].ToString();



            }

            reader.Close();

            sqlConnection.Close();

            foreach (string[] s in data)
            {                
                dataGridView3.Rows.Add(s);
            }
        }

        private void buttonDel2_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            if (labelDelError.Visible)
            {
                labelDelError.Visible = false;
            }


            SqlCommand command = new SqlCommand("DELETE FROM [Students] WHERE [Mark1] = @Mark1 AND [Mark2] = @Mark2 AND [Mark3] = @Mark3", sqlConnection);


            command.Parameters.AddWithValue("Mark1", textBoxMarks.Text);
            command.Parameters.AddWithValue("Mark2", textBoxMarks.Text);
            command.Parameters.AddWithValue("Mark3", textBoxMarks.Text);


            try
            {
                command.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        
    }
}
