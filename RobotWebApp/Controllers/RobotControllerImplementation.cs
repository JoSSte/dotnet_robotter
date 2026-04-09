using System.Collections.Generic;
using System.Threading.Tasks;

namespace RobotWebApp.RobotBoard
{
    public class RobotControllerImpl : IRobotController
    {
        private readonly List<Board> _boards = new();
        private readonly List<Robot> _robots = new();

        public Task<Board> CreateBoardAsync(Dimensions body)
        {
            if (body.Rows <= 2 || body.Columns <= 2)
            {
                //TODO: return nice error according to spec
                throw new ArgumentException("Rows and columns must be greater than two.");
            }

            var board = new Board { Id = _boards.Count + 1, Rows = body.Rows, Columns = body.Columns };
            _boards.Add(board);
            return Task.FromResult(board);
        }

        public Task<ICollection<Board>> GetAllBoardsAsync() => Task.FromResult((ICollection<Board>)_boards);

        public Task<Board> GetBoardByIdAsync(int boardId) =>
            Task.FromResult(_boards.Find(b => (int)b.Id == boardId)!);

        public Task DeleteBoardAsync(int boardId)
        {
            _boards.RemoveAll(b => (int)b.Id == boardId);
            return Task.CompletedTask;
        }

        public Task<ICollection<Robot>> GetRobotsOnBoardAsync(int boardId) =>
            Task.FromResult((ICollection<Robot>)_robots.FindAll(r => (int)r.Id == boardId));

        public Task<Robot> RobotAsync(int boardId, Robot body)
        {
            _robots.Add(body);
            return Task.FromResult(body);
        }

        public Task<ICollection<Robot>> GetAllRobotsAsync() => Task.FromResult((ICollection<Robot>)_robots);

        public Task<Robot> GetRobotByIdAsync(int robotId) =>
            Task.FromResult(_robots.Find(r => (int)r.Id == robotId)!);

        public Task DeleteRobotAsync(int robotId)
        {
            _robots.RemoveAll(r => (int)r.Id == robotId);
            return Task.CompletedTask;
        }

        public Task<CoordinateDirection> MoveRobotAsync(int robotId, string body)
        {
            //Check that the robot exists
            var robot = _robots.Find(r => (int)r.Id == robotId);
            
            if (robot == null)
            {
                //TODO: return nice error according to spec
                throw new ArgumentException($"Robot {robotId} not found.");
            }
            else if (body == "")
            {
                //TODO: return nice error according to spec
                throw new ArgumentException("Empty move string is not allowed.");
            }
            else
            {
                // validate move string
                if (!IsValidMoveString(body))
                {
                    //TODO: return nice error according to spec
                    throw new ArgumentException("Invalid move string.");
                }
                else
                {
                    try
                    {
                        var board = _boards.Find(b => (int)b.Id == (int)robot.Id);
                        if (board == null)
                        {
                            throw new ArgumentException($"Board {(int)robot.Id} not found for robot {robotId}.");
                        }
                        return Task.FromResult(MoveDirector.MoveRobot(robot, board, body).CoordinateDirection);
                    }
                    catch (InvalidOperationException ex)
                    {
                        throw new ArgumentException("Invalid move command. would move robot past edge of board.");

                    }

                    // If valid, move robot and update coordinates

                    // return coordinates
                    return Task.FromResult(robot.CoordinateDirection);
                    
                }
            }
        }

        /*
            Validates that the move string only contains 'L', 'R', and 'F' characters.
            Does NOT validate that the move string results in a valid move on the board, only that the string itself is valid.
        */
        private bool IsValidMoveString(string moveString)
        {
            foreach (char c in moveString)
            {
                if (c != 'L' && c != 'R' && c != 'F')
                {
                    return false;
                }
            }
            return true;
        }
    }

}
