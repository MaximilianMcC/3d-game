using System.Numerics;
using Raylib_cs;

class Grid : Component
{
	private Vector3 size = new Vector3(10, 1, 10);

	public override void Start()
	{
		gameObject.AddComponent(new BoxCollider(size));
	}

	public override void Update()
	{
		
	}

	public override void Draw()
	{
		Raylib.DrawPlane(Position, Maths.Vector3ToVector2(size), Color.White);		
	}
}