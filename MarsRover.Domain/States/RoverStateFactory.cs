using MarsRover.Core.Enums;
using MarsRover.Domain.Entities;

namespace MarsRover.Domain.States
{
    public class RoverStateFactory
    {
        public IBaseRoverState Create(Direction direction, Coordinate coordinate)
        {
            switch (direction)
            {
                case Direction.N:
                    return new RoverNorthState(coordinate);
                case Direction.E:
                    return new RoverEastState(coordinate);
                case Direction.W:
                    return new RoverWestState(coordinate);
                case Direction.S:
                    return new RoverSouthState(coordinate);
                default:
                    return null;
            }
        }
    }
}
