namespace FlightControl.Models
{
    public class Point : IEquatable<Point>
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int R { get; set; }

        public bool Equals(Point? other)
        {
            return X == other?.X && Y == other?.Y;
        }

        public void Set(int x, int y,int r)
        {
            X = x;
            Y = y;
            R = r;
        }

        public void Set(Point target)
        {
            Set(target.X, target.Y, target.R);
        }

        public async Task Step(Point target, int speed, int delay)
        {
            while (!Equals(target))
            {
                if (X != target.X) X = SetStep(X, target.X, speed);
                if (Y != target.Y) Y = SetStep(Y, target.Y, speed);
                await Task.Delay(delay);
            }
        }

        private static int SetStep(int value, int targetValue, int speed)
        {
            int delta = value - targetValue;
            if (Math.Abs(delta) < speed) return targetValue;
            else if (delta < 0) return value + speed;
            else return value - speed;
        }
    }
}

