﻿
//using NZWalks.API.Data;
//using NZWalks.API.Models.Domain;
//using Tourist.API.Data;

//namespace NZWalks.API.Repositories
//{
//    public class LocalImageRepository: IImageRepository
//    {
//        private readonly IWebHostEnvironment webHostEnvironment;
//        private readonly IHttpContextAccessor httpContextAccessor;
//        private readonly TouristDbContext dbContext;
//        public LocalImageRepository(IWebHostEnvironment webHostEnvironment,
//            IHttpContextAccessor httpContextAccessor,
//            TouristDbContext dbContext) 
//        {
//            this.webHostEnvironment = webHostEnvironment;
//            this.httpContextAccessor = httpContextAccessor;
//            this.dbContext = dbContext;
//        }
//        public async Task<Image> Upload(Image image)
//        {
//            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images",
//                $"{image.FileName}{image.FileExtention}");
//            using var stream = new FileStream(localFilePath, FileMode.Create);
//            await image.File.CopyToAsync(stream);
//            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.Path}/Images/{image.FileName}{image.FileExtention}";
//            image.FilePath = urlFilePath;

//            await dbContext.Images.AddAsync(image);
//            await dbContext.SaveChangesAsync();

//            return image;
//        }
//    }
//}
