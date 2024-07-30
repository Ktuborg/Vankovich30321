using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GR30321.API.Data;
using GR30321.Domain.Entities;
using GR30321.Domain.Models;

namespace GR30321.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PicturesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public PicturesController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: api/Pictures
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Picture>>> GetPictures()
        //{
        // return await _context.Pictures.ToListAsync();
        //}

        public async Task<ActionResult<ResponseData<ListModel<Picture>>>> GetPictures(string? category,
                                                                                int pageNo = 1,
                                                                                int pageSize = 3)
        {
            // Создать объект результата
            var result = new ResponseData<ListModel<Picture>>();
            // Фильтрация по категории загрузка данных категории
            var data = _context.Pictures.Include(d => d.Category).Where(d => String.IsNullOrEmpty(category) || d.Category.NormalizedName.Equals(category));

            // Подсчет общего количества страниц
            int totalPages = (int)Math.Ceiling(data.Count() / (double)pageSize);
            if (pageNo > totalPages)
                pageNo = totalPages;
            // Создание объекта ProductListModel с нужной страницей данных
            var listData = new ListModel<Picture>()
            {
                Items = await data.Skip((pageNo - 1) * pageSize).Take(pageSize).ToListAsync(),
                CurrentPage = pageNo,
                TotalPages = totalPages
            };
            // поместить данные в объект результата
            result.Data = listData;
            // Если список пустой
            if (data.Count() == 0)
            {
                result.Success = false;
                result.ErrorMessage = "Нет объектов в выбранной категории";
            }
            return result;
        }

        // GET: api/Pictures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseData<Picture>>> GetPicture(int id)
        {
            // Создать объект результата
            var result = new ResponseData<Picture>();
            
            var picture = await _context.Pictures.Include(b => b.Category).FirstOrDefaultAsync(b => b.Id == id);

            result.Data = picture;

            if (result.Data == null)
            {
                result.Success = false;
                result.ErrorMessage = "Данные не найдены";
            }

            return result;
        }

        // PUT: api/Pictures/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPicture(int id, Picture picture)
        {
            if (id != picture.Id)
            {
                return BadRequest();
            }

            _context.Entry(picture).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PictureExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Pictures
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Picture>> PostPicture(Picture picture)
        {
            _context.Pictures.Add(picture);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPicture", new { id = picture.Id }, picture);
        }

        // DELETE: api/Pictures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePicture(int id)
        {
            var picture = await _context.Pictures.FindAsync(id);
            if (picture == null)
            {
                return NotFound();
            }

            // Путь к папке wwwroot/Images
            var imagesPath = Path.Combine(_env.WebRootPath, "Images");
            // Удалить старый файл, если он существует
            if (!string.IsNullOrEmpty(picture.Image))
            {
                var oldFileName = Path.GetFileName(new Uri(picture.Image).LocalPath);
                var oldFilePath = Path.Combine(imagesPath, oldFileName);
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }

            _context.Pictures.Remove(picture);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> SaveImage(int id, IFormFile image)
        {
            // Найти объект по Id
            var picture = await _context.Pictures.FindAsync(id);
            if (picture == null)
            {
                return NotFound();
            }
            // Путь к папке wwwroot/Images
            var imagesPath = Path.Combine(_env.WebRootPath, "Images");
            // получить случайное имя файла
            var randomName = Path.GetRandomFileName();
            // получить расширение в исходном файле
            var extension = Path.GetExtension(image.FileName);
            // задать в новом имени расширение как в исходном файле
            var fileName = Path.ChangeExtension(randomName, extension);
            // полный путь к файлу
            var filePath = Path.Combine(imagesPath, fileName);
            // создать файл и открыть поток для записи
            using var stream = System.IO.File.OpenWrite(filePath);
            // скопировать файл в поток
            await image.CopyToAsync(stream);
            // получить Url хоста
            var host = "https://" + Request.Host;
            // Url файла изображения
            var url = $"{host}/Images/{fileName}";
            // Сохранить url файла в объекте
            picture.Image = url;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("{id}/upload-image")]
        public async Task<IActionResult> UpdateImage(int id, IFormFile image)
        {
            // Найти объект по Id
            var picture = await _context.Pictures.FindAsync(id);
            if (picture == null)
            {
                return NotFound();
            }
            // Путь к папке wwwroot/Images
            var imagesPath = Path.Combine(_env.WebRootPath, "Images");
            // Удалить старый файл, если он существует
            if (!string.IsNullOrEmpty(picture.Image))
            {
                var oldFileName = Path.GetFileName(new Uri(picture.Image).LocalPath);
                var oldFilePath = Path.Combine(imagesPath, oldFileName);
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }
            // получить случайное имя файла
            var randomName = Path.GetRandomFileName();
            // получить расширение в исходном файле
            var extension = Path.GetExtension(image.FileName);
            // задать в новом имени расширение как в исходном файле
            var fileName = Path.ChangeExtension(randomName, extension);
            // полный путь к файлу
            var filePath = Path.Combine(imagesPath, fileName);
            // создать файл и открыть поток для записи
            using var stream = System.IO.File.OpenWrite(filePath);
            // скопировать файл в поток
            await image.CopyToAsync(stream);
            // получить Url хоста
            var host = "https://" + Request.Host;
            // Url файла изображения
            var url = $"{host}/Images/{fileName}";
            // Сохранить url файла в объекте
            picture.Image = url;
            await _context.SaveChangesAsync();
            return Ok();
        }

        private bool PictureExists(int id)
        {
            return _context.Pictures.Any(e => e.Id == id);
        }
    }
}
