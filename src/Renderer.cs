using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace Microsoft.Xna.Framework.Graphics
{
	public class Renderer
	{
		private readonly GraphicsDeviceManager _graphicsDeviceManager;
		private SpriteBatch _spriteBatch;
		private Texture2D _backbone;
		private readonly GameWindow _window;
		public RenderTarget2D RenderTarget { get; set; }

		public Renderer(Game game)
        {
            _graphicsDeviceManager = new GraphicsDeviceManager(game);
			_window = game.Window;
			RenderTarget = new RenderTarget2D(_graphicsDeviceManager.GraphicsDevice, 1024, 1024);
		}

		public void DrawTexture(Texture2D tex, Rectangle drawRectangle, Rectangle? sourceRectangle = null, bool flipped = false, float alpha = 1f)
		{
			DrawTexture(tex, Color.White, drawRectangle, sourceRectangle, flipped, alpha);
		}

		public void DrawTexture(Texture2D tex, Color color, Rectangle drawRectangle, Rectangle? sourceRectangle = null, bool flipped = false, float alpha = 1f)
		{
			SpriteEffects se = flipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
			Rectangle srcRect;
			if (sourceRectangle == null)
			{
				srcRect = new Rectangle(0, 0, tex.Width, tex.Height);
			}
			else
			{
				srcRect = (Rectangle)sourceRectangle;
			}
			_spriteBatch.Draw(tex, drawRectangle, srcRect, new Color(color, alpha), 0f, Vector2.Zero, se, 0f);
		}

		public void DrawRectangle(Rectangle rectangle, Color color)
		{
			_spriteBatch.Draw(_backbone, rectangle, new Rectangle(0, 0, _backbone.Width, _backbone.Height), color, 0f, Vector2.Zero, SpriteEffects.None, 0f);
		}

		// Credit: http://stackoverflow.com/a/16407171/827326
		public void DrawLine(Vector2 a, Vector2 b, Color? color = null)
		{
			Color c = color ?? Color.Black;

			Rectangle r = new Rectangle((int)a.X, (int)a.Y, (int)(a - b).Length(), 1);
			Vector2 v = Vector2.Normalize(a - b);
			float angle = (float)Math.Acos(Vector2.Dot(v, -Vector2.UnitX));
			if (a.Y > b.Y)
			{
				angle = MathHelper.TwoPi - angle;
			}

			_spriteBatch.Draw(_backbone, r, null, c, angle, Vector2.Zero, SpriteEffects.None, 0);
		}

		public TextureWithMetadata LoadTextureWithMetadata(string fileName)
		{
			Dictionary<string, string> metadata = new Dictionary<string, string>();

			if (File.Exists(fileName))
			{
				// !TODO: Make this so this stream is passed on to LoadTexture instead of us loading it twice
				using (FileStream stream = new FileStream(fileName, FileMode.Open))
				{
					using (Bitmap image = new Bitmap(stream))
					{
						foreach (PropertyItem pi in image.PropertyItems)
						{
							string stringVersion = "";
							if (pi.Len > 1)
							{
								stringVersion = Encoding.UTF8.GetString(pi.Value, 0, pi.Value.Length - 1)/*.Replace("\n","\r\n")*/;
							}
							switch (pi.Id)
							{
								case 20752:
								case 20753:
								case 20754:
									break;
								case 800:
									metadata["Title"] = stringVersion;
									break;
								case 37510:
									metadata["Comment"] = stringVersion;
									break;
								case 315:
									metadata["Author"] = stringVersion;
									break;
								case 33432:
									metadata["Copyright"] = stringVersion;
									break;
								case 270:
									metadata["Description"] = stringVersion;
									break;
								case 305:
									metadata["Software"] = stringVersion;
									break;
								case 272:
									metadata["Source"] = stringVersion;
									break;
								default:
									metadata[$"{pi.Id} of type {pi.Type}"] = Encoding.UTF8.GetString(pi.Value);
									break;
							}
						}
					}
				}
			}

			return new TextureWithMetadata(LoadTexture(fileName), metadata);
		}

		public Texture2D[] LoadTextureColors(string fileName, Color[] inputColors)
		{
			Texture2D texture = LoadTexture(fileName);

			int sizeW = texture.Width;
			int sizeH = texture.Height;
			int pixelCount = sizeW * sizeH;

			Color[] textureAsColor = new Color[pixelCount];

			texture.GetData(textureAsColor);

			Texture2D[] tx = new Texture2D[inputColors.Length];
			Color[][] newTexs = new Color[inputColors.Length][];
			for (int i = 0; i < inputColors.Length; i++)
			{
				tx[i] = new Texture2D(_graphicsDeviceManager.GraphicsDevice, sizeW, sizeH);
				newTexs[i] = new Color[pixelCount];
			}

			for (int i = 0; i < pixelCount; i++)
			{
				for (int c = 0; c < inputColors.Length; c++)
				{
					if (inputColors[c] == textureAsColor[i])
					{
						newTexs[c][i] = Color.White;
					}
					else
					{
						newTexs[c][i] = Color.Transparent;
					}
				}
			}

			for (int i = 0; i < inputColors.Length; i++)
			{
				tx[i].SetData(newTexs[i]);
			}
			
			return tx;
		}

		public Texture2D LoadTexture(string fileName)
		{
			if (File.Exists(fileName))
			{
				using (FileStream stream = new FileStream(fileName, FileMode.Open))
				{
					return Texture2D.FromStream(_graphicsDeviceManager.GraphicsDevice, stream);
				}
			}

			Color orangeLight = new Color(224, 192, 0);
			Color orangeDark = new Color(224, 160, 0);
			Color blueLight = new Color(0, 0, 255);
			Color blueDark = new Color(0, 64, 192);

			const int size = 64;

			Texture2D tx = new Texture2D(_graphicsDeviceManager.GraphicsDevice, size, size);
			Color[] colors = new Color[size * size];

			for (int i = 0; i < colors.Length; i++)
			{
				if (i % size >= (size / 2))
				{
					if (i / size >= (size / 2))
					{
						if (((i / size) % 2) == (i % 2))
						{
							colors[i] = orangeLight;
						}
						else
						{
							colors[i] = orangeDark;
						}
					}
					else
					{
						if (((i / size) % 2) == (i % 2))
						{
							colors[i] = blueLight;
						}
						else
						{
							colors[i] = blueDark;
						}
					}
				}
				else
				{
					if (i / size >= (size / 2))
					{
						if (((i / size) % 2) == (i % 2))
						{
							colors[i] = blueLight;
						}
						else
						{
							colors[i] = blueDark;
						}
					}
					else
					{
						if (((i / size) % 2) == (i % 2))
						{
							colors[i] = orangeLight;
						}
						else
						{
							colors[i] = orangeDark;
						}
					}
				}
			}

			tx.SetData(colors);

			return tx;
		}

		public void ChangeResolution(int width, int height)
		{
			_graphicsDeviceManager.PreferredBackBufferWidth = width;
			_graphicsDeviceManager.PreferredBackBufferHeight = height;
			_graphicsDeviceManager.ApplyChanges();
		}
		
		public void Begin(bool pointSample, int width, int height, bool nonPremultiplied = true)
		{
			_graphicsDeviceManager.GraphicsDevice.SetRenderTarget(RenderTarget);
			if (pointSample)
			{
				_spriteBatch.Begin(SpriteSortMode.Deferred, nonPremultiplied ? BlendState.NonPremultiplied : BlendState.AlphaBlend, SamplerState.PointClamp,
					DepthStencilState.None, RasterizerState.CullCounterClockwise);
			}
			else
			{
				_spriteBatch.Begin();
			}
		}

		public void Begin(bool pointSample, bool nonPremultiplied = true)
		{
			_graphicsDeviceManager.GraphicsDevice.SetRenderTarget(null);
			if (pointSample)
			{
				_spriteBatch.Begin(SpriteSortMode.Deferred, nonPremultiplied ? BlendState.NonPremultiplied : BlendState.AlphaBlend, SamplerState.PointClamp,
					DepthStencilState.None, RasterizerState.CullCounterClockwise);
			}
			else
			{
				_spriteBatch.Begin();
			}
		}

		public void End()
		{
			_spriteBatch.End();
		}

		public GraphicsDevice GraphicsDevice
		{
			get { return _graphicsDeviceManager.GraphicsDevice; }
		}

		public SpriteBatch SpriteBatch
		{
			get { return _spriteBatch; }
		}

		internal void Initialize()
		{
			_spriteBatch = new SpriteBatch(_graphicsDeviceManager.GraphicsDevice);
			_backbone = new Texture2D(_graphicsDeviceManager.GraphicsDevice, 1, 1);
			_backbone.SetData(new[] { Color.White });
		}

		public int ScreenWidth
		{
			get
			{
				return _window.ClientBounds.Width;
			}
		}

		public int ScreenHeight
		{
			get
			{
				return _window.ClientBounds.Height;
			}
		}

		public void Clear(Color color)
		{
			_graphicsDeviceManager.GraphicsDevice.Clear(color);
		}
	}
}
