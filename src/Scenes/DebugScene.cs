using System.Numerics;

class DebugScene : Scene
{
	public override void Populate()
	{
		GameObject player = new GameObject();
		player.AddComponent(new PlayerMovement());
		GameObjects.Add(player);

		GameObject grid = new GameObject();
		grid.AddComponent(new Grid());
		GameObjects.Add(grid);
	}
}