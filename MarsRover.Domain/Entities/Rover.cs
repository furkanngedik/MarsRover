using MarsRover.Core.Enums;
using MarsRover.Domain.States;

namespace MarsRover.Domain.Entities
{
    public class Rover
    {
        public IBaseRoverState RoverState { get; set; }
        public Rover(Coordinate coordinate, Direction direction)
        {
            var factory = new RoverStateFactory();
            this.RoverState = factory.Create(direction, coordinate);
        }
        public void MoveForward()
        {
            this.RoverState.MoveForward();
        }
        public void TurnLeft()
        {
            this.RoverState = this.RoverState.TurnLeft();
        }
        public void TurnRight()
        {
            this.RoverState = this.RoverState.TurnRight();
        }
    }
}
