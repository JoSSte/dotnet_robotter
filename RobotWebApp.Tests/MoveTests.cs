using RobotWebApp.RobotBoard;

namespace RobotWebApp.Tests;

public class MoveTests
{
    [Fact]
    public void MoveRobotTest()
    {        
        var robot = new Robot { Id = 1, CoordinateDirection = new CoordinateDirection { X = 0, Y = 0, Direction = Direction.N } };
        var board = new Board { Id = 1, Rows = 5, Columns = 5 };

        // Test turning left from North
        var newDirection = MoveDirector.MoveRobot(robot, board, "L");
        Assert.Equal(Direction.W, newDirection.CoordinateDirection.Direction);

        // Test turning right from West
        robot.CoordinateDirection.Direction = Direction.W;
        newDirection = MoveDirector.MoveRobot(robot, board, "R");
        Assert.Equal(Direction.N, newDirection.CoordinateDirection.Direction);

        // Test moving forward from North (should not change direction)
        //robot.CoordinateDirection.Direction = Direction.N;
        //newDirection = MoveDirector.MoveRobot(robot, board, "F");
        //Assert.Equal(Direction.N, newDirection.CoordinateDirection.Direction);
        
    }

    // many testcases since I had x,y but rows,columns instead of columns,rows in the testcase
    [Theory]
    [InlineData( 0,  0,  2,  2, Direction.S, "Cross below Y axis")]
    [InlineData( 0,  0,  2,  2, Direction.W, "Cross below Y axis")]
    [InlineData( 4,  4,  5,  5, Direction.N, "Go out of bounds above X axis")]
    [InlineData( 4,  4,  5,  5, Direction.E, "Go out of bounds above Y axis")]
    [InlineData(14, 14, 15, 15, Direction.N, "Go out of bounds above X axis. Bigger board.")]
    [InlineData(14, 14, 15, 15, Direction.E, "Go out of bounds above Y axis. Bigger board.")]
    [InlineData( 3,  4,  4,  5, Direction.E, "Go out of bounds above Y axis. Rectangular board")]
    public void InvalidMoveTest(int x, int y, int columns, int rows, Direction direction, string description)
    {
        Console.WriteLine($"Testing invalid move for Robot at ({x}, {y}) facing {direction} on a {rows}x{columns} board {description}");
        var r = new Robot { Id = 1, CoordinateDirection = new CoordinateDirection { X = x, Y = y, Direction = direction } };
        var b = new Board { Id = 1, Rows = rows, Columns = columns };
        Assert.Throws<InvalidOperationException>(() => MoveDirector.MoveRobot(r, b, "F"));
    }

    [Theory]
    [InlineData( 0,  1,  2,  2, Direction.S,  "FF", "Cross below X axis")]
    [InlineData( 1,  0,  2,  2, Direction.W,  "FF", "Cross below Y axis")]
    [InlineData( 4,  3,  5,  5, Direction.N,  "FF", "Go out of bounds above X axis")]
    [InlineData( 3,  4,  5,  5, Direction.E,  "FF", "Go out of bounds above Y axis")]
    [InlineData( 0,  1,  2,  2, Direction.W, "LFF", "Turn andCross below X axis")]
    [InlineData( 1,  0,  2,  2, Direction.S, "RFF", "Turn andCross below Y axis")]
    public void InvalidComplexMoveTest(int x, int y, int columns, int rows, Direction direction, string moveString,string description)
    {
        Console.WriteLine($"Testing invalid move \"{moveString}\" for Robot at ({x}, {y}) facing {direction} on a {rows}x{columns} board {description}");
        var r = new Robot { Id = 1, CoordinateDirection = new CoordinateDirection { X = x, Y = y, Direction = direction } };
        var b = new Board { Id = 1, Rows = rows, Columns = columns };
        Assert.Throws<InvalidOperationException>(() => MoveDirector.MoveRobot(r, b, moveString));
    }
}
