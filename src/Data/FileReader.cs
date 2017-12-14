using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace Microsoft.Xna.Framework.Data
{
	/// <summary>
	/// The version of FileReader that doesn't have BE and LE mixed up (BE vs LE is named really poorly >:-C)
	/// </summary>
	public sealed class FileReader : IDisposable
	{
		private readonly Stream _s;

		public FileReader(byte[] file)
		{
			_s = new MemoryStream(file);
		}

		public FileReader(string fileName)
		{
			byte[] file = File.ReadAllBytes(fileName);
			_s = new MemoryStream(file);
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Eof")]
		public bool ReachedEof
		{
			get
			{
				return _s.Position >= _s.Length;
			}
		}

		public long CurrentPosition
		{
			get { return _s.Position; }
		}

		public string ReadCString()
		{
			StringBuilder sb = new StringBuilder();
			int c = _s.ReadByte();
			while (c != 0)
			{
				sb.Append((char)c);
				c = _s.ReadByte();
			}

			return sb.ToString();
		}

		public string ReadCString(char terminator)
		{
			StringBuilder sb = new StringBuilder();
			int c = _s.ReadByte();
			while (c != terminator)
			{
				sb.Append((char)c);
				c = _s.ReadByte();
			}

			return sb.ToString();
		}

		public string ReadCString(uint bufferSize)
		{
			StringBuilder sb = new StringBuilder();
			int i = 1;
			int c = _s.ReadByte();
			while (c != 0 && i <= bufferSize)
			{
				sb.Append((char)c);
				c = _s.ReadByte();
				i++;
			}

			_s.Seek(bufferSize - i, SeekOrigin.Current);
			return sb.ToString();
		}

		public string ReadCharArray(int length)
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < length; i++)
			{
				sb.Append((char)_s.ReadByte());
			}
			
			return sb.ToString();
		}

		public byte[] Read(int length)
		{
			byte[] ba = new byte[length];
			_s.Read(ba, 0, length);
			return ba;
		}

		public byte[] Read(uint length)
		{
			byte[] ba = new byte[length];
			if (length > int.MaxValue) // CHECKTHIS: Does this even work?
			{
				int position = 0;
				while (position + int.MaxValue < length)
				{
					_s.Read(ba, position, int.MaxValue);
					position += int.MaxValue;
				}
				_s.Read(ba, position, (int)(length - position));
			}
			else
			{
				_s.Read(ba, 0, (int)length);
			}
			return ba;
		}

		public int ReadInt32Be()
		{
			int j = 0;
			j += _s.ReadByte() << 24;
			j += _s.ReadByte() << 16;
			j += _s.ReadByte() << 8;
			j += _s.ReadByte();
			return j;
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Le")]
		public int ReadInt32Le()
		{
			int j = 0;
			j += _s.ReadByte();
			j += _s.ReadByte() << 8;
			j += _s.ReadByte() << 16;
			j += _s.ReadByte() << 24;
			return j;
		}

		public short ReadInt16Be()
		{
			int j = 0;
			j += _s.ReadByte() << 8;
			j += _s.ReadByte();
			return (short)j;
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Le")]
		public short ReadInt16Le()
		{
			int j = 0;
			j += _s.ReadByte();
			j += _s.ReadByte() << 8;
			return (short)j;
		}

		public uint ReadUInt32Be()
		{
			int j = 0;
			j += _s.ReadByte() << 24;
			j += _s.ReadByte() << 16;
			j += _s.ReadByte() << 8;
			j += _s.ReadByte();
			return (uint)j;
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Le")]
		public uint ReadUInt32Le()
		{
			int j = 0;
			j += _s.ReadByte();
			j += _s.ReadByte() << 8;
			j += _s.ReadByte() << 16;
			j += _s.ReadByte() << 24;
			return (uint)j;
		}

		public ushort ReadUInt16Be()
		{
			int j = 0;
			j += _s.ReadByte() << 8;
			j += _s.ReadByte();
			return (ushort)j;
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Le")]
		public ushort ReadUInt16Le()
		{
			int j = 0;
			j += _s.ReadByte();
			j += _s.ReadByte() << 8;
			return (ushort)j;
		}

		public byte ReadByte()
		{
			return (byte)_s.ReadByte();
		}

		public void SeekUntil(int position)
		{
			_s.Seek(position, SeekOrigin.Begin);
		}

		public void SeekUntil(uint position)
		{
			_s.Seek(position, SeekOrigin.Begin);
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "bool")]
		public bool ReadBool()
		{
			return _s.ReadByte() > 0;
		}

		public ulong ReadUInt64Be()
		{
			ulong j = 0;
			j += (ulong)_s.ReadByte() << 56;
			j += (ulong)_s.ReadByte() << 48;
			j += (ulong)_s.ReadByte() << 40;
			j += (ulong)_s.ReadByte() << 32;
			j += (ulong)_s.ReadByte() << 24;
			j += (ulong)_s.ReadByte() << 16;
			j += (ulong)_s.ReadByte() << 8;
			j += (ulong)_s.ReadByte();
			return j;
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Le")]
		public ulong ReadUInt64Le()
		{
			ulong j = 0;
			j += (ulong)_s.ReadByte();
			j += (ulong)_s.ReadByte() << 8;
			j += (ulong)_s.ReadByte() << 16;
			j += (ulong)_s.ReadByte() << 24;
			j += (ulong)_s.ReadByte() << 32;
			j += (ulong)_s.ReadByte() << 40;
			j += (ulong)_s.ReadByte() << 48;
			j += (ulong)_s.ReadByte() << 56;
			return j;
		}

		public string ReadFourChars()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}{3}",
				(char)_s.ReadByte(), (char)_s.ReadByte(), (char)_s.ReadByte(), (char)_s.ReadByte());
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "float32")]
		public float ReadFloat32Be()
		{
			byte[] temp = new byte[4];
			_s.Read(temp, 0, 4);
			if (BitConverter.IsLittleEndian)
			{
				Array.Reverse(temp);
			}

			return BitConverter.ToSingle(temp, 0);
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "float32"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Le")]
		public float ReadFloat32Le()
		{
			byte[] temp = new byte[4];
			_s.Read(temp, 0, 4);
			if (!BitConverter.IsLittleEndian)
			{
				Array.Reverse(temp);
			}

			return BitConverter.ToSingle(temp, 0);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		~FileReader()
		{
			Dispose(false);
		}

		private void Dispose(bool something)
		{
			if (!something) return;
			//Clean up the managed resources
			_s.Dispose();
			//Now clean up the unmanaged resources
			// There are none.
		}

		/*public void PrintPosition()
		{
			//Console.WriteLine("Pos: {0}", _s.Position);
		}*/

		public void WriteToFile(int size, string fileName)
		{
			using (FileWriter fw = new FileWriter(fileName))
			{
				fw.Write(Read(size));
			}
		}

		public void WriteToFile(uint size, string fileName)
		{
			using (FileWriter fw = new FileWriter(fileName))
			{
				fw.Write(Read(size));
			}
		}
	}
}
