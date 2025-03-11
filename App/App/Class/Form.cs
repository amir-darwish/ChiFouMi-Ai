namespace App.Class;

public abstract class Form
{
        protected string _name;
        protected string _color;
        
        protected Form(string name, string color)
        {
                _name = name;
                _color = color;
        }
        public void setName(string name)
        {
                _name = name;
        }
        public void setColor(string color)
        {
                _color = color;
        }

        public string getColor()
        {
                return _color;
        }
        public abstract double CalculateArea();
        public abstract double CalculatePerimeter();
        public abstract void  DisplayInfo();
        
}
    
