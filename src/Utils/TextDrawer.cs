using System.Numerics;
using System.Runtime.CompilerServices;
using Raylib_cs;

static class TextDrawer
{
	public static float Padding { get; set; } = 10f;

	// Storing the output so we can draw it at the right time
	private static List<Text> output = [];

	public static void DrawOutput()
	{
		float y = 0;
		float x = 0;

		foreach (Text text in output)
		{
			// Draw the text
			Raylib.DrawText(text.Contents, (int)(Padding + x), (int)(Padding + y), text.FontSize, text.Color);
			x += Raylib.MeasureText(text.Contents, text.FontSize);

			// Move down
			if (text.NewLine)
			{
				y += ((text.Contents.Split("\n").Length) * text.FontSize) + Padding;
				x = 0;
			}
		}
		
		// Clear the text for next frame
		output.Clear();
	}

	public static void DrawGap() => DrawLine("");
	public static void DrawLine(object text) => DrawLine(text, 30, Color.White);
	public static void DrawLine(object text, Color color) => DrawLine(text, 30, color);
	public static void DrawLine(object text, int fontSize) => DrawLine(text, fontSize, Color.White);
	public static void DrawLine(object text, int fontSize, Color color)
	{
		// Color bools to be green/red
		if (text is bool boolean) color = boolean ? Color.Green : Color.Red;

		// Remember what we've gotta draw
		output.Add(new Text
		{
			Contents = text.ToString(),
			FontSize = fontSize,
			Color = color,
			NewLine = true
		});
	}

	public static void Draw(object text) => Draw(text, 30, Color.White);
	public static void Draw(object text, Color color) => Draw(text, 30, color);
	public static void Draw(object text, int fontSize) => Draw(text, fontSize, Color.White);
	public static void Draw(object text, int fontSize, Color color)
	{
		// Remember what we've gotta draw
		output.Add(new Text
		{
			Contents = text.ToString(),
			FontSize = fontSize,
			Color = color,
			NewLine = false
		});
	}

	public static void DrawValue<T>(T value, [CallerArgumentExpression("value")] string label = "")
	{
		// Capitalise the first letter of the label
		// TODO: Convert pascal and camel stuff to have spaces
		label = label[0].ToString().ToUpper() + label.Substring(1);

		// Draw the label
		Draw($"{label}: ", Color.SkyBlue);

		if (value is Vector3 vector3) DrawVector(vector3);
		else if (value is Vector2 vector2) DrawVector(vector2);
		else if (value is Boolean boolean) DrawBoolean(boolean);
		else Draw(value, Color.White);
		DrawGap();
	}

	private static void DrawVector(Vector3 vector)
	{
		Draw("<", Color.White);
		Draw(vector.X.ToString("0.####"), Color.Magenta);
		Draw(", ", Color.White);
		Draw(vector.Y.ToString("0.####"), Color.Red);
		Draw(", ", Color.White);
		Draw(vector.Z.ToString("0.####"), Color.Yellow);
		Draw(">", Color.White);
	}

	private static void DrawVector(Vector2 vector)
	{
		Draw("<", Color.White);
		Draw(vector.X.ToString("0.####"), Color.Magenta);
		Draw(", ", Color.White);
		Draw(vector.Y.ToString("0.####"), Color.Yellow);
		Draw(">", Color.White);
	}

	private static void DrawBoolean(bool boolean)
	{
		Draw(boolean.ToString(), boolean ? Color.Green : Color.Red);
	}

	public static void DrawFps()
	{
		int fps = Raylib.GetFPS();
		Color color = Color.Lime;
		if (fps < 60) color = Color.Red;
		if (fps < 250) color = Color.Orange;

		Draw($"FPS: ", Color.SkyBlue);
		Draw(fps, color);
		DrawGap();
	}

	struct Text
	{
		public string Contents;
		public int FontSize;
		public Color Color;
		public bool NewLine;
	}
}
