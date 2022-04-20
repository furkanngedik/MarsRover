using MarsRover.Core.Enums;
using MarsRover.Domain.Entities;

namespace MarsRover.Domain.States
{
    public class RoverWestState : IBaseRoverState
    {
        public Coordinate Coordinate { get; set; }

        public Direction Direction
        {
            get
            {
                return Direction.W;
            }
        }

        public RoverWestState(Coordinate coordinate)
        {
            this.Coordinate = coordinate;
        }
        public void MoveForward()
        {
            this.Coordinate.SetXAxis(this.Coordinate.X_Axis - 1);
        }

        public IBaseRoverState TurnLeft()
        {
            return new RoverSouthState(Coordinate);
        }

        public IBaseRoverState TurnRight()
        {
            return new RoverNorthState(Coordinate);
        }
    }
}
