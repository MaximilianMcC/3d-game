using System.Numerics;

class DebugScene : Scene
{
	public override void Populate()
	{
		GameObject player = new GameObject();
		// player.Components.Add(new PlayerMovement());

		Camera.Position = new Vector3(5, 1.5f, 5);
		Camera.LookAt(Vector3.Zero);

		GameObject grid = new GameObject();
		grid.AddComponent(new Grid());
		GameObjects.Add(grid);
	}
}