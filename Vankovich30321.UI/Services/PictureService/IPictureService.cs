using GR30321.Domain.Entities;
using GR30321.Domain.Models;

namespace Vankovich30321.UI.Services.PictureService
{
    public interface IPictureService
    {

        /// Получение списка всех объектов 
        public Task<ResponseData<ListModel<Picture>>> GetPictureListAsync(string? categoryNormalizedName, int pageNo = 1);

        /// Поиск объекта по Id   
        public Task<ResponseData<Picture>> GetPictureByIdAsync(int id);

        /// Обновление объекта  
        public Task<ResponseData<Picture>> UpdatePictureAsync(int id, Picture picture, IFormFile? formFile);

        /// Удаление объекта  
        public Task<ResponseData<Picture>> DeletePictureAsync(int id);

        /// Создание объекта
        public Task<ResponseData<Picture>> CreatePictureAsync(Picture picture, IFormFile? formFile);
    }
}
