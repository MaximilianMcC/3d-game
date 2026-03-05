using System.Reflection;
using Raylib_cs;

class AssetManager
{
	private static readonly string Namespace = "3DGame";

	private static byte[] GetAssetBytes(string assetPath, out string extension)
	{
		// Get the current assembly and namespace
		// so we can extract the assets and whatnot
		Assembly assembly = Assembly.GetExecutingAssembly();

		// Get the path to the asset and its file extension
		assetPath = assetPath.Replace("./", "").Replace("/", ".");
		string path = $"{Namespace}.{assetPath}";
		extension = Path.GetExtension(assetPath);

		// Get the stream containing the assets data
		using (Stream stream = assembly.GetManifestResourceStream(path))
		{
			// Check for if there is a stream or not
			if (stream == null)
			{
				throw new Exception("Could not find embedded asset at " + path);
			}

			// Get the stream as a byte array
			byte[] bytes;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				// Extract the bytes
				stream.CopyTo(memoryStream);

				// Give them back
				bytes = memoryStream.ToArray();
				return bytes;
			}
		}

	}

	public static Image LoadImage(string path)
	{
		// Get the asset byte array and extension
		byte[] bytes = GetAssetBytes(path, out string extension);

		// Load the image from the byte array
		Image image = Raylib.LoadImageFromMemory(extension, bytes);

		// Give back the loaded image
		return image;
	}

	public static Texture2D LoadTexture(string path)
	{
		// Load the texture as an image
		// then convert it to a texture
		Image image = LoadImage(path);
		Texture2D texture = Raylib.LoadTextureFromImage(image);

		// Unload the image since we no longer need it
		Raylib.UnloadImage(image);

		// Give back the loaded texture
		return texture;
	}

	public static Sound LoadSound(string path)
	{
		// Get the asset byte array and extension
		byte[] bytes = GetAssetBytes(path, out string extension);

		// Load the wave data from the raw bytes then use
		// that to load the sound
		Wave wave = Raylib.LoadWaveFromMemory(extension, bytes);
		Sound sound = Raylib.LoadSoundFromWave(wave);

		// Unload the wave data because we don't need it anymore
		Raylib.UnloadWave(wave);

		// Give back the sound
		return sound;
	}

	public static Music LoadMusic(string path)
	{
		// Get the asset byte array and extension
		byte[] bytes = GetAssetBytes(path, out string extension);

		// Load the music from the data
		Music music = Raylib.LoadMusicStreamFromMemory(extension, bytes);

		// Give back the sound
		return music;
	}

	public static Font LoadFont(string path)
	{
		// Get the asset byte array and extension
		byte[] bytes = GetAssetBytes(path, out string extension);

    	// ASCII for characters are from 32 to 126
    	int[] fontChars = Enumerable.Range(32, 126).ToArray(); 

		// Load the font from the raw bytes
		Font font = Raylib.LoadFontFromMemory(extension, bytes, 32, fontChars, fontChars.Length);

		// Give back the font
		return font;
	}

	//! Debug
	// TODO: Make it so you can press a button and it will show this or something (debug)
	public static void PrintEmbeddedAssets()
	{
		// Get all of the assets that are embedded rn
		Assembly assembly = Assembly.GetExecutingAssembly();
		string[] assets = assembly.GetManifestResourceNames();
		
		// Print them all
		Console.WriteLine("All embedded assets:");
		foreach (string asset in assets) Console.WriteLine(asset);
	}
}