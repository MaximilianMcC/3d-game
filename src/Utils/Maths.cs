using System.Numerics;

static class Maths
{
	public static float Lerp(float start, float end, float progress)
	{
		return start + (end - start) * progress;
	}

	public static bool MovingDownwards(Vector2 velocity) => velocity.Y > 0;
	public static bool MovingUpwards(Vector2 velocity) => velocity.Y < 0;
	public static bool MovingLeft(Vector2 velocity) => velocity.X < 0;
	public static bool MovingRight(Vector2 velocity) => velocity.X > 0;
}