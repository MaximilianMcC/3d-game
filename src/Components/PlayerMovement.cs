using System.Numerics;
using Raylib_cs;

class PlayerMovement : Component
{
	private float mouseSensitivity = 30f;

	private float velocity;
	private float maxSpeed = 15f;
	private float acceleration = 120f;
	private float deceleration = 60f;
	private Vector3 movementDirection;
	// TODO: ^^^ Put this somewhere else

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
			
			TextDrawer.DrawValue(Freecam);
		}
	}

	private void LookAround()
	{
		Vector2 movement = Input.GetDampenedMouseDelta() * mouseSensitivity;
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

	// TODO: friction-based
	private void CalculateSpeed(bool movingRn)
	{
		// If we're moving then accelerate, otherwise decelerate
		velocity += (movingRn ? acceleration : -deceleration) * State.DeltaTime;

		// Ensure we do not decelerate 'backwards'
		velocity = Maths.ClampAtZero(velocity);

		// Ensure we do not go too fast
		if (velocity > maxSpeed) velocity = maxSpeed;

		TextDrawer.DrawValue(velocity);
	}

	private void MoveAround()
	{
		Vector3 newPosition = gameObject.Position;

		Vector3 movementInput = Input.Get3DMovement();
		bool moving = Maths.IsAnyComponentNotZero(movementInput);

		// If we're moving then convert the movement
		// to be in the direction of the camera
		if (moving)
		{
			movementDirection = (Rotation.Forward * movementInput.Z) + (Rotation.Right * movementInput.X);
			movementDirection = Maths.NormaliseIfNotZero(movementDirection);

			// Check for if we're using freecam or not (ignore Y)
			Vector3 freecamThing = new Vector3(1f, (Freecam ? 1 : 0), 1f);
			movementDirection *= freecamThing;
		}

		// Get our speed
		CalculateSpeed(moving);

		// Move
		newPosition += movementDirection * velocity * State.DeltaTime;

		// Set our position
		gameObject.Position = newPosition;

		// Set our eye height then set the cameras position
		newPosition.Y = Position.Y + eyeHeight;
		SceneCamera.Position = newPosition;

		TextDrawer.DrawValue(Position);
		TextDrawer.DrawValue(movementDirection);
	}
}