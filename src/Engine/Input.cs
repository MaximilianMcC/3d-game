using System.Numerics;
using Raylib_cs;

class Input
{
	// Side to side stuff
	public static Vector2 Get1DMovement()
	{
		Vector2 movement = Vector2.Zero;
		if (Raylib.IsKeyDown(KeyboardKey.A) || Raylib.IsKeyDown(KeyboardKey.Left)) movement.X--;
		if (Raylib.IsKeyDown(KeyboardKey.D) || Raylib.IsKeyDown(KeyboardKey.Right)) movement.X++;
		return movement;
	}

	// WASD
	// TODO: Make vector3
	public static Vector2 Get2DMovement()
	{
		Vector2 movement = Vector2.Zero;
		if (Raylib.IsKeyDown(KeyboardKey.A) || Raylib.IsKeyDown(KeyboardKey.Left)) movement.X--;
		if (Raylib.IsKeyDown(KeyboardKey.D) || Raylib.IsKeyDown(KeyboardKey.Right)) movement.X++;
		if (Raylib.IsKeyDown(KeyboardKey.W) || Raylib.IsKeyDown(KeyboardKey.Up)) movement.Y--;
		if (Raylib.IsKeyDown(KeyboardKey.S) || Raylib.IsKeyDown(KeyboardKey.Down)) movement.Y++;
		return movement;
	}

	public static bool Jumping() => Raylib.IsKeyDown(KeyboardKey.Space) || Raylib.IsKeyDown(KeyboardKey.Up);
	public static bool Debug() => Raylib.IsKeyPressed(KeyboardKey.F3) || Raylib.IsKeyPressed(KeyboardKey.Grave);

	public static Vector2 GetDampenedMouseDelta()
	{
		//? Converts it to something a little more usable (sensitivity for the sensitivity)
		const float mouseDampening = 0.002f;
		return Raylib.GetMouseDelta() * mouseDampening;
	}

	public static void LockAndHideCursor()
	{
		Raylib.DisableCursor();

		Vector2 centre = Graphics.WindowSize / 2f;
		Raylib.SetMousePosition((int)centre.X, (int)centre.Y);
	}

	public static void UnlockAndShowCursor()
	{
		// Raylib.EnableCursor();
		Raylib.ShowCursor();
	}
}