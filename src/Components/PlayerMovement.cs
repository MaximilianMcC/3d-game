using System.Numerics;
using Raylib_cs;

class PlayerMovement : Component
{
	private float MouseSensitivity = 30f;

	public override void Start()
	{
		Input.LockAndHideCursor();

		SceneManager.Scene.Camera.Position = new Vector3(0, 1.8f, 0);
	}

	public override void Update()
	{
		LookAround();
	}

	public void LookAround()
	{
		Vector2 movement = Input.GetDampenedMouseDelta() * MouseSensitivity;
		Rotation newRotation = gameObject.Rotation;

		// Look side to side
		// TODO: % 365 or whatever
		newRotation.Yaw += -movement.X;

		// Look up and down
		newRotation.Pitch += -movement.Y;
		newRotation.Pitch = Math.Clamp(
			gameObject.Rotation.Pitch,
			-30f,
			30f
		);

		// Use the new rotation
		gameObject.Rotation = newRotation;
		SceneCamera.Rotation = Rotation;
	}
}