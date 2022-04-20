using MarsRover.Core.Enums;

namespace MarsRover.Domain.Entities
{
    public class Coordinate
    {
        public int X_Axis { get; private set; }
        public int Y_Axis { get; private set; }

        public Coordinate(int x_Axis, int y_Axis)
        {
            this.X_Axis = x_Axis;
            this.Y_Axis = y_Axis;
        }

        public void SetXAxis(int x_Axis)
        {
            if (x_Axis > -1)
                this.X_Axis = x_Axis;
        }
        public void SetYAxis(int y_Axis)
        {
            if (y_Axis > -1)
                this.Y_Axis = y_Axis;
        }
    }
}
