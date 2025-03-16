namespace App.Class
{
    public class Player
    {
        public int Id { get; set; }  
        public string Name { get; set; }
        private Form ChosenShape { get; set; }
        public List<GameHistory> GameHistories { get; set; } = new List<GameHistory>();

        public Player(string name)
        {
            Name = name;
        }

        public void ChooseShape(Form shape)
        {
            ChosenShape = shape;
        }

        public enShapeType GetEnumShape()
        {
            switch (ChosenShape)
            {
                case Triangle:
                    return enShapeType.Triangle;
                case Rectangle:
                    return enShapeType.Rectangle;
                default:
                    return enShapeType.Rond;
            }
        }
    }
}