using listEnum;

namespace EdgeProperty
{
    internal class Edge
    {
        private double xCoordinate;
        private double yCoordinate;
        private Color edgeColor;

        public Edge(double xCoordinate = 0, double yCoordinate = 0)
        {
            this.xCoordinate = xCoordinate;
            this.yCoordinate = yCoordinate;
        }

        public double XCoordinate
        {
            get { return xCoordinate; }
            set { xCoordinate = value; }
        }

        public double YCoordinate
        {
            get { return yCoordinate; }
            set { yCoordinate = value; }
        }

        public Color EdgeColor
        {
            get { return edgeColor; }   
            set { edgeColor = value; }
        }

    }
}
