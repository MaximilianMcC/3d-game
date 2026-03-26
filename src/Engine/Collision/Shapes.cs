using System.Numerics;
using Raylib_cs;

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

	public override CollisionInfo GetCollisionInfoWith(Collider other)
	{
		// Check for other boxes
		if (other is BoxCollider box)
		{
			// Check for if we're not colliding
			// bool colliding = Collisions.BoxSphere(this, sphere);

			Vector3 aMin = Position - Size / 2f;
			Vector3 aMax = Position + Size / 2f;
			Vector3 bMin = box.Position - box.Size / 2f;
			Vector3 bMax = box.Position + box.Size / 2f;

			float overlapX = MathF.Min(aMax.X, bMax.X) - MathF.Max(aMin.X, bMin.X);
			float overlapY = MathF.Min(aMax.Y, bMax.Y) - MathF.Max(aMin.Y, bMin.Y);
			float overlapZ = MathF.Min(aMax.Z, bMax.Z) - MathF.Max(aMin.Z, bMin.Z);

			// Check for if we're not colliding
			if (overlapX <= 0 || overlapY <= 0 || overlapZ <= 0) return CollisionInfo.NoCollision;

			// Figure out how much we've collided
			Vector3 delta = Position - box.Position;
			if (overlapX < overlapY && overlapX < overlapZ)
			{
				// X collision
				return new CollisionInfo
				{
					Colliding = true,
					Normal = new Vector3(MathF.Sign(delta.X), 0, 0),
					Depth = overlapX
				};
			}
			else if (overlapY < overlapZ)
			{
				// Y collision
				return new CollisionInfo
				{
					Colliding = true,
					Normal = new Vector3(0, MathF.Sign(delta.Y), 0),
					Depth = overlapY
				};
			}
			else
			{
				// Z collision
				return new CollisionInfo
				{
					Colliding = true,
					Normal = new Vector3(0, 0, MathF.Sign(delta.Z)),
					Depth = overlapZ
				};
			}
		}

		// Check for if we've got a sphere
		if (other is SphereCollider sphere)
		{
			// Check for if the X, Y, or Z is closer to the sphere
			Vector3 closest = Vector3.Clamp(
				sphere.gameObject.Position,
				Position - Size / 2f,
				Position + Size / 2f
			);
			Vector3 delta = sphere.gameObject.Position - closest;
			float distance = delta.Length();

			// Check for if we didn't collide
			if (distance >= sphere.Radius) return CollisionInfo.NoCollision;

			// We did collide
			return new CollisionInfo()
			{
				Colliding = true,
				Normal = distance > 0.0001f ? -Vector3.Normalize(delta) : Vector3.UnitY,
				Depth = sphere.Radius - distance
			};
		}

		// No collision
		return CollisionInfo.NoCollision;
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

	public override CollisionInfo GetCollisionInfoWith(Collider other)
	{
		// Check for other spheres
		if (other is SphereCollider sphere)
		{
			// Get how 'deep' the collision penetrated
			float distance = Vector3.Distance(Position, sphere.Position);

			float radiiSum = Radius + sphere.Radius;

			// Check for if we've not collided
			if (distance >= radiiSum) return CollisionInfo.NoCollision;

			// We have collided. Get the angle, and by how much
			return new CollisionInfo()
			{
				Colliding = true,
				Depth = distance,

				// If we're pretty much bang on then return the opposite (Y)
				Normal = (distance > 0.0001f) ? Vector3.Normalize(Position - sphere.Position) : Vector3.UnitY
			};
		}

		// Check for boxes
		if (other is BoxCollider box)
		{
			// Reverse the normal from a sphere vs a box since it's the same check
			CollisionInfo collisionInformation = box.GetCollisionInfoWith(this);
			collisionInformation.Normal *= 1f;
			return collisionInformation;
		}

		// Didn't collide with anything
		return CollisionInfo.NoCollision;
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