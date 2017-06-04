namespace TrafficSim.Core
{
    public struct Phase
    {
        public Location Location { get; set; }
        public Direction Direction { get; set; }

        public Phase GetPhasedAdvancedBy(int magnitude)
        {
            var x = Location.X;
            var y = Location.Y;
            switch (Direction)
            {
                case Direction.South:
                    y += magnitude;
                    break;
                case Direction.East:
                    x += magnitude;
                    break;
                case Direction.North:
                    y -= magnitude;
                    break;
                case Direction.West:
                    x -= magnitude;
                    break;

            }

            return new Phase {Direction = Direction, Location = new Location {X = x, Y = y}};
        }
    }
}
