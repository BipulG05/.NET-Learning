using System.Runtime.CompilerServices;
using WebApi2024.Dtos.Comment;
using WebApi2024.Models;

namespace WebApi2024.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto (this Comment commentModel)
        {
            return new CommentDto
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreateOn = commentModel.CreateOn,
                CreatedBy = commentModel.AppUser.UserName,
                StockId = commentModel.StockId


            };
        }
        public static Comment ToCommentFromCreate(this CreateCommentDto commentDto, int stockId)
        {
            return new Comment
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
                StockId = stockId


            };
        }

        public static Comment ToCommentFromUpdate(this UpdateCommentrequestDto commentModel)
        {
            return new Comment
            {
                Title = commentModel.Title,
                Content = commentModel.Content
            };
        }
    }
}
