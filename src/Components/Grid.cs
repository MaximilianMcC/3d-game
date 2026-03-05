using System.Numerics;
using Raylib_cs;

class Grid : Component
{
	public override void Draw()
	{
		Raylib.DrawGrid(10, 1f);

		Raylib.DrawCube(
			new Vector3(0, 4, -5),
			1f, 1f, 1f,
			Color.Magenta
		);
	}
}