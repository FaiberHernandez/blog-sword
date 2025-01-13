using System.ComponentModel.DataAnnotations;

namespace api.Infrastructure.Dtos.Post
{
    public class RatePostRequestDto
    {
        [Range(1, 5, ErrorMessage = "Rate must be between 1 and 5.")]
        public int Rate { get; set; }
    }
}