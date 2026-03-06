using System.Numerics;
using Raylib_cs;

abstract class Collider : Component
{
	// TODO: Make these internal
	public bool CollidingWithSomething { get; set; } = false;
	public GameObject ThingBeingCollidedWith { get; set; } = null;

	public bool Enabled = true;

	public override void Start()
	{
		// Register ourself
		CollisionHandler.Colliders.Add(this);
	}

	public abstract bool IsCollidingWith(Collider other);
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

class BoxCollider : Collider
{
	public Vector3 Size;

	public BoxCollider(Vector3 size) => Size = size;

	public BoundingBox AsRaylibBoundingBox => new BoundingBox(
		Position - (Size / 2f),
		Position + (Size / 2f)
	);

	public override bool IsCollidingWith(Collider other)
	{
		// Check for what the other thing is
		if (other is BoxCollider box) return Collisions.BoxBox(this, box);
		if (other is SphereCollider sphere) return Collisions.BoxSphere(this, sphere);

		return false;
	}

	public override void DrawHitbox()
	{
		Raylib.DrawBoundingBox(AsRaylibBoundingBox, (CollidingWithSomething ? Color.Red : Color.Lime));
	}
}

class SphereCollider : Collider
{
	public float Radius;

	public SphereCollider(float radius) => Radius = radius;

	public override bool IsCollidingWith(Collider other)
	{
		// Check for what the other thing is
		if (other is SphereCollider sphere) return Collisions.SphereSphere(this, sphere);
		if (other is BoxCollider box) return Collisions.BoxSphere(box, this);

		return false;
	}

	public override void DrawHitbox()
	{
		Raylib.DrawSphereWires(Position, Radius, 8, 8, (CollidingWithSomething ? Color.Red : Color.Lime));
	}
}

static class Collisions
{
	// TODO: Write manually
	public static bool SphereSphere(SphereCollider sphere1, SphereCollider sphere2)
	{
		return Raylib.CheckCollisionSpheres(
			sphere1.gameObject.Position,
			sphere1.Radius,
			sphere2.gameObject.Position,
			sphere2.Radius
		);
	}

	// TODO: Write manually
	public static bool BoxBox(BoxCollider box1, BoxCollider box2)
	{
		return Raylib.CheckCollisionBoxes(
			box1.AsRaylibBoundingBox,
			box2.AsRaylibBoundingBox
		);
	}

	// TODO: Write manually
	public static bool BoxSphere(BoxCollider box, SphereCollider sphere)
	{
		return Raylib.CheckCollisionBoxSphere(
			box.AsRaylibBoundingBox,
			sphere.gameObject.Position,
			sphere.Radius
		);
	}
}

static class CollisionHandler
{
	public static List<Collider> Colliders = [];

	public static void Update()
	{
		foreach (var collision in Colliders)
		{
			collision.CollidingWithSomething = false;
			collision.ThingBeingCollidedWith = null;
		}

		// Loop over every collider with every over collider
		for (int i = 0; i < Colliders.Count; i++)
		{
			for (int j = i + 1; j < Colliders.Count; j++)
			{
				// Check for collision
				Collider a = Colliders[i];
				Collider b = Colliders[j];

				// Check for if we are doing collision
				if ((a.Enabled || b.Enabled) == false) continue;

				if (a.IsCollidingWith(b))
				{
					a.CollidingWithSomething = true;
					b.CollidingWithSomething = true;

					a.ThingBeingCollidedWith = b.gameObject;
					b.ThingBeingCollidedWith = a.gameObject;
				}
			}
		}
	}
}