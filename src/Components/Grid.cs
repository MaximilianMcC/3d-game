using System.Numerics;
using Raylib_cs;

class Grid : Component
{
	private Vector3 size = new Vector3(10, 1, 10);
	private Mesh plane;
	private Material material;

	public override void Start()
	{
		// gameObject.AddComponent(new BoxCollider(size));

		plane = Raylib.GenMeshPlane(10, 10, 1, 1);

		material = Raylib.LoadMaterialDefault();
		Raylib.SetMaterialTexture(ref material, MaterialMapIndex.Albedo, AssetManager.LoadTexture("./assets/test.png"));
	}

	public override void Update()
	{
		
	}

	public override void Draw()
	{
		Raylib.DrawMesh(plane, material, Matrix4x4.Identity);
	}
}