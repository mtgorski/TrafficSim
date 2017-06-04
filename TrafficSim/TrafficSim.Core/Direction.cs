namespace TrafficSim.Core
{
    public enum Direction
    {
        North,
        East,
        South,
        West
    }

    public static class DirectionExtensions
    {
        public static bool IsOpposite(this Direction first, Direction otherDirection)
        {
            return (first == Direction.West && otherDirection == Direction.East)
                   || (first == Direction.East && otherDirection == Direction.West)
                   || (first == Direction.South && otherDirection == Direction.North)
                   || (first == Direction.North && otherDirection == Direction.South);
        }
    }
}