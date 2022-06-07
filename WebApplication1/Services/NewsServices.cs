using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class NewsServices : INewsService
    {
        public string Constr { get; set; }
        public IConfiguration _configuration;
        public SqlConnection connection;
        public NewsServices(IConfiguration configuration)
        {
            _configuration = configuration;
            Constr = _configuration.GetConnectionString("DBConnection");
        }

        public List<News> GetNewsRecord()
        {
            List<News> newsList = new List<News>();
            try
            {
                using (connection = new SqlConnection(Constr))
                {
                    connection.Open();
                    var cmd = new SqlCommand("SP_GetNewsRecords", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while(rdr.Read())
                    {
                        News news = new News();
                        news.NewsId = Convert.ToInt32(rdr["NewsId"]);
                        news.Title = rdr["Title"].ToString(); 
                        news.Description = rdr["Description"].ToString();


                       newsList.Add(news);
                    }
                }
                return newsList.ToList();

            }
            catch (Exception)
            {


                throw;
            }
        }



    }

    public interface INewsService
    {
        public List<News> GetNewsRecord();
    }
}
