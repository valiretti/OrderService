using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using OrderService.DataProvider.Entities;
using OrderService.DataProvider.Repositories;
using OrderService.Model;

namespace OrderService.Logic.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IRepository<Photo> _repository;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ICommitProvider _commitProvider;

        public PhotoService(IRepository<Photo> repository, IHostingEnvironment hostingEnvironment, ICommitProvider commitProvider)
        {
            _repository = repository;
            _hostingEnvironment = hostingEnvironment;
            _commitProvider = commitProvider;
        }

        public async Task<IEnumerable<int>> Create(CreatePhotoModel item)
        {
            var photoIds = new List<int>();

            if (item.Files != null)
            {
                foreach (var file in item.Files)
                {
                    var path = $"{Guid.NewGuid():N}{Path.GetExtension(file.FileName)}";
                    var imagePath = Path.Combine("Files", path);

                    using (var fileStream = new FileStream(Path.Combine(_hostingEnvironment.WebRootPath, imagePath), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    var photo = new Photo
                    {
                        PhotoPath = imagePath
                    };
                    await _repository.Create(photo);
                    await _commitProvider.SaveAsync();
                    photoIds.Add(photo.Id);
                }
            }

            return photoIds;
        }

        public async Task<IEnumerable<Photo>> GetByIds(IEnumerable<int> ids)
        {
            var photos = await _repository.GetAll().Where(photo => ids.Contains(photo.Id)).ToListAsync();
            return photos;
        }
    }
}
