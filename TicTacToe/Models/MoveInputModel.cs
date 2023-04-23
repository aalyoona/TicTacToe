using System.ComponentModel.DataAnnotations;

namespace TicTacToe.Models
{
    public class MoveInputModel
    {
        [Required]
        [Range(0, 2)]
        public int Xaxis { get; set; }

        [Required]
        [Range(0, 2)]
        public int Yaxis { get; set; }
    }
}
