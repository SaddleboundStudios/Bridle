using System;
using System.IO;

namespace Microsoft.Xna.Framework.Data
{
	/// <summary>
	/// The version of FileWriter that doesn't have BE and LE mixed up (BE vs LE is named really poorly >:-C)
	/// </summary>
	public sealed class FileWriter : IDisposable
	{
		private readonly FileStream _s;

		public FileWriter(string fileName)
		{
			_s = File.Create(fileName);
		}

		public bool WriteCString(string value)
		{
			if (value == null)
			{
				_s.WriteByte(0x00);
				return false;
			}

			foreach (char c in value)
			{
				_s.WriteByte((byte)c);
			}

			_s.WriteByte(0x00);
			return false;
		}

		public bool Write(byte[] ba)
		{
			if (ba == null)
			{
				return false;
			}

			foreach (byte c in ba)
			{
				_s.WriteByte(c);
			}

			return true;
		}

		public void WriteInt32Be(int value)
		{
			_s.WriteByte((byte)((value & 0xFF000000) >> 24));
			_s.WriteByte((byte)((value & 0xFF0000) >> 16));
			_s.WriteByte((byte)((value & 0xFF00) >> 8));
			_s.WriteByte((byte)(value & 0xFF));
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Le")]
		public void WriteInt32Le(int value)
		{
			_s.WriteByte((byte)(value & 0xFF));
			_s.WriteByte((byte)((value & 0xFF00) >> 8));
			_s.WriteByte((byte)((value & 0xFF0000) >> 16));
			_s.WriteByte((byte)((value & 0xFF000000) >> 24));
		}

		public void WriteInt16Be(short value)
		{
			_s.WriteByte((byte)((value & 0xFF00) >> 8));
			_s.WriteByte((byte)(value & 0xFF));
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Le")]
		public void WriteInt16Le(short value)
		{
			_s.WriteByte((byte)(value & 0xFF));
			_s.WriteByte((byte)((value & 0xFF00) >> 8));
		}

		public void WriteUInt32Be(uint value)
		{
			_s.WriteByte((byte)((value & 0xFF000000) >> 24));
			_s.WriteByte((byte)((value & 0xFF0000) >> 16));
			_s.WriteByte((byte)((value & 0xFF00) >> 8));
			_s.WriteByte((byte)(value & 0xFF));
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Le")]
		public void WriteUInt32Le(uint value)
		{
			_s.WriteByte((byte)(value & 0xFF));
			_s.WriteByte((byte)((value & 0xFF00) >> 8));
			_s.WriteByte((byte)((value & 0xFF0000) >> 16));
			_s.WriteByte((byte)((value & 0xFF000000) >> 24));
		}

		public void WriteUInt16Be(ushort value)
		{
			_s.WriteByte((byte)((value & 0xFF00) >> 8));
			_s.WriteByte((byte)(value & 0xFF));
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Le")]
		public void WriteUInt16Le(ushort value)
		{
			_s.WriteByte((byte)(value & 0xFF));
			_s.WriteByte((byte)((value & 0xFF00) >> 8));
		}

		public void WriteByte(byte value)
		{
			_s.WriteByte(value);
		}

		public void PadUntil(int position)
		{
			_s.Seek(position, SeekOrigin.Begin);
		}

		public void WriteColor(byte red, byte green, byte blue)
		{
			_s.WriteByte(red);
			_s.WriteByte(green);
			_s.WriteByte(blue);
			_s.WriteByte(0xFF);
		}

		public void WriteColor(byte red, byte green, byte blue, byte alpha)
		{
			_s.WriteByte(red);
			_s.WriteByte(green);
			_s.WriteByte(blue);
			_s.WriteByte(alpha);
		}

		public void WriteBool(bool value)
		{
			_s.WriteByte(value ? (byte)1 : (byte)0);
		}

		public void WriteUInt64Be(ulong value)
		{
			_s.WriteByte((byte)((value & 0xFF00000000000000) >> 56));
			_s.WriteByte((byte)((value & 0xFF000000000000) >> 48));
			_s.WriteByte((byte)((value & 0xFF0000000000) >> 40));
			_s.WriteByte((byte)((value & 0xFF00000000) >> 32));
			_s.WriteByte((byte)((value & 0xFF000000) >> 24));
			_s.WriteByte((byte)((value & 0xFF0000) >> 16));
			_s.WriteByte((byte)((value & 0xFF00) >> 8));
			_s.WriteByte((byte)(value & 0xFF));
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Le")]
		public void WriteUInt64Le(ulong value)
		{
			_s.WriteByte((byte)(value & 0xFF));
			_s.WriteByte((byte)((value & 0xFF00) >> 8));
			_s.WriteByte((byte)((value & 0xFF0000) >> 16));
			_s.WriteByte((byte)((value & 0xFF000000) >> 24));
			_s.WriteByte((byte)((value & 0xFF00000000) >> 32));
			_s.WriteByte((byte)((value & 0xFF0000000000) >> 40));
			_s.WriteByte((byte)((value & 0xFF000000000000) >> 48));
			_s.WriteByte((byte)((value & 0xFF00000000000000) >> 56));
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		~FileWriter()
		{
			Dispose(false);
		}

		private void Dispose(bool something)
		{
			if (!something) return;
			//Clean up the managed resources
			_s.Flush();
			_s.Dispose();
			//Now clean up the unmanaged resources
		}
	}
}
