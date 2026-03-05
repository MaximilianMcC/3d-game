using System.Numerics;
using Raylib_cs;

static class Graphics
{
	public static Vector2 WindowSize => new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());

	public static Rectangle GetTextureRectangle(Texture2D texture)
	{
		return new Rectangle(0, 0, texture.Dimensions);
	}

	public static void DrawTexture(Texture2D texture, Vector2 position, Vector2 size)
	{
		Raylib.DrawTexturePro(
			texture,
			GetTextureRectangle(texture),
			new Rectangle(position, size),
			Vector2.Zero,
			0f,
			Color.White
		);
	}

	public static void DrawTexture(Texture2D texture, Rectangle rectangle, bool flippedX = false)
	{
		Rectangle source = GetTextureRectangle(texture);
		if (flippedX)
		{
			source.X += source.Width;
			source.Width *= -1;
		}

		Raylib.DrawTexturePro(
			texture,
			source,
			rectangle,
			Vector2.Zero,
			0f,
			Color.White
		);
	}

	public static void DrawRotatedTextureFromCentre(Texture2D texture, Rectangle rectangle, float rotation)
	{
		Raylib.DrawTexturePro(
			texture,
			GetTextureRectangle(texture),
			rectangle,
			rectangle.Size / 2f,
			rotation,
			Color.White
		);
	}

	public static void DrawRenderTextureOverWholeScreen(RenderTexture2D renderTexture)
	{
		Raylib.DrawTexturePro(
			renderTexture.Texture,
			new Rectangle(0, 0, renderTexture.Texture.Dimensions * new Vector2(1, -1)),
			new Rectangle(0, 0, WindowSize),
			Vector2.Zero,
			0f,
			Color.White
		);
	}

	public static void DrawTextureOverWholeScreen(Texture2D texture)
	{
		Raylib.DrawTexturePro(
			texture,
			GetTextureRectangle(texture),
			new Rectangle(0, 0, WindowSize),
			Vector2.Zero,
			0f,
			Color.White
		);
	}

	public static void DrawCentreText(string text, float fontSize)
	{
		Vector2 size = Raylib.MeasureTextEx(Raylib.GetFontDefault(), text, fontSize, fontSize / 10);

		Raylib.DrawTextPro(
			Raylib.GetFontDefault(),
			text,
			Raylib.GetScreenCenter(),
			size * 0.5f,
			0f,
			fontSize,
			fontSize / 10,
			Color.White
		);
	}
}