using System.Numerics;

class DebugScene : Scene
{
	public override void Populate()
	{
		Spawn(new GameObject(
			new PlayerMovement(),
			new WeaponHandler()
		));

		// Floor
		Spawn(new GameObject(
			new Grid()
		));

	}
}