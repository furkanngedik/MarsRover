using MarsRover.Core.Enums;
using MarsRover.Domain.Entities;

namespace MarsRover.Domain.States
{
    public interface IBaseRoverState
    {
        Direction Direction { get; }
        Coordinate Coordinate { get; set; }
        void MoveForward();
        IBaseRoverState TurnLeft();
        IBaseRoverState TurnRight();
    }
}
