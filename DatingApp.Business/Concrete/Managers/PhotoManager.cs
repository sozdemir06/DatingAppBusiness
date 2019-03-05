using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DatingApp.Business.Abstract;
using DatingApp.Business.Extensions;
using DatingApp.Business.Mappings.AutoMapper.Dtos;
using DatingApp.DataAccess.Abstract;
using DatingApp.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace DatingApp.Business.Concrete.Managers
{
    public class PhotoManager : IPhotoService
    {
        private readonly IMapper mapper;
        private readonly IPhotoDal photoDal;
        private Cloudinary _cloudinary;
        private readonly IHttpContextAccessor httpContext;
        private readonly IUserDal userDal;
        private readonly IOptions<CloudinarySettings> cloudinarySettings;
        public PhotoManager(IOptions<CloudinarySettings> cloudinarySettings, IMapper mapper, IPhotoDal photoDal, IHttpContextAccessor httpContext, IUserDal userDal)
        {
            this.userDal = userDal;
            this.httpContext = httpContext;
            this.photoDal = photoDal;
            this.mapper = mapper;
            this.cloudinarySettings = cloudinarySettings;

            Account acc = new Account(
                cloudinarySettings.Value.CloudName,
                cloudinarySettings.Value.ApiKey,
                cloudinarySettings.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);



        }
        public async Task<PhotosForReturnDto> AddPhotoForUser(int userId, PhotoForCreationDto photoForCreationDto)
        {
            if (userId != int.Parse(httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                throw new Exception("You can't upload Photo for this profile because the profile does not belog to you.!!");
            }

            var userFromRepo = await userDal.GetUserWithPhotos(userId);
            
         if (userFromRepo == null)
            {
                throw new Exception("User Could not find.!");
            }
            var file = photoForCreationDto.File;
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);

                }
            }
            photoForCreationDto.Url = uploadResult.Uri.ToString();
            photoForCreationDto.PublicId = uploadResult.PublicId;


            var photo = mapper.Map<Photo>(photoForCreationDto);

            photo.UserId = userFromRepo.Id;


            if (!userFromRepo.Photos.Any(u => u.IsMain))
            {
                photo.IsMain = true;
            }

            var addPhoto = await photoDal.Add(photo);
            if (addPhoto == null)
            {
                throw new Exception("Photo Could not added.!");
            }

            var photoForReturn = mapper.Map<PhotosForReturnDto>(addPhoto);

            return photoForReturn;
        }
    }
}