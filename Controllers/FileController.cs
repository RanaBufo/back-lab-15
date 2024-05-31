using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace HandCrafter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        private readonly string _uploadsFolderPath;

        public FileController(IWebHostEnvironment environment)
        {
            _uploadsFolderPath = Path.Combine(environment.WebRootPath, "img");
        }

        [HttpPost("Upload")]
        public IActionResult Upload(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("Файл не выбран");

                // Генерируем уникальное имя для файла на сервере
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string filePath = Path.Combine(_uploadsFolderPath, fileName);

                // Сохраняем файл на сервер
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                // Формируем URL для доступа к загруженному файлу
                string fileUrl = Path.Combine("/img", fileName);

                // Формируем информацию о загруженном файле
                var fileInfo = new
                {
                    Name = file.FileName,
                    Extension = Path.GetExtension(file.FileName),
                    Url = fileUrl
                };

                return Ok(fileInfo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка при загрузке файла: {ex.Message}");
            }
        }
    }
}
