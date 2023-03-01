using Microsoft.Extensions.Configuration;
using MyBlog.Models.Photo;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using static MyBlog.Repository.PhotoRepository;
using System.Linq;

namespace MyBlog.Repository
{
    
        public class PhotoRepository : IPhotoRepository
        {
            private readonly IConfiguration _config;
            public PhotoRepository(IConfiguration config)
            {
                _config = config;
            }
            public async Task<int> DeleteAsync(int photoID)
            {
                int affectedRows = 0;
                using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();
                    affectedRows = await connection.ExecuteAsync("photo_delete",
                                                               new { photoID = photoID },
                                                               commandType: CommandType.StoredProcedure);
                }

                return affectedRows;
            }

            public async Task<List<Photo>> GetAllByUserId(int userId)
            {
                IEnumerable<Photo> photos;

                using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();
                    photos = await connection.QueryAsync<Photo>("photo_getByUserId",
                                                               new { userID = userId },
                                                               commandType: CommandType.StoredProcedure);
                }
                return photos.ToList();
            }

            public async Task<Photo> GetAsync(int photoID)
            {
                Photo photo;
                using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();
                    photo = await connection.QueryFirstOrDefaultAsync("photo_get",
                                                               new { photoID = photoID },
                                                               commandType: CommandType.StoredProcedure);
                }
                return photo;
            }

            public async Task<Photo> InsertAsync(PhotoCreate ph, int userID)
            {
                Photo photo;
                DataTable dt = new DataTable();
                dt.Columns.Add("ImgURL", typeof(string));
                dt.Columns.Add("PublicId", typeof(string));
                dt.Columns.Add("Description", typeof(string));

                dt.Rows.Add(ph.ImgURL, ph.PublicId, ph.Description);

                int newPhotoID;
                using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();
                    newPhotoID = await connection.ExecuteScalarAsync<int>("photo_insert",
                                                               new { 
                                                                   Photo = dt.AsTableValuedParameter("dbo.photoType") ,
                                                                   UserID= userID
                                                               },
                                                               commandType: CommandType.StoredProcedure);
                }
                photo = await GetAsync(newPhotoID);
                return photo;
            }
        }
    }
