using MarsRover.Core.Enums;
using MarsRover.Domain.Entities;

namespace MarsRover.Domain.States
{
    public class RoverEastState : IBaseRoverState
    {
        public Coordinate Coordinate { get; set; }
        public Direction Direction
        {
            get
            {
                return Direction.E;
            }
        }
        public RoverEastState(Coordinate coordinate)
        {
            this.Coordinate = coordinate;
        }
        public void MoveForward()
        {
            this.Coordinate.SetXAxis(this.Coordinate.X_Axis + 1);
        }

        public IBaseRoverState TurnLeft()
        {
            return new RoverNorthState(Coordinate);
        }

        public IBaseRoverState TurnRight()
        {
            return new RoverSouthState(Coordinate);
        }
    }
}
