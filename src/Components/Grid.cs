using Raylib_cs;

class Grid : Component
{
	public override void Draw()
	{
		Raylib.DrawGrid(10, 1f);
	}
}