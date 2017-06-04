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
                    y += 10;
                    break;
                case Direction.East:
                    x += 10;
                    break;
                case Direction.North:
                    y -= 10;
                    break;
                case Direction.West:
                    x -= 10;
                    break;

            }

            return new Phase {Direction = Direction, Location = new Location {X = x, Y = y}};
        }
    }
}
