using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.Extensions.Configuration;
using WebApplication1.Models;

namespace WebApplication1.Database.Repository
{
    internal class PreviewRepository : IPreviewRepository
    {
        private readonly IConfiguration _configuration;
        public PreviewRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ICollection<Preview> GetPreviews()
        {
            var conString = _configuration.GetConnectionString("DBConnection");
            List<Preview> previewsList = new List<Preview>();
            try
            {
                using (var connection = new SqlConnection(conString))
                {
                    connection.Open();
                    var cmd = new SqlCommand("SP_GetPreviewRecords", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Preview preview = new Preview();
                        preview.ID = Convert.ToInt32(rdr["ID"]);
                        preview.Title = rdr["Title"].ToString();
                        preview.Description = rdr["Description"].ToString();
                        preview.Date_create = Convert.ToDateTime(rdr["Date_create"].ToString());
                        preview.ImagePath = rdr["ImagePath"].ToString();


                        previewsList.Add(preview);
                    }
                }

                return previewsList.ToList();

            }
            catch (Exception)
            {
                throw;
            }



        }

        public Preview GetPreview(int id)
        {
            var conString = _configuration.GetConnectionString("DBConnection");
            List<Preview> previewsList = new List<Preview>();
            try
            {
                using (var connection = new SqlConnection(conString))
                {
                    connection.Open();
                    var cmd = new SqlCommand("SP_GetPreviewRecords", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Preview preview = new Preview();
                        preview.ID = Convert.ToInt32(rdr["ID"]);
                        preview.Title = rdr["Title"].ToString();
                        preview.Description = rdr["Description"].ToString();
                        preview.Date_create = Convert.ToDateTime(rdr["Date_create"].ToString());
                        preview.ImagePath = rdr["Image_Path"].ToString();

                        previewsList.Add(preview);
                    }
                }

                return new Preview();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
    
    



