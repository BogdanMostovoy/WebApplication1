using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.Extensions.Configuration;
using WebApplication1.Models;

namespace WebApplication1.Database.Repository
{
    internal class NewsRepository : INewsRepository
    {
        private readonly IConfiguration _configuration;

        public NewsRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public ICollection<News> GetNews()
        {
            var conString = _configuration.GetConnectionString("DBConnection");
            List<News> newsList = new List<News>();
            try
            {
                using ( var connection = new SqlConnection(conString))
                {
                    connection.Open();
                    var cmd = new SqlCommand("SP_GetNewsRecords", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
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
}