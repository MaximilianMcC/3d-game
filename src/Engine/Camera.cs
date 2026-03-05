using System.Numerics;
using Raylib_cs;

class Camera
{
	public Vector3 Position;
	public Rotation Rotation;
	public float Fov = 60f;

	public Vector3 Target => Position + Rotation.Forward;

	public void LookAt(Vector3 target)
	{
		// Get the difference in the two points
		Vector3 forward = Vector3.Normalize(target - Position);

		// Look at it
		Rotation = new Rotation(Quaternion.CreateFromRotationMatrix(
			Matrix4x4.CreateWorld(Vector3.Zero, forward, Vector3.UnitY)
		));
	}

	public void Begin()
	{
		// TODO: Don't make a new one each time maybe (definitely)
		Camera3D camera = new Camera3D()
		{
			Position = this.Position,
			Target = this.Target,

			Up = Vector3.UnitY,
			FovY = this.Fov,
			Projection = CameraProjection.Perspective
		};

		Raylib.BeginMode3D(camera);
	}

	public void End() => Raylib.EndMode3D();
}