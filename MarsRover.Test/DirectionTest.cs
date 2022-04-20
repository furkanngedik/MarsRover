using MarsRover.Core.Enums;
using MarsRover.Domain;
using System;
using System.Linq;
using Xunit;

namespace MarsRover.Test
{
    public class DirectionTest
    {
        IMarsRoverService _marsRoverService;
        public DirectionTest()
        {
            this._marsRoverService = new MarsRoverService();
        }

        [Theory]
        [InlineData("1 1 N", 1, 2, "N", 5, 5)]
        [InlineData("1 1 E", 2, 1, "E", 5, 5)]
        [InlineData("1 1 S", 1, 0, "S", 5, 5)]
        [InlineData("1 1 W", 0, 1, "W", 5, 5)]
        public void RoverCommand_RoverShouldMove(string roverPosition, int expectedCoordinateX, int expectedCoordinateY, string expectedRoverDirection, int plateauWidth, int plateauHeight)
        {
            //Parse Rover Location Input
            var roverPositions = roverPosition.Split(" ");

            //Act
            var plateau = _marsRoverService.SetInitialRoverPosition(Convert.ToInt32(roverPositions[0]), Convert.ToInt32(roverPositions[1]), plateauWidth, plateauHeight, Enum.Parse<Direction>(roverPositions[2], true));
            var commands = "M";
            _marsRoverService.MoveRover(plateau, commands.ToList());

            // Assert
            Assert.Equal(expectedCoordinateX, plateau.Rover.RoverState.Coordinate.X_Axis);
            Assert.Equal(expectedCoordinateY, plateau.Rover.RoverState.Coordinate.Y_Axis);
            Assert.Equal(expectedRoverDirection, plateau.Rover.RoverState.Direction.ToString());
        }

        [Theory]
        [InlineData("1 2 N", 1, 2, "W", 5, 5)]
        [InlineData("1 2 E", 1, 2, "N", 5, 5)]
        [InlineData("1 2 S", 1, 2, "E", 5, 5)]
        [InlineData("1 2 W", 1, 2, "S", 5, 5)]
        public void RoverCommand_RoverShouldTurnLeft(string roverPosition, int expectedCoordinateX, int expectedCoordinateY, string expectedRoverDirection, int plateauWidth, int plateauHeight)
        {
            //Parse Rover Location Input
            var roverPositions = roverPosition.Split(" ");

            //Act
            var plateau = _marsRoverService.SetInitialRoverPosition(Convert.ToInt32(roverPositions[0]), Convert.ToInt32(roverPositions[1]), plateauWidth, plateauHeight, Enum.Parse<Direction>(roverPositions[2], true));
            var commands = "L";
            _marsRoverService.MoveRover(plateau, commands.ToList());

            // Assert
            Assert.Equal(expectedCoordinateX, plateau.Rover.RoverState.Coordinate.X_Axis);
            Assert.Equal(expectedCoordinateY, plateau.Rover.RoverState.Coordinate.Y_Axis);
            Assert.Equal(expectedRoverDirection, plateau.Rover.RoverState.Direction.ToString());
        }
        [Theory]
        [InlineData("1 2 N", 1, 2, "E", 5, 5)]
        [InlineData("1 2 E", 1, 2, "S", 5, 5)]
        [InlineData("1 2 S", 1, 2, "W", 5, 5)]
        [InlineData("1 2 W", 1, 2, "N", 5, 5)]
        public void RoverCommand_RoverShouldTurnRight(string roverPosition, int expectedCoordinateX, int expectedCoordinateY, string expectedRoverDirection, int plateauWidth, int plateauHeight)
        {
            //Parse Rover Location Input
            var roverPositions = roverPosition.Split(" ");

            //Act
            var plateau = _marsRoverService.SetInitialRoverPosition(Convert.ToInt32(roverPositions[0]), Convert.ToInt32(roverPositions[1]), plateauWidth, plateauHeight, Enum.Parse<Direction>(roverPositions[2], true));
            var commands = "R";
            _marsRoverService.MoveRover(plateau, commands.ToList());

            // Assert
            Assert.Equal(expectedCoordinateX, plateau.Rover.RoverState.Coordinate.X_Axis);
            Assert.Equal(expectedCoordinateY, plateau.Rover.RoverState.Coordinate.Y_Axis);
            Assert.Equal(expectedRoverDirection, plateau.Rover.RoverState.Direction.ToString());
        }
    }
}
