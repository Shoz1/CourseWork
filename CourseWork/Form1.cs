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

namespace CourseWork
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection;

        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Курсовая\CourseWork\CourseWork\DatabaseCourse.mdf;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM [Students]", sqlConnection);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    listBoxDB.Items.Add(Convert.ToString(sqlReader["Id"] + " " + Convert.ToString(sqlReader["Name"]) + " " + Convert.ToString(sqlReader["SName"]) + " "
                        + Convert.ToString(sqlReader["TName"]) + " " + Convert.ToString(sqlReader["RecordBookNumber"]) + " " + Convert.ToString(sqlReader["Faculty"]) + " "
                        + Convert.ToString(sqlReader["Course"])));

                    listBoxDel.Items.Add(Convert.ToString(sqlReader["Id"] + " " + Convert.ToString(sqlReader["Name"]) + " " + Convert.ToString(sqlReader["SName"]) + " "
                        + Convert.ToString(sqlReader["TName"]) + " " + Convert.ToString(sqlReader["RecordBookNumber"]) + " " + Convert.ToString(sqlReader["Faculty"]) + " "
                        + Convert.ToString(sqlReader["Course"])));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                {
                    sqlReader.Close();
                }
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
            {
                sqlConnection.Close();
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
            {
                sqlConnection.Close();
            }
        }

        private async void botton_insert_Click(object sender, EventArgs e)
        {
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
                SqlCommand command = new SqlCommand("INSERT INTO [Students] (Name, SName,TName, RecordBookNumber, Faculty, Course)VALUES(@Name, @SName, @TName, @RecordBookNumber, @Faculty, @Course)", sqlConnection);

                command.Parameters.AddWithValue("Name", textBoxName.Text);
                command.Parameters.AddWithValue("SName", textBoxSecondName.Text);
                command.Parameters.AddWithValue("TName", textBoxThridName.Text);
                command.Parameters.AddWithValue("RecordBookNumber", textBoxRecordBook.Text);
                command.Parameters.AddWithValue("Faculty", textBoxFaculty.Text);
                command.Parameters.AddWithValue("Course", textBoxCourse.Text);

                await command.ExecuteNonQueryAsync();
            }
            else
            {
                labelInsertError.Visible = true;

                labelInsertError.Text = "Ошибка Вы не правильно заполнили поля";
            }


        }

       
        private async void button_update_Click(object sender, EventArgs e)
        {
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
                SqlCommand command = new SqlCommand("UPDATE [Students] SET [Name] = @Name,[SName] = @SName,[TName] = @TName,[RecordBookNumber] = @RecordBookNumber,[Faculty] = @Faculty,[Course] = @Course WHERE [Id] = @Id", sqlConnection);

                command.Parameters.AddWithValue("Id", numericUpDownId.Value);
                command.Parameters.AddWithValue("Name", textBoxUpdName.Text);
                command.Parameters.AddWithValue("SName", textBoxUpdSecondName.Text);
                command.Parameters.AddWithValue("TName", textBoxUpdThridName.Text);
                command.Parameters.AddWithValue("RecordBookNumber", textBoxUpdRecordBook.Text);
                command.Parameters.AddWithValue("Faculty", textBoxUpdFaculty.Text);
                command.Parameters.AddWithValue("Course", textBoxUpdCourse.Text);

                await command.ExecuteNonQueryAsync();
            }
            else
            {
                labelUpdError.Visible = true;

                labelUpdError.Text = "Ошибка Вы не правильно заполнили поля";
            }


        }

        private async void buttonUpdate_Click(object sender, EventArgs e)
        {
            listBoxDB.Items.Clear();
            listBoxDel.Items.Clear();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("Select * From [Students]", sqlConnection);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    listBoxDB.Items.Add(Convert.ToString(sqlReader["Id"] + " " + Convert.ToString(sqlReader["Name"]) + " " + Convert.ToString(sqlReader["SName"]) + " "
                        + Convert.ToString(sqlReader["TName"]) + " " + Convert.ToString(sqlReader["RecordBookNumber"]) + " " + Convert.ToString(sqlReader["Faculty"]) + " "
                        + Convert.ToString(sqlReader["Course"])));

                    listBoxDel.Items.Add(Convert.ToString(sqlReader["Id"] + " " + Convert.ToString(sqlReader["Name"]) + " " + Convert.ToString(sqlReader["SName"]) + " "
                        + Convert.ToString(sqlReader["TName"]) + " " + Convert.ToString(sqlReader["RecordBookNumber"]) + " " + Convert.ToString(sqlReader["Faculty"]) + " "
                        + Convert.ToString(sqlReader["Course"])));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                {
                    sqlReader.Close();
                }
            }
        }

        private async void buttonDel_Click(object sender, EventArgs e)
        {
            if (labelDelError.Visible)
            {
                labelDelError.Visible = false;
            }


            SqlCommand command = new SqlCommand("DELETE FROM [Students] WHERE [Id] = @Id", sqlConnection);

            command.Parameters.AddWithValue("Id", numericUpDownDelId.Value);
                       
            await command.ExecuteNonQueryAsync();
        }
    }
}
