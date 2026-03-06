using Raylib_cs;

class WeaponHandler : Component
{
	public override void Update()
	{
		// Check for if we wanna throw a grenade
		if (Raylib.IsMouseButtonPressed(MouseButton.Left))
		{
			// Create a new grenade
			GameObject grenade = new GameObject();
			grenade.AddComponent(new ModelRenderer());
			Grenade grenadeGrenade = new Grenade();
			grenade.AddComponent(grenadeGrenade);
			grenadeGrenade.Spawn(SceneCamera.Position, SceneCamera.Rotation, 20f);

			// Spawn the grenade
			SceneManager.Scene.Spawn(grenade);
			Console.WriteLine("Spawned grenade");
		}
	}
}