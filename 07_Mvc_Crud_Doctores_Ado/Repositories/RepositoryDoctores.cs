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
            string connectionStringCasa = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=sa;Password=MCSD2022"; ;

            this.connection = new SqlConnection(connectionStringCasa);
            this.command = new SqlCommand();
            this.command.Connection = this.connection;
        }

        public List<Doctor> GetDoctores()
        {
            string consulta = "SELECT * FROM DOCTOR ORDER BY HOSPITAL_COD";

            this.command.CommandType = CommandType.Text;
            this.command.CommandText = consulta;

            this.connection.Open();
            this.reader = this.command.ExecuteReader();

            List<Doctor> doctores = new List<Doctor>();

            while(this.reader.Read())
            {
                Doctor doctor = new Doctor();
                doctor.HospitalCod = reader["HOSPITAL_COD"].ToString();
                doctor.DoctorNum = reader["DOCTOR_NO"].ToString();
                doctor.Apellido = reader["APELLIDO"].ToString();
                doctor.Especialidad = reader["ESPECIALIDAD"].ToString();
                doctor.Salario = int.Parse(reader["SALARIO"].ToString());

                doctores.Add(doctor);
            }

            this.reader.Close();
            this.connection.Close();

            return doctores;
        }

        public Doctor FindDoctor(string doctorNum)
        {
            string consulta = "SELECT * FROM DOCTOR WHERE DOCTOR_NO = @DOCTORNUM";

            SqlParameter paramDoctorNum = new SqlParameter("@DOCTORNUM", doctorNum);
            this.command.Parameters.Add(paramDoctorNum);

            this.command.CommandType = CommandType.Text;
            this.command.CommandText = consulta;

            this.connection.Open();

            this.reader = this.command.ExecuteReader();
            this.reader.Read();

            Doctor doc = new Doctor();
            doc.HospitalCod = reader["HOSPITAL_COD"].ToString();
            doc.DoctorNum = reader["DOCTOR_NO"].ToString();
            doc.Apellido = reader["APELLIDO"].ToString();
            doc.Especialidad = reader["ESPECIALIDAD"].ToString();
            doc.Salario = int.Parse(reader["SALARIO"].ToString());

            this.reader.Close();
            this.connection.Close();
            this.command.Parameters.Clear();

            return doc;
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
                hospi.HospitalCod = reader["HOSPITAL_COD"].ToString();
                hospi.Nombre = reader["NOMBRE"].ToString();
                hospi.Direccion = reader["DIRECCION"].ToString();
                hospi.Telefono = reader["TELEFONO"].ToString();
                hospi.NumCama = reader["NUM_CAMA"].ToString();

                hospitales.Add(hospi);
            }

            this.reader.Close();
            this.connection.Close();

            return hospitales;
        }

        public string GetMaxNumDoctor()
        {
            string consulta = "SELECT MAX(DOCTOR_NO) + 1 AS MAXIMO FROM DOCTOR";
            this.command.CommandType = CommandType.Text;
            this.command.CommandText = consulta;
            this.connection.Open();
            //SI LA CONSULTA CONTIENE SOLAMENTE UNA FILA Y UN DATO        
            //NO ES NECESARIO UN READER, PODEMOS UTILIZAR EL METODO        
            //ExecuteScalar()
            string maximo = this.command.ExecuteScalar().ToString();
            this.connection.Close();
            return maximo;
        }

        public void CreateDoctores(string hospitalCod, string doctorNum, string apellido, string especialidad, int salario)
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

            this.connection.Close();
            this.command.Parameters.Clear();
        }

        public void DeleteDoctores(string doctorNum)
        {
            string consulta = "DELETE FROM DOCTOR WHERE DOCTOR_NO = @DOCTORNUM";

            SqlParameter paramDoctorNum = new SqlParameter("@DOCTORNUM", doctorNum);
            this.command.Parameters.Add(paramDoctorNum);

            this.command.CommandType = CommandType.Text;
            this.command.CommandText = consulta;

            this.connection.Open();
            this.command.ExecuteNonQuery();

            this.connection.Close();
            this.command.Parameters.Clear();
        }

        public void UpdateDoctor(string hospitalCod, string apellido, string especialidad, int salario, string doctorNum)
        {
            string consulta = "UPDATE DOCTOR SET HOSPITAL_COD = @IDHOSPITAL, APELLIDO = @APELLIDO, ESPECIALIDAD = @ESPECIALIDAD, SALARIO = @SALARIO WHERE DOCTOR_NO = @DOCTORNUM";

            SqlParameter paramHospitalCod = new SqlParameter("@IDHOSPITAL", hospitalCod);
            SqlParameter paramApellido = new SqlParameter("@APELLIDO", apellido);
            SqlParameter paramEspecialidad = new SqlParameter("@ESPECIALIDAD", especialidad);
            SqlParameter paramSalario = new SqlParameter("@SALARIO", salario);
            SqlParameter paramDoctorNum = new SqlParameter("@DOCTORNUM", doctorNum);
            this.command.Parameters.Add(paramHospitalCod);
            this.command.Parameters.Add(paramApellido);
            this.command.Parameters.Add(paramEspecialidad);
            this.command.Parameters.Add(paramSalario);
            this.command.Parameters.Add(paramDoctorNum);

            this.command.CommandType = CommandType.Text;
            this.command.CommandText = consulta;

            this.connection.Open();
            this.command.ExecuteNonQuery();

            this.connection.Close();
            this.command.Parameters.Clear();
        }
    }
}
