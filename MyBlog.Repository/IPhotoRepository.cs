using MyBlog.Models.Photo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Repository
{
    public interface IPhotoRepository
    {
        public Task<Photo> InsertAsync(PhotoCreate ph, int userID);
        public Task<Photo> GetAsync(int photoID);
        public Task<List<Photo>> GetAllByUserId(int userId);
        public Task<int> DeleteAsync(int photoID);

    }
}
