namespace api.Infrastructure.Dtos.Comment
{
    public class CreateCommentDto
    {
        public string Content { get; set; } = string.Empty;
        public int? ParentCommentId { get; set; }
    }
}