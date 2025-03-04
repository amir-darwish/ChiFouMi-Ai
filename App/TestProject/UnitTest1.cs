namespace App.Class;
using App.Class;

public class ShapeManagerTests
{
    [Fact]
    public void TestCalculateTotalArea()
    {
        ShapeManager shapeManager = new ShapeManager();
        shapeManager.AddForm(new Rectangle(2, 3, "red"));
        shapeManager.AddForm(new Rond(4, "blue"));
        shapeManager.AddForm(new Triangle(5,4,3,"green"));

        double expectedArea = (2 * 3) + (Math.PI * 4 * 4) + (0.5 * 5 * 4);
        Assert.Equal(expectedArea, shapeManager.CalculateTotalArea(), 2);
    }

    [Fact]
    public void TestCalculateTotalPerimeter()
    {
        ShapeManager shapeManager = new ShapeManager();
        shapeManager.AddForm(new Rectangle(3, 4, "red")); // 14
        shapeManager.AddForm(new Rond(2, "blue"));                  // 12.57
        
        Assert.Equal(26.57, shapeManager.CalculateTotalPerimeter(),2);
    }

    [Fact]
    public void GivenNoShapes_WhenCalculatingTotalArea_ThenResultShouldBeZero()
    {
        ShapeManager shapeManager = new ShapeManager();
        double actualArea = shapeManager.CalculateTotalArea();
        Assert.Equal(0, actualArea);
    }
    
    [Fact]
    public void GivenNoShapes_WhenCalculatingTotalPerimeter_ThenResultShouldBeZero()
    {
        ShapeManager shapeManager = new ShapeManager();
        double totalPerimeter = shapeManager.CalculateTotalPerimeter();
        Assert.Equal(0, totalPerimeter);
    }

    [Fact]
    public void GivenShapes_WhenDisplayingAll_ThenOutputShouldContainShapesInformation()
    {
        ShapeManager shapeManager = new ShapeManager();
        shapeManager.AddForm(new Rectangle(2, 3, "red"));
        shapeManager.AddForm(new Rond(4, "blue"));
        shapeManager.AddForm(new Triangle(5,4,3,"green"));

        using (var sw = new StringWriter())
        {
            Console.SetOut(sw);
            shapeManager.DisplayAll();
            var result = sw.ToString();
            
            Assert.Contains("Rectangle", result);
            Assert.Contains("Rond", result);
            Assert.Contains("Triangle", result);
            Assert.Contains("Total des Aires:", result);
            Assert.Contains("Total des Périmètres:", result);
        }
        
        
    }

}