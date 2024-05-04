using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZMHDotNetCore.Shared
{
    public class AdoDotNetServices
    {
        private readonly string _connectionString;

        public AdoDotNetServices(string connectionString)
        {
            _connectionString = connectionString;
        }   

        public List<T> Query<T>(string query,params AdoDotNetParameters[]? parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            if (parameters.Length > 0 && parameters is not null)
            {
                /*foreach (var item in parameters)
                {
                    cmd.Parameters.AddWithValue(item.Name, item.Value);
                }*/
                //cmd.Parameters.AddRange(parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray());

                var paramsArray = parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray();
                cmd.Parameters.AddRange(paramsArray);
            }

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            var json = JsonConvert.SerializeObject(dt);
            var list = JsonConvert.DeserializeObject<List<T>>(json);
            return list;
        }

        public T QueryFirstOrDefault<T>(string query, params AdoDotNetParameters[]? parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            if (parameters is not null && parameters.Length > 0)
            {
                var parametersArray = parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray();
                cmd.Parameters.AddRange(parametersArray);
            }
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            connection.Close();

            string json = JsonConvert.SerializeObject(dt); 
            List<T> lst = JsonConvert.DeserializeObject<List<T>>(json)!;

            return lst[0];
        }

        public int Execute(string query, params AdoDotNetParameters[]? parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            if (parameters is not null && parameters.Length > 0)
            {
                var parametersArray = parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray();
                cmd.Parameters.AddRange(parametersArray);
            }
            int result = cmd.ExecuteNonQuery();

            connection.Close();

            return result;
        }

    }

    public class AdoDotNetParameters
    {
        public AdoDotNetParameters() { }
        public AdoDotNetParameters(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public object Value { get; set; }
    }
}
