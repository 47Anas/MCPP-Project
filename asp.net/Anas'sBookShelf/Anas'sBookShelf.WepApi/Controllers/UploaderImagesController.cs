using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Anas_sBookShelf.EfCore;
using Anas_sBookShelf.Entities.Uploader;
using Anas_sBookShelf.WepApi.Helpers.ImageUploader;
using static Anas_sBookShelf.WepApi.Attributes.AllowedExtentionAtribute;
using Anas_sBookShelf.Dtos.Uploaders;

namespace Anas_sBookShelf.WepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        #region Data and Const
        private readonly IImageUploader _fileUploader;

        public UploadController(IImageUploader fileUploader)
        {
            _fileUploader = fileUploader;
        }
        #endregion

        #region Actions
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Upload([AllowedExtensions()] IFormFile[] files)
        {
            if (files.Length > 0)
            {
                var imagesNames = _fileUploader.Upload(files);
                var imagesDtos = GetImageDtos(imagesNames);

                return Ok(imagesDtos);
            }
            else
            {
                return BadRequest();
            }
        }
        #endregion

        #region Privates
        private List<UploaderImageDto> GetImageDtos(List<string> imagesNames)
        {
            var imagesNamesDtos = new List<UploaderImageDto>();

            foreach (var imageName in imagesNames)
            {
                var villaImage = new UploaderImageDto();
                villaImage.Id = 0;
                villaImage.Name = imageName;

                imagesNamesDtos.Add(villaImage);
            }

            return imagesNamesDtos;
        }
        #endregion
    }
}