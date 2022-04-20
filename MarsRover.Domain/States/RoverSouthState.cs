using MarsRover.Core.Enums;
using MarsRover.Domain.Entities;

namespace MarsRover.Domain.States
{
    public class RoverSouthState : IBaseRoverState
    {
        public Coordinate Coordinate { get; set; }
        public Direction Direction
        {
            get
            {
                return Direction.S;
            }
        }
        public RoverSouthState(Coordinate coordinate)
        {
            this.Coordinate = coordinate;
        }
        public void MoveForward()
        {
            this.Coordinate.SetYAxis(this.Coordinate.Y_Axis - 1);
        }

        public IBaseRoverState TurnLeft()
        {
            return new RoverEastState(Coordinate);
        }

        public IBaseRoverState TurnRight()
        {
            return new RoverWestState(Coordinate);
        }
    }
}
