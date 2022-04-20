using MarsRover.Core.Enums;
using MarsRover.Domain.Entities;

namespace MarsRover.Domain.States
{
    public class RoverNorthState : IBaseRoverState
    {
        public Coordinate Coordinate { get; set; }
        public Direction Direction
        {
            get
            {
                return Direction.N;
            }
        }
        public RoverNorthState(Coordinate coordinate)
        {
            this.Coordinate = coordinate;
        }
        public void MoveForward()
        {
            this.Coordinate.SetYAxis(this.Coordinate.Y_Axis + 1);
        }

        public IBaseRoverState TurnLeft()
        {
            return new RoverWestState(Coordinate);
        }

        public IBaseRoverState TurnRight()
        {
            return new RoverEastState(Coordinate);
        }
    }
}
