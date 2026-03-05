using System.Collections.ObjectModel;
using Raylib_cs;

static class SceneManager
{
	public static Scene Scene;
}

abstract class Scene
{
	public List<GameObject> GameObjects = [];
	public Camera Camera;

	// TODO: maybe don't do this
	public abstract void Populate();

	public void Start()
	{
		// Make a default camera for if we forget one
		Camera = new Camera();

		Populate();

		for (int i = 0; i < GameObjects.Count; i++)
		{
			GameObjects[i].Start();
		}
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