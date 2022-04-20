using MarsRover.Core.Enums;
using MarsRover.Core.Models;
using MarsRover.Domain.Entities;
using System;
using System.Collections.Generic;

namespace MarsRover.Domain
{
    public class MarsRoverService : IMarsRoverService
    {
        public void MoveRover(Plateau plateau, List<char> commands)
        {
            commands.ForEach(command =>
            {
                var isValidEnum = Enum.TryParse(command.ToString(), true, out Command cmd);
                if (isValidEnum)
                    plateau.MoveRover(cmd);
                else
                    throw new BusinessException((int)ErrorCodes.InvalidCommand, "Invalid command");
            });
        }

        public Plateau SetInitialRoverPosition(int x, int y, int xMax, int yMax, Direction direction)
        {
            var plataeu = new Plateau(xMax, yMax);
            plataeu.SetRoverInitialPostion(new Rover(new Coordinate(x, y), direction));

            return plataeu;
        }
    }
}
