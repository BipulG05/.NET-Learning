using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi2024.Dtos.Comment;
using WebApi2024.Extensions;
using WebApi2024.Interfaces;
using WebApi2024.Mappers;
using WebApi2024.Models;

namespace WebApi2024.Controllers
{
    [Route("api/comment")]
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepositoty _stockRepo;
        private readonly UserManager<AppUser> _userManager;

        public CommentController(ICommentRepository commentRepo, IStockRepositoty stockRepo, UserManager<AppUser> userManager)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            var comments = await _commentRepo.GetAllAsync();
            var commentDto = comments.Select(s => s.ToCommentDto());
            return Ok(commentDto);
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult>GetById(int id)
        {
            var comment = await _commentRepo.GetByIdAsync(id);
            if (comment == null)
            {
                return null;

            }
            return Ok(comment.ToCommentDto());
        }
        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId,[FromBody] CreateCommentDto commentDto )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!await _stockRepo.StockExists(stockId))
            {
                return BadRequest("Stock not exists");
            }
            var userName = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(userName);
            var commentModel = commentDto.ToCommentFromCreate(stockId);
            commentModel.AppUserId = appUser.Id;
            await _commentRepo.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentrequestDto updateCommentDto )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var comment = await _commentRepo.UpdateAsync(id, updateCommentDto.ToCommentFromUpdate());
            if (comment == null)
            {
                return NotFound("comment Not found!!");
            }
            return Ok(comment.ToCommentDto());
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var commentModel = await _commentRepo.DeleteAsync(id);
            if (commentModel == null)
            {
                return NotFound("comment Not found!!");
            }
            return Ok(commentModel.ToCommentDto());
        }
    }
}
