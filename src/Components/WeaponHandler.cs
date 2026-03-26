using System.Numerics;
using Raylib_cs;

class WeaponHandler : Component
{
	public override void Update()
	{
		// Check for if we wanna throw a grenade
		if (Raylib.IsMouseButtonPressed(MouseButton.Left))
		{
			// Make it so if the player is moving their force is added
			float throwForce = 20f;
			throwForce += GetPlayersVelocity() / 2f;

			// Create a new grenade
			GameObject grenade = new GameObject();
			grenade.AddComponent(new ModelRenderer("./assets/grenade.glb"));
			Grenade grenadeGrenade = new Grenade();
			grenade.AddComponent(grenadeGrenade);
			grenade.AddComponent(new SphereCollider(0.3f));
			grenadeGrenade.Spawn(SceneCamera.Position, SceneCamera.Rotation, throwForce);

			// Spawn the grenade
			SceneManager.Scene.Spawn(grenade);
		}
	}

	// TODO: Don't do this like this here
	private float GetPlayersVelocity()
	{
		PlayerMovement playerMovement = SceneManager.Scene.GameObjects
			.SelectMany(gameObject => gameObject.Components)
			.OfType<PlayerMovement>()
			.FirstOrDefault();

		return playerMovement.Velocity;
	}
}