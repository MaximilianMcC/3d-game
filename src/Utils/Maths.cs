using System.Numerics;

static class Maths
{
    public const float PI = 3.14159265f;

	public static float Lerp(float start, float end, float progress)
	{
		return start + (end - start) * progress;
	}

	public static bool MovingDownwards(Vector2 velocity) => velocity.Y > 0;
	public static bool MovingUpwards(Vector2 velocity) => velocity.Y < 0;
	public static bool MovingLeft(Vector2 velocity) => velocity.X < 0;
	public static bool MovingRight(Vector2 velocity) => velocity.X > 0;

	public static bool IsAnyComponentNotZero(Vector3 vector)
	{
		if (vector.X != 0) return true;
		if (vector.Y != 0) return true;
		if (vector.Z != 0) return true;
		return false;
	}

	public static float ClampAtZero(float value) => value < 0 ? 0 : value;

	public static float DegreesToRadians(float degrees) => degrees * (PI / 180f);
	public static float RadiansToDegrees(float radians) => (radians * 180f) / PI;

	public static Vector3 NormaliseIfNotZero(Vector3 vector)
	{
		if (vector == Vector3.Zero) return vector;
		return Vector3.Normalize(vector);
	}

}