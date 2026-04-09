using Xunit;
using RobotWebApp.RobotBoard;

namespace RobotWebApp.Tests;

public class RobotTests
{
    // testcase used to verify that the Robot class and its properties are working as expected, due to confusion in nswag code
    [Fact]
    public void createRobotTest()
    {
        var robot1 = new Robot { Id = 1, CoordinateDirection = new CoordinateDirection { X = 0, Y = 0, Direction = Direction.N } };
        Assert.Equal(0, robot1.CoordinateDirection.X);
        Assert.Equal(0, robot1.CoordinateDirection.Y);
        Assert.Equal(Direction.N, robot1.CoordinateDirection.Direction);
    }

}