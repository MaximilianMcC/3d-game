using System.Collections.ObjectModel;
using Raylib_cs;

static class SceneManager
{
	public static Scene Scene;
}

abstract class Scene
{
	//? Earth
	public float Gravity = -9.81f;

	private List<GameObject> gameObjects = [];
	public IReadOnlyList<GameObject> GameObjects => gameObjects;
	public Camera Camera;

	// TODO: maybe don't do this
	public abstract void Populate();

	public void Spawn(GameObject gameObject)
	{
		gameObjects.Add(gameObject);
		gameObject.Start();
	}

	public void Start()
	{
		// Make a default camera for if we forget one
		Camera = new Camera();

		// Spawn everything into the world
		Populate();
	}

	public void Update()
	{
		for (int i = GameObjects.Count - 1; i >= 0 ; i--)
		{
			GameObjects[i].Update();
		}
	}

	public void Render()
	{
		Camera.Begin();
		for (int i = 0; i < GameObjects.Count; i++)
		{
			GameObjects[i].Draw();
		}
		Camera.End();
	}

	public void CleanUp()
	{
		foreach (GameObject gameObject in GameObjects)
		{
			gameObject.CleanUp();
		}
	}

	public T Get<T>() => GameObjects.OfType<T>().FirstOrDefault();
	public List<T> GetAll<T>() => GameObjects.OfType<T>().ToList();
}