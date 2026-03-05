using System.Numerics;
using Raylib_cs;

class PlayerMovement : Component
{
	private float MouseSensitivity = 30f;
	private float Speed = 50f;

	private float eyeHeight = 1.7f;

	public bool Freecam = false;

	public override void Start()
	{
		Input.LockAndHideCursor();
	}

	public override void Update()
	{
		LookAround();
		MoveAround();

		// If we're debugging and press f move into freecam
		if (State.Debug)
		{
			Input.ToggleBooleanWhenKeyPressed(ref Freecam, KeyboardKey.F);
			
			TextDrawer.DrawLine(Freecam);
		}
	}

	private void LookAround()
	{
		Vector2 movement = Input.GetDampenedMouseDelta() * MouseSensitivity;
		Rotation newRotation = gameObject.Rotation;

		// Look side to side
		// TODO: % 365 or whatever
		newRotation.Yaw += -movement.X;

		// Get the max pitch depending on if we are in freecam or not
		float pitchLimit = Freecam ? 90 : 30;

		// Look up and down
		newRotation.Pitch = Math.Clamp(
			newRotation.Pitch += -movement.Y,
			-pitchLimit,
			pitchLimit
		);

		// Use the new rotation
		gameObject.Rotation = newRotation;
		SceneCamera.Rotation = Rotation;
	}

	private void MoveAround()
	{
		Vector3 newPosition = gameObject.Position;
		Vector3 movementInput = Input.Get3DMovement();

		// If we're not in freecam then ignore the Y
		Vector3 freecamThing = new Vector3(1f, (Freecam ? 1 : 0), 1f);

		// Move in the direction of the camera
		newPosition += (Rotation.Forward * freecamThing) * movementInput.Z * Speed * State.DeltaTime;
		newPosition += (Rotation.Right * freecamThing) * movementInput.X * Speed * State.DeltaTime;

		// Set our position
		gameObject.Position = newPosition;

		// Set our eye height then set the cameras position
		newPosition.Y = Position.Y + eyeHeight;
		SceneCamera.Position = newPosition;
	}
}