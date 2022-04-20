using MarsRover.Core.Enums;
using MarsRover.Domain.Entities;
using System.Collections.Generic;

namespace MarsRover.Domain
{
    public interface IMarsRoverService
    {
        Plateau SetInitialRoverPosition(int x, int y, int xMax, int yMax, Direction direction);
        void MoveRover(Plateau plateau, List<char> commands);
    }
}
