using MarsRover.Core.Enums;
using MarsRover.Core.Models;
using MarsRover.Domain;
using MarsRover.Domain.Entities;
using System;
using System.Linq;
using Xunit;

namespace MarsRover.Test
{
    public class PlateauTest
    {
        IMarsRoverService _marsRoverService;
        public PlateauTest()
        {
            this._marsRoverService = new MarsRoverService();
        }
        [Theory]
        [InlineData("5 5", 5, 5)]
        [InlineData("1 1", 1, 1)]
        [InlineData("2 9", 2, 9)]
        [InlineData("5 1", 5, 1)]
        public void WhenCoordinatesAreGiven_ThenShouldDefinePlateau(string plateauLimits, int width, int height)
        {
            var plateauMaxLimits = plateauLimits.Split(" ");

            // Arrange
            var plateau = new Plateau(Convert.ToInt32(plateauMaxLimits[0]), Convert.ToInt32(plateauMaxLimits[1]));

            // Assert
            Assert.Equal(width, plateau.X_Max);
            Assert.Equal(height, plateau.Y_Max);
        }
        [Theory]
        [InlineData("3 3 N", 2, 2)]
        public void WhenCoordinatesAreGiven_ThenSetRoverInitialPosition(string roverPosition, int plateauWidth, int plateauHeight)
        {
            //Parse Rover Location Input
            var roverPositions = roverPosition.Split(" ");
            Assert.Throws<BusinessException>(() => _marsRoverService.SetInitialRoverPosition(Convert.ToInt32(roverPositions[0]), Convert.ToInt32(roverPositions[1]), plateauWidth, plateauHeight, Enum.Parse<Direction>(roverPositions[2], true)));
        }
        [Theory]
        [InlineData("X")]
        public void WhenCreatingRover_ThenShouldThrowFormatException(string roverCommand)
        {
            Assert.Throws<BusinessException>(() => _marsRoverService.MoveRover(null, roverCommand.ToList()));
        }
        [Theory]
        [InlineData("1 5 6")]
        public void WhenPlateauInitialCoordinate_ThenShouldThrowLengthException(string plateauCommand)
        {
            var plateauLimit = plateauCommand.Split(" ");
            Assert.Throws<BusinessException>(() =>
            {
                if (plateauLimit.Length != 2)
                    throw new BusinessException("The Plateau coordinates must be 2 dimensions.");
            });
        }
        [Theory]
        [InlineData("6 5 6 N")]
        public void WhenRoverInitialCoordinate_ThenShouldThrowLengthException(string roverCommand)
        {
            var roverPosition = roverCommand.Split(" ");
            Assert.Throws<BusinessException>(() =>
            {
                if (roverPosition.Length != 3)
                    throw new BusinessException("Rover starting coordinates and direction must be specified.");
            });
        }
        [Theory]
        [InlineData("X 2")]
        public void WhenPlateauInitialCoordinate_ThenShouldThrowFormatException(string plateauCommand)
        {
            var plateauLimit = plateauCommand.Split(" ");
            Assert.Throws<Exception>(() =>
            {
                foreach (var limit in plateauLimit)
                {
                    if (limit.GetType() != typeof(int))
                        throw new Exception();
                }
            });
        }
        [Theory]
        [InlineData("X 2 M")]
        [InlineData("1 X M")]
        [InlineData("1 2 X")]
        public void WhenRoverInitialCoordinate_ThenShouldThrowFormatException(string roverCommand)
        {
            var roverPosition = roverCommand.Split(" ");
            Assert.Throws<Exception>(() =>
            {
                if (roverPosition[0].GetType() != typeof(int) || roverPosition[1].GetType() != typeof(int) || !Enum.TryParse<Direction>(roverPosition[2], out Direction direction))
                    throw new Exception();
            });
        }
        [Theory]
        [InlineData(new object[] { "5 5", "1 2 N", "LMLMLMLMM", 1, 3, Direction.N })]
        [InlineData(new object[] { "5 5", "3 3 E", "MMRMMRMRRM", 5, 1, Direction.E })]
        public void MarsRoverCase_ShouldGiveExpectedOutputWithTestInputs(string plateauSize, string roverLocation, string roverCommands, int expectedCoordinateX, int expectedCoordinateY, Direction expectedRoverDirection)
        {
            //Parse Plateau Input
            var plateauMaxCoordinateInputs = plateauSize.Split(' ');

            //Parse Rover Location Input
            var roverLocationInputs = roverLocation.Split(' ');

            // Arrange
            var plateau = _marsRoverService.SetInitialRoverPosition(Convert.ToInt32(roverLocationInputs[0]), Convert.ToInt32(roverLocationInputs[1]), Convert.ToInt32(plateauMaxCoordinateInputs[0]),
                                                                           Convert.ToInt32(plateauMaxCoordinateInputs[1]), Enum.Parse<Direction>(roverLocationInputs[2], true));
            _marsRoverService.MoveRover(plateau, roverCommands.ToList());
            //Act
            Assert.Equal(expectedCoordinateX, plateau.Rover.RoverState.Coordinate.X_Axis);
            Assert.Equal(expectedCoordinateY, plateau.Rover.RoverState.Coordinate.Y_Axis);
            Assert.Equal(expectedRoverDirection, plateau.Rover.RoverState.Direction);
        }
    }
}
