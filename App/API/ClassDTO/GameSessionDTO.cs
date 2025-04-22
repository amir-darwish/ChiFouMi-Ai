namespace API.ClassDTO;

public class GameSessionDTO
{
    public int Id { get; set; }
    public string PlayerName { get; set; }
    public string PlayerShape { get; set; }
    public string ComputerShape { get; set; }
    public string Result { get; set; }
    public int GameSessionId { get; set; }
    public DateTime PlayedAt { get; set; }
}