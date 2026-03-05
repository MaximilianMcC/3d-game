class GameObject
{
	private List<Component> components = [];
	public IReadOnlyList<Component> Components => components;

	public void AddComponent(Component component)
	{
		// Set our parent
		component.gameObject = this;
		components.Add(component);
	}

	public void Start()
	{
		for (int i = 0; i < Components.Count; i++)
		{
			Components[i].Start();
		}
	}

	public void Update()
	{
		for (int i = 0; i < Components.Count; i++)
		{
			Components[i].Update();
		}
	}

	public void Draw()
	{
		for (int i = 0; i < Components.Count; i++)
		{
			Components[i].Draw();
		}
	}

	public void CleanUp()
	{
		for (int i = 0; i < Components.Count; i++)
		{
			Components[i].CleanUp();
		}
	}
}

class Component
{
	public GameObject gameObject;

	public virtual void Start() { }
	public virtual void Update() { }
	public virtual void Draw() { }
	public virtual void CleanUp() { }
}