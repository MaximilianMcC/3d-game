using System.Numerics;
using Raylib_cs;

class Grenade : Component
{
	private Vector3 velocity;
	private float airResistance = 0.5f;
	private Collider collider => gameObject.GetComponent<Collider>();

	public void Spawn(Vector3 spawnPosition, Rotation spawnRotation, float throwForce)
	{
		gameObject.Position = spawnPosition;
		gameObject.Rotation = spawnRotation;

		// Add a little bit of an upwards boost
		gameObject.Rotation.Pitch += 20f;

		velocity = Rotation.Forward * throwForce;
	}

	// Defenestrate or something
	public override void Update()
	{
		// Apply gravity
		velocity.Y += SceneManager.Scene.Gravity * State.DeltaTime;

		// Add air resistance to slow it down
		velocity -= velocity * airResistance * State.DeltaTime;

		// Apply the movement and spin it in a cool looking way
		gameObject.Position += velocity * State.DeltaTime;
		gameObject.Rotation += (new Rotation(200f, 30f, 45f) * 3) * State.DeltaTime;

		CorrectForPhysics();
	}

	private void CorrectForPhysics()
	{
		// Check for if there has been collision
		if (collider.CollidingWithSomething == false) return;
		CollisionInfo collision = collider.CollisionInfo;

		// Correct the position by pushing it 'out'
		Position += collision.Normal * collision.Depth;
		velocity = Vector3.Zero;
	}
}