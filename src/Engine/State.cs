using System.Numerics;
using Raylib_cs;

static class State
{
	public static bool ShowHitboxes;
	private static bool debug;
	public static bool Debug
	{
		get => debug;
		set
		{
			debug = value;
			UpdateDebugWindowTitle();
		}
	}


	public static string WindowTitle { get; set; }

	public static float DeltaTime { get; private set; }

	public static void Init(string windowTitle, Vector2 size)
	{
		WindowTitle = windowTitle;
		Graphics.GameSize = size;

		Raylib.SetConfigFlags(ConfigFlags.ResizableWindow | ConfigFlags.AlwaysRunWindow);
		Raylib.SetTraceLogLevel(TraceLogLevel.Warning);
		Raylib.InitWindow((int)Graphics.GameSize.X, (int)Graphics.GameSize.Y, WindowTitle);
		Raylib.SetExitKey(KeyboardKey.Null);
	}

	public static void Update()
	{
		// We keep the same delta time for the entire frame
		DeltaTime = Raylib.GetFrameTime();

		// Check for if we toggle debug mode
		if (Raylib.IsKeyPressed(KeyboardKey.Grave) || Raylib.IsKeyPressed(KeyboardKey.F3))
		{
			// Toggle debug mode
			Debug = !Debug;
		}

		if (debug)
		{
			TextDrawer.DrawFps();
			TextDrawer.DrawValue(ShowHitboxes);
		}

		// Check for if we toggle hitboxes
		Input.ToggleBooleanWhenShortcutDone(ref ShowHitboxes, KeyboardKey.F3, KeyboardKey.B);
	}

	private static void UpdateDebugWindowTitle()
	{
		// Update the window title
		const string debugPrefix = "(debug) ";
		Raylib.SetWindowTitle((Debug ? debugPrefix : "") + WindowTitle);
	}
}