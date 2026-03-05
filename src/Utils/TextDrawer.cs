using Raylib_cs;

static class TextDrawer
{
	public static float Padding { get; set; } = 10f;

	// Storing the output so we can draw it at the right time
	private static List<Text> output = [];

	public static void DrawOutput()
	{
		float y = 0;

		foreach (Text text in output)
		{
			// Draw the text
			Raylib.DrawText(text.Contents, (int)Padding, (int)(Padding + y), text.FontSize, text.Color);
			y += ((text.Contents.Split("\n").Length) * text.FontSize) + Padding;
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
			Color = color
		});
	}

	struct Text
	{
		public string Contents;
		public int FontSize;
		public Color Color;
	}
}