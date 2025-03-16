using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Class
{
    public class GameHistory
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("Player")]
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        
        public enShapeType PlayerShape { get; set; }
        public enShapeType ComputerShape { get; set; }
        
        public string Result { get; set; }
        public DateTime PlayedAt { get; set; } = DateTime.Now;
    }
}