namespace RobotWebApp.RobotBoard
{
    public class MoveDirector
    {
        public Robot MoveRobot(Robot robot, Board board, string moveString)
        {
            foreach (char move in moveString)
            {

                switch (move)
                {
                    case 'L':
                        robot.CoordinateDirection.Direction = TurnLeft(robot.CoordinateDirection.Direction);
                        break;
                    case 'R':
                        robot.CoordinateDirection.Direction = TurnRight(robot.CoordinateDirection.Direction);
                        break;
                    case 'F':
                        robot.CoordinateDirection = MoveForward(robot, board);
                        break;
                    default:
                        throw new ArgumentException($"Invalid move command: {move}");
                }
            }
            return robot;
        }

        private Direction TurnLeft(Direction currentDirection) =>
            currentDirection switch
            {
                Direction.S => Direction.E,
                Direction.E => Direction.N,
                Direction.N => Direction.W,
                Direction.W => Direction.S,
                _ => throw new ArgumentException($"Invalid direction: {currentDirection}")
            };

        private Direction TurnRight(Direction currentDirection) =>
            currentDirection switch
            {
                Direction.N => Direction.E,
                Direction.E => Direction.S,
                Direction.S => Direction.W,
                Direction.W => Direction.N,
                _ => throw new ArgumentException($"Invalid direction: {currentDirection}")
            };

        /*
           Moves the robot forward in the direction it is currently facing, if the move is valid (not moving off the board).
           If the move is invalid, an exception is thrown.
        */
        private CoordinateDirection MoveForward(Robot robot, Board board)
        {
            Console.WriteLine($"Moving robot {robot.Id} forward from ({robot.CoordinateDirection.X}, {robot.CoordinateDirection.Y}) facing {robot.CoordinateDirection.Direction}");
            // Check if move is valid (not moving off the board)
            if ((robot.CoordinateDirection.Direction == Direction.S && robot.CoordinateDirection.Y == 0) ||
                (robot.CoordinateDirection.Direction == Direction.W && robot.CoordinateDirection.X == 0) ||
                (robot.CoordinateDirection.Direction == Direction.N && robot.CoordinateDirection.Y == board.Rows - 1) ||
                (robot.CoordinateDirection.Direction == Direction.E && robot.CoordinateDirection.X == board.Columns - 1))
            {
                throw new InvalidOperationException("Cannot move forward, robot is at the edge of the board.");
            }
            else
            {
                // Move robot forward in the direction it is currently facing
                switch (robot.CoordinateDirection.Direction)
                {
                    case Direction.N:
                        robot.CoordinateDirection.Y += 1;
                        break;
                    case Direction.E:
                        robot.CoordinateDirection.X += 1;
                        break;
                    case Direction.S:
                        robot.CoordinateDirection.Y -= 1;
                        break;
                    case Direction.W:
                        robot.CoordinateDirection.X -= 1;
                        break;
                }
            }

            Console.WriteLine($"Moved robot {robot.Id} to ({robot.CoordinateDirection.X}, {robot.CoordinateDirection.Y}) facing {robot.CoordinateDirection.Direction}");

            return robot.CoordinateDirection;
        }
    }
}