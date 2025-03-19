namespace App.Class;

public class GameSession
{ 
        
        public int Id { get; set; } 
        public int PlayerId { get; set; } 
        public Player Player { get; set; }
        public int TotalRounds { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<GameHistory> GameHistories { get; set; } = new List<GameHistory>();
        
}