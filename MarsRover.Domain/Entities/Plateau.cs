using MarsRover.Core.Enums;
using MarsRover.Core.Models;

namespace MarsRover.Domain.Entities
{
    public class Plateau
    {
        public int X_Max { get; private set; }
        public int Y_Max { get; private set; }

        public Rover Rover { get; set; }

        public Plateau(int xMax, int yMax)
        {
            this.X_Max = xMax;
            this.Y_Max = yMax;
        }

        /// <summary>
        /// Rover inital position sets on the plateau.
        /// </summary>
        /// <param name="rover"></param>
        public void SetRoverInitialPostion(Rover rover)
        {
            if (rover.RoverState.Coordinate.X_Axis > this.X_Max || rover.RoverState.Coordinate.Y_Axis > this.Y_Max)
                throw new BusinessException("Initial Rover coordinate cannot be out of the Plateau.");
               
            this.Rover = rover;
        }
        public void MoveRover(Command command)
        {
            switch (command)
            {
                case Command.L:
                    Rover.TurnLeft();
                    break;
                case Command.R:
                    Rover.TurnRight();
                    break;
                case Command.M:
                    if (Rover.RoverState.Coordinate.X_Axis <= this.X_Max && Rover.RoverState.Coordinate.Y_Axis <= this.Y_Max)
                        Rover.MoveForward();
                    break;
                default:
                    break;
            }
        }
    }
}
