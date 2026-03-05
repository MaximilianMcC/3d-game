using Raylib_cs;

static class TextDrawer
{
	public static float Padding { get; set; } = 10f;

	private static float y;
	public static void BeginDrawing() => y = 0;

	public static void DrawLine(object text) => DrawLine(text, 30, Color.White);
	public static void DrawLine(object text, Color color) => DrawLine(text, 30, color);
	public static void DrawLine(object text, int fontSize) => DrawLine(text, fontSize, Color.White);
	public static void DrawLine(object text, int fontSize, Color color)
	{
		Raylib.DrawText(text.ToString(), (int)Padding, (int)(Padding + y), fontSize, color);
		y += ((text.ToString().Split("\n").Length) * fontSize) + Padding;
	}

	public static void DrawGap() => DrawLine("");
}