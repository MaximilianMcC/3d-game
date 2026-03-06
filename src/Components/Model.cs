using System.Numerics;
using Raylib_cs;

class ModelRenderer : Component
{
	private Model model;

	public override void Start()
	{
		// Load the model
		model = AssetManager.LoadGlbModel("./assets/grenade.glb");
	}

	public override void Draw()
	{
		Matrix4x4 rotation = Matrix4x4.CreateFromQuaternion(Rotation.AsQuaternion);
		model.Transform = rotation;

		Raylib.DrawModel(model, Position, 1f, Color.White);
	}

	public override void CleanUp()
	{
		Raylib.UnloadModel(model);
	}
}