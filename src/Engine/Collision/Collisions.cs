using System.Numerics;
using Raylib_cs;

abstract class Collider : Component
{
	// TODO: Make these internal
	// TODO: Either remove CollidingWithSomething or remove CollisionInformation.Colliding
	public bool CollidingWithSomething { get; set; } = false;
	public GameObject ThingBeingCollidedWith { get; set; } = null;
	public CollisionInfo CollisionInfo;

	public bool Enabled = true;

	public override void Start()
	{
		// Register ourself
		CollisionHandler.Colliders.Add(this);
	}

	public abstract bool IsCollidingWith(Collider other);
	// TODO: Make this:
	// public abstract bool CollisionWith(Collider other, out CollisionInfo);
	public abstract CollisionInfo GetCollisionInfoWith(Collider other);
	public abstract void DrawHitbox();

	public override void Draw()
	{
		if (State.ShowHitboxes == false) return;
		DrawHitbox();
	}

	public override void CleanUp()
	{
		// Remove ourself from the collision list thingy
		CollisionHandler.Colliders.Remove(this);
	}
}

static class CollisionHandler
{
	public static List<Collider> Colliders = [];

	public static void Update()
	{
		// Reset everythings info
		foreach (var collision in Colliders)
		{
			collision.CollidingWithSomething = false;
			collision.ThingBeingCollidedWith = null;
			collision.CollisionInfo = default;
		}

		// Loop over every collider with every over collider
		for (int i = 0; i < Colliders.Count; i++)
		{
			for (int j = i + 1; j < Colliders.Count; j++)
			{
				// Get the target/victim
				Collider a = Colliders[i];
				Collider b = Colliders[j];

				// Check for if we are doing collision
				if ((a.Enabled && b.Enabled) == false) continue;

				// Check for collision
				CollisionInfo collision = a.GetCollisionInfoWith(b);
				if (collision.Colliding)
				{
					// Say we've been collided with
					a.CollidingWithSomething = true;
					b.CollidingWithSomething = true;

					// Say what we've collided with
					a.ThingBeingCollidedWith = b.gameObject;
					b.ThingBeingCollidedWith = a.gameObject;

					// Update the collision info
					a.CollisionInfo = collision;
					b.CollisionInfo = new CollisionInfo(-collision.Normal, collision.Depth);
				}
			}
		}
	}
}

struct CollisionInfo
{
	public bool Colliding = false;
	public Vector3 Normal = Vector3.Zero;
	public float Depth = 0f;

	public CollisionInfo(Vector3 normal, float depth)
	{
		Colliding = true;
		Normal = normal;
		Depth = depth;
	}

	public static CollisionInfo NoCollision => new CollisionInfo() { Colliding = false };
}