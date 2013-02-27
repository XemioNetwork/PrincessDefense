using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Math;

namespace Xemio.PrincessDefense.Entities
{
    public static class DirectionHelper
    {
        #region Methods
        /// <summary>
        /// Gets the closest animation.
        /// </summary>
        /// <param name="direction">The direction.</param>
        public static Direction GetClosestFacing(Vector2 direction)
        {
            float angle = MathHelper.ToAngle(direction);
            float degrees = MathHelper.ToDegrees(angle);

            Direction result = Direction.Down;

            if (degrees >= 337.5 || degrees < 22.5) result = Direction.Left;
            if (degrees >= 22.5 && degrees < 67.5) result = Direction.LeftUp;
            if (degrees >= 67.5 && degrees < 112.5) result = Direction.Up;
            if (degrees >= 112.5 && degrees < 157.5) result = Direction.UpRight;
            if (degrees >= 157.5 && degrees < 202.5) result = Direction.Right;
            if (degrees >= 202.5 && degrees < 247.5) result = Direction.RightDown;
            if (degrees >= 247.5 && degrees < 292.5) result = Direction.Down;
            if (degrees >= 292.5 && degrees < 337.5) result = Direction.DownLeft;

            return result;
        }
        /// <summary>
        /// Gets the direction vector for a specific walkdirection.
        /// </summary>
        /// <param name="direction">The direction.</param>
        public static Vector2 GetDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Down:
                    return new Vector2(0, 1);
                case Direction.DownLeft:
                    return new Vector2(-1, 1);
                case Direction.Left:
                    return new Vector2(-1, 0);
                case Direction.LeftUp:
                    return new Vector2(-1, -1);
                case Direction.Right:
                    return new Vector2(1, 0);
                case Direction.RightDown:
                    return new Vector2(1, 1);
                case Direction.Up:
                    return new Vector2(0, -1);
                case Direction.UpRight:
                    return new Vector2(1, -1);

                default: return Vector2.Zero;
            }
        }
        /// <summary>
        /// Gets the name of the animation.
        /// </summary>
        /// <param name="direction">The direction.</param>
        public static string GetAnimationName(Direction direction)
        {
            switch (direction)
            {
                case Direction.Down:
                case Direction.DownLeft:
                    return "Down";
                case Direction.Left:
                case Direction.LeftUp:
                    return "Left";
                case Direction.Right:
                case Direction.RightDown:
                    return "Right";
                case Direction.Up:
                case Direction.UpRight:
                    return "Up";

                default: return "None";
            }
        }
        #endregion
    }
}
