using System.ComponentModel.DataAnnotations;

namespace TicTacToe.Models
{
    public class GameCreateInputModel
    {
        [Required]
        public string PlayerXName { get; set; }

        [Required]
        public string PlayerOName { get; set; }
    }
}
