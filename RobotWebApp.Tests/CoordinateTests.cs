using Xunit;
using RobotWebApp.RobotBoard;

namespace RobotWebApp.Tests;

public class CoordinateTests
{
    [Theory]
    [InlineData(57, 42)]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    public void createCoordinateTest(int x, int y)
    {
        // CoordinateDirection extens Coordinate, so it should be the same values for X and Y
        var cd = new CoordinateDirection { X = x, Y = y, Direction = Direction.N };
        var c = new Coordinate { X = x, Y = y };
        Assert.Equal(x, cd.X);
        Assert.Equal(x, c.X);
        Assert.Equal(c.X, cd.X);

        Assert.Equal(y, cd.Y);
        Assert.Equal(y, c.Y);
        Assert.Equal(c.Y, cd.Y);

        Assert.Equal(Direction.N, cd.Direction);
    }

}