using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Datingapp.API.Data;
using Datingapp.API.DTO;
using Datingapp.API.Helpers;
using Datingapp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Datingapp.API.Controllers
{

    [Authorize]
    [Route("api/users/{userId}/photos")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IDateingRepositry _repo;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudanirySettingd> _cloudinaryConfig;
        private Cloudinary _cloudinary;

        public PhotosController(IDateingRepositry repo, IMapper mapper, IOptions<CloudanirySettingd> cloudinaryConfig)
        {
            _cloudinaryConfig = cloudinaryConfig;
            _mapper = mapper;
            _repo = repo;

            Account acc = new Account(

                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret

            );

            _cloudinary = new Cloudinary(acc);

        }
        [HttpGet("{id}", Name = "GetPhoto")]

        public async Task<IActionResult> GetPhoto(int id){
            var photoFromRepo = await _repo.GetPhoto(id);

            var photo = _mapper.Map<PhotoForReturnDTO>(photoFromRepo);

            return Ok(photo);
        }

        [HttpPost]

        public async Task<IActionResult> AddPhotoForUser(int userId, [FromForm]PhotoForCreateDTO photoForCreateDTO){

             if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }
            var userFromRepo = await _repo.GetUser(userId);

            var file = photoForCreateDTO.File;

            var uploudResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream()){
                    var uploadparm = new ImageUploadParams(){
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation()
                        .Width(500).Height(500).Crop("fill").Gravity("face")
                    };

                    uploudResult= _cloudinary.Upload(uploadparm);


                }
            }

            photoForCreateDTO.Url = uploudResult.Uri.ToString();
            photoForCreateDTO.PublicId = uploudResult.PublicId;

            var photo = Mapper.Map<Photo>(photoForCreateDTO);

            if (!userFromRepo.Photos.Any(u => u.IsMain))
                photo.IsMain = true;

                userFromRepo.Photos.Add(photo);

                

                if (await _repo.SaveAll())
                {
                    var photoToReturn = _mapper.Map<PhotoForReturnDTO>(photo);

                    return CreatedAtRoute("GetPhoto", new { id = photo.Id}, photo);
                }

                return BadRequest("Error Uplod of Photo");
        }

        [HttpPost("{id}/setmain")]
        public async Task<IActionResult> SetMainPhoto(int userId, int id){
             if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            
            var user = await _repo.GetUser(userId);

            if(!user.Photos.Any(p => p.Id == id))
            return Unauthorized();

            var Photofromrepo = await _repo.GetPhoto(id);

            if(Photofromrepo.IsMain)
            return BadRequest("This is already the main Photo");

            var currentMain = await _repo.GetMainPhoto(userId);
            currentMain.IsMain = false;

            Photofromrepo.IsMain = true;

            if(await _repo.SaveAll())
            return NoContent();

            return BadRequest("Could not set to Main");
        }

        [HttpDelete("{id}")]
    
        public async Task<IActionResult> DeletePhoto(int userId, int id){

        if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            
            var user = await _repo.GetUser(userId);

            if(!user.Photos.Any(p => p.Id == id))
            return Unauthorized();

            var Photofromrepo = await _repo.GetPhoto(id);

            if(Photofromrepo.IsMain)
            return BadRequest("This is the main photo");

            if(Photofromrepo != null){
                var Deleteparams = new DeletionParams(Photofromrepo.PublicId);
            var  reults = _cloudinary.Destroy(Deleteparams);

            if (reults.Result == "ok")
            {
                _repo.Delete(Photofromrepo);
            }
            }else{
                 _repo.Delete(Photofromrepo);
            }

            if(await _repo.SaveAll())
            return Ok();

            return BadRequest("Fail To Delete");

        }
    }
}