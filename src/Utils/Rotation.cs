using System.Numerics;

struct Rotation
{
	// Values in degrees
	public float Yaw;
	public float Pitch;
	public float Roll;

	public Vector3 AsVector => new Vector3(Yaw, Pitch, Roll);
	public Quaternion AsQuaternion => Quaternion.CreateFromYawPitchRoll(
		Maths.DegreesToRadians(Yaw),
		Maths.DegreesToRadians(Pitch),
		Maths.DegreesToRadians(Roll)
	);

	public Vector3 Forward => Vector3.Transform(-Vector3.UnitZ, AsQuaternion);
	public Vector3 Right => Vector3.Transform(Vector3.UnitX, AsQuaternion);
	public Vector3 Up => Vector3.Transform(Vector3.UnitY, AsQuaternion);





	public Rotation(float yaw, float pitch, float roll)
	{
		Yaw = yaw;
		Pitch = pitch;
		Roll = roll;
	}

	// I did not write this, and I'm not gonna pretend I wrote this or understand it
	public Rotation(Quaternion quaternion)
	{
		float sinPitch = 2f * (quaternion.W * quaternion.X - quaternion.Z * quaternion.Y);
		Pitch = Maths.RadiansToDegrees(MathF.Asin(sinPitch));

		float sinYaw = 2f * (quaternion.W * quaternion.Y + quaternion.X * quaternion.Z);
		float cosYaw = 1f - 2f * (quaternion.X * quaternion.X + quaternion.Y * quaternion.Y);
		Yaw = Maths.RadiansToDegrees(MathF.Atan2(sinYaw, cosYaw));

		float sinRoll = 2f * (quaternion.W * quaternion.Z + quaternion.X * quaternion.Y);
		float cosRoll = 1f - 2f * (quaternion.X * quaternion.X + quaternion.Z * quaternion.Z);
		Roll = Maths.RadiansToDegrees(MathF.Atan2(sinRoll, cosRoll));
	}
}