using System.Data.SqlClient;
using System.Data;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace HCCDotNetCore.Shared
{
    public class AdoDotNetService
    {
        private readonly SqlConnectionStringBuilder _connectionStringBuilder;
        public AdoDotNetService(string connectionString)
        {
            _connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
        }
        public List<T> Query<T>(string query, List<SqlParameter>? parameters = null)
        {
            SqlConnection connection = new SqlConnection(_connectionStringBuilder.ConnectionString);
            connection.Open();

            DataTable dt = new DataTable();

            SqlCommand command = new SqlCommand(query, connection);

            if (parameters != null)
            {
                command.Parameters.AddRange(parameters.ToArray());
            }

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            connection.Close();

            string json = JsonConvert.SerializeObject(dt);
            List<T> lst = JsonConvert.DeserializeObject<List<T>>(json);
            return lst!;
        }

        public T QueryFirstOrDefault<T>(string query, List<SqlParameter>? parameters = null)
        {
            SqlConnection connection = new SqlConnection(_connectionStringBuilder.ConnectionString);
            connection.Open();

            DataTable dt = new DataTable();

            SqlCommand command = new SqlCommand(query, connection);

            if (parameters != null)
            {
                command.Parameters.AddRange(parameters.ToArray());
            }

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            connection.Close();

            string json = JsonConvert.SerializeObject(dt);
            List<T> lst = JsonConvert.DeserializeObject<List<T>>(json);
            return lst![0];
        }

        public int Execute(string query, List<SqlParameter>? parameters = null)
        {
            SqlConnection connection = new SqlConnection(_connectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand command = new SqlCommand(query, connection);
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters.ToArray());
            }
            int result = command.ExecuteNonQuery();

            connection.Close();

            return result;
        }

    }
}