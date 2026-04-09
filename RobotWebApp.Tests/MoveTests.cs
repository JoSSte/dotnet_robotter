using RobotWebApp.RobotBoard;

namespace RobotWebApp.Tests;

public class MoveTests
{
    [Fact]
    public void MoveRobotTest()
    {
        var moveDirector = new MoveDirector();
        
        var robot = new Robot { Id = 1, CoordinateDirection = new CoordinateDirection { X = 0, Y = 0, Direction = Direction.N } };
        var board = new Board { Id = 1, Rows = 5, Columns = 5 };

        // Test turning left from North
        var newDirection = moveDirector.MoveRobot(robot, board, "L");
        Assert.Equal(Direction.W, newDirection.CoordinateDirection.Direction);

        // Test turning right from West
        robot.CoordinateDirection.Direction = Direction.W;
        newDirection = moveDirector.MoveRobot(robot, board, "R");
        Assert.Equal(Direction.N, newDirection.CoordinateDirection.Direction);

        // Test moving forward from North (should not change direction)
        //robot.CoordinateDirection.Direction = Direction.N;
        //newDirection = moveDirector.MoveRobot(robot, board, "F");
        //Assert.Equal(Direction.N, newDirection.CoordinateDirection.Direction);
        
    }


    [Theory]
    [InlineData(0, 0, 5, 5, Direction.S)]
    [InlineData(0, 0, 5, 5, Direction.W)]
    [InlineData(5, 5, 5, 5, Direction.N)]
    [InlineData(5, 5, 5, 5, Direction.E)]
    public void InvalidMoveTest(int x, int y, int rows, int columns, Direction direction)
    {
        var r = new Robot { Id = 1, CoordinateDirection = new CoordinateDirection { X = x, Y = y, Direction = direction } };
        var b = new Board { Id = 1, Rows = rows, Columns = columns };
        MoveDirector md = new MoveDirector();
        Assert.Throws<InvalidOperationException>(() => md.MoveRobot(r, b, "F"));
    }


}
