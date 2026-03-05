using System.Numerics;
using Raylib_cs;

static class State
{
	public static bool Debug { get; set; }
	public static string WindowTitle { get; set; }

	public static void Init(string windowTitle, Vector2 size)
	{
		WindowTitle = windowTitle;
		Graphics.GameSize = size;

		Raylib.SetConfigFlags(ConfigFlags.ResizableWindow | ConfigFlags.AlwaysRunWindow);
		Raylib.SetTraceLogLevel(TraceLogLevel.Warning);
		Raylib.InitWindow((int)Graphics.GameSize.X, (int)Graphics.GameSize.Y, WindowTitle);
	}

	public static void Update()
	{
		if (Raylib.IsKeyPressed(KeyboardKey.Grave) || Raylib.IsKeyPressed(KeyboardKey.F3))
		{
			// Toggle debug mode
			Debug = !Debug;

			// Update the window title
			const string debugPrefix = "(debug) ";
			Raylib.SetWindowTitle((Debug ? debugPrefix : "") + WindowTitle);
		}
	}
}