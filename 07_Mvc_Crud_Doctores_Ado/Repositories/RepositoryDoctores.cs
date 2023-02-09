using _07_Mvc_Crud_Doctores_Ado.Models;
using System.Data;
using System.Data.SqlClient;

namespace _07_Mvc_Crud_Doctores_Ado.Repositories
{
    public class RepositoryDoctores
    {
        SqlCommand command;
        SqlConnection connection;
        SqlDataReader reader;

        public RepositoryDoctores()
        {
            string connectionString = @"Data Source=LOCALHOST\DESARROLLO;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=sa;Password=MCSD2022"; ;
            
            this.connection = new SqlConnection(connectionString);
            this.command = new SqlCommand();
            this.command.Connection = this.connection;
        }

        public List<Doctor> GetDoctores()
        {
            string consulta = "SELECT * FROM DOCTOR";

            this.command.CommandType = CommandType.Text;
            this.command.CommandText = consulta;

            this.connection.Open();
            this.reader = this.command.ExecuteReader();

            List<Doctor> doctores = new List<Doctor>();

            while(this.reader.Read())
            {
                Doctor doctor = new Doctor();
                doctor.HospitalCod = int.Parse(reader["HOSPITAL_COD"].ToString());
                doctor.DoctorNum = int.Parse(reader["DOCTOR_NO"].ToString());
                doctor.Apellido = reader["APELLIDO"].ToString();
                doctor.Especialidad = reader["ESPECIALIDAD"].ToString();
                doctor.Salario = int.Parse(reader["SALARIO"].ToString());

                doctores.Add(doctor);
            }

            this.reader.Close();
            this.connection.Close();

            return doctores;
        }

        public List<Hospital> GetHospitales()
        {
            string consulta = "SELECT * FROM HOSPITAL";

            this.command.CommandType = CommandType.Text;
            this.command.CommandText = consulta;

            this.connection.Open();
            this.reader = this.command.ExecuteReader();

            List<Hospital> hospitales = new List<Hospital>();

            while (this.reader.Read())
            {
                Hospital hospi = new Hospital();
                hospi.HospitalCod = int.Parse(reader["HOSPITAL_COD"].ToString());
                hospi.DoctorNum = int.Parse(reader["DOCTOR_NO"].ToString());
                hospi.Apellido = reader["APELLIDO"].ToString();
                doctor.Especialidad = reader["ESPECIALIDAD"].ToString();
                doctor.Salario = int.Parse(reader["SALARIO"].ToString());

                doctores.Add(doctor);
            }

            this.reader.Close();
            this.connection.Close();

            return doctores;
        }

        public Doctor CreateDoctores(int hospitalCod, int doctorNum, string apellido, string especialidad, int salario)
        {
            string consulta = "INSERT INTO DOCTOR VALUES (@IDHOSPITAL, @DOCTORNUM, @APELLIDO, @ESPECIALIDAD, @SALARIO)";

            SqlParameter paramHospitalCod = new SqlParameter("@IDHOSPITAL", hospitalCod);
            SqlParameter paramDoctorNum = new SqlParameter("@DOCTORNUM", doctorNum);
            SqlParameter paramApellido = new SqlParameter("@APELLIDO", apellido);
            SqlParameter paramEspecialidad = new SqlParameter("@ESPECIALIDAD", especialidad);
            SqlParameter paramSalario = new SqlParameter("@SALARIO", salario);
            this.command.Parameters.Add(paramHospitalCod);
            this.command.Parameters.Add(paramDoctorNum);
            this.command.Parameters.Add(paramApellido);
            this.command.Parameters.Add(paramEspecialidad);
            this.command.Parameters.Add(paramSalario);

            this.command.CommandType = CommandType.Text;
            this.command.CommandText = consulta;

            this.connection.Open();
            this.command.ExecuteNonQuery();

            Doctor doctor = new Doctor();
            doctor.HospitalCod = int.Parse(reader["HOSPITAL_COD"].ToString());
            doctor.DoctorNum = int.Parse(reader["DOCTOR_NO"].ToString());
            doctor.Apellido = reader["APELLIDO"].ToString();
            doctor.Especialidad = reader["ESPECIALIDAD"].ToString();
            doctor.Salario = int.Parse(reader["SALARIO"].ToString());

            this.connection.Close();
            this.command.Parameters.Clear();

            return doctor;
        }
    }
}
