using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using WebApplication1.Database.Repository;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class PreviewServices : IPreviewServices
    {
        public string Constr { get; set; }
        public IConfiguration _configuration;
        public SqlConnection connection;

        public PreviewServices(IConfiguration configuration)
        {
            _configuration = configuration;
            Constr = _configuration.GetConnectionString("DBConnection");
        }

        public List<Preview> GetPreviewRecords()
        {
            List<Preview> previewList = new List<Preview>();
            try
            {
                using (connection = new SqlConnection(Constr))
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


                        previewList.Add(preview);
                    }
                }

                return previewList.ToList();

            }
            catch (Exception)
            {
                throw;
            }
        }



        public interface I
        {
            public List<Preview> GetPreviewRecords();
        }
    }
}
    



