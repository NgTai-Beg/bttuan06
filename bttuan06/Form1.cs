
using bttuan06.models2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.Entity;


namespace bttuan06
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


            try
            {
                Model1 context = new Model1();
                List<Faculty> listFalcultys = context.Faculties.ToList(); //l y các khoa
                List<Student> listStudent = context.Students.ToList(); //l y sinh viên
                FillFalcultyCombobox(listFalcultys);
                BindGrid(listStudent);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        //Hàm binding list có tên hi n th là tên khoa, giá tr là Mã khoa
        private void FillFalcultyCombobox(List<Faculty> listFalcultys)
        {
            this.cmbKhoa.DataSource = listFalcultys;
            this.cmbKhoa.DisplayMember = "FacultyName";
            this.cmbKhoa.ValueMember = "FacultyID";
        }
        //Hàm binding gridView t list sinh viên
        private void BindGrid(List<Student> listStudent)
        {
            dgvHienThi.Rows.Clear();
            foreach (var item in listStudent)
            {
                int index = dgvHienThi.Rows.Add();
                dgvHienThi.Rows[index].Cells[0].Value = item.StudentID;
                dgvHienThi.Rows[index].Cells[1].Value = item.Fullname;
                dgvHienThi.Rows[index].Cells[2].Value = item.Faculty.FacultyName;
                dgvHienThi.Rows[index].Cells[3].Value = item.AverageScore;
            }
        }

        private void dgvHienThi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtMSSV.Text = dgvHienThi.Rows[e.RowIndex].Cells[0].Value?.ToString();
                txtHoTen.Text = dgvHienThi.Rows[e.RowIndex].Cells[1].Value?.ToString();
                cmbKhoa.Text = dgvHienThi.Rows[e.RowIndex].Cells[2].Value?.ToString();
                txtDiemTB.Text = dgvHienThi.Rows[e.RowIndex].Cells[3].Value?.ToString();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                Model1 context = new Model1();

                // Tạo đối tượng sinh viên mới
                Student newStudent = new Student
                {
                    StudentID = txtMSSV.Text,
                    Fullname = txtHoTen.Text,
                    FacultyID = (int)cmbKhoa.SelectedValue,
                    AverageScore = double.Parse(txtDiemTB.Text)
                };

                // Thêm vào database
                context.Students.Add(newStudent);
                context.SaveChanges();

                // Cập nhật lại DataGridView
                LoadData();
                MessageBox.Show("Thêm thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm dữ liệu: " + ex.Message);
            }
        }
        private void LoadData()
        {
            try
            {
                Model1 context = new Model1();
                List<Student> listStudent = context.Students.Include(s => s.Faculty).ToList();
                BindGrid(listStudent);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                Model1 context = new Model1();
                string studentID = txtMSSV.Text;

                // Tìm sinh viên cần sửa trong database
                Student updateStudent = context.Students.FirstOrDefault(s => s.StudentID == studentID);
                if (updateStudent != null)
                {
                    // Cập nhật thông tin
                    updateStudent.Fullname = txtHoTen.Text;
                    updateStudent.FacultyID = (int)cmbKhoa.SelectedValue;
                    updateStudent.AverageScore = double.Parse(txtDiemTB.Text);

                    // Lưu thay đổi
                    context.SaveChanges();
                    LoadData();
                    MessageBox.Show("Cập nhật thành công!");
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sinh viên để cập nhật.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa dữ liệu: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                Model1 context = new Model1();
                string studentID = txtMSSV.Text;

                // Tìm sinh viên cần xóa trong database
                Student deleteStudent = context.Students.FirstOrDefault(s => s.StudentID == studentID);
                if (deleteStudent != null)
                {
                    // Xóa khỏi database
                    context.Students.Remove(deleteStudent);
                    context.SaveChanges();

                    LoadData();
                    MessageBox.Show("Xóa thành công!");
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sinh viên để xóa.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa dữ liệu: " + ex.Message);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
