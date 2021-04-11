using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Core.Utilities.Results;
using Core.Utilities.Business;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarBrandImagesController : ControllerBase
    {
        ICarBrandImageService _imageService;

        public CarBrandImagesController(ICarBrandImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _imageService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getimagesbybrand")]
        public IActionResult GetImagesByBrand(int brandId)
        {
            var result = _imageService.GetBrandImagesByBrandId(brandId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int imageId)
        {
            var result = _imageService.GetById(imageId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }



        [HttpPost("add")]
        public IActionResult Add([FromForm] IFormFile[] files, [FromForm] CarBrandImage brandImage)
        {

            var result = _imageService.AddCollective(files, brandImage);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);


        }



        [HttpPost("update")]
        public IActionResult Update([FromForm] IFormFile file, [FromForm] CarBrandImage brandImage)
        {
            var result = _imageService.Update(file, brandImage);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(CarBrandImage brandImage)
        {
            var result = _imageService.Delete(brandImage);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
