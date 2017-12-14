using System.Collections.Generic;
using Microsoft.Xna.Framework.Data;

namespace Microsoft.Xna.Framework.Graphics
{
	public class TextureWithMetadata
	{
		public Texture2D Texture { get; set; }
		public Dictionary<string, string> Metadata { get; set; }

		public TextureWithMetadata(Texture2D texture, Dictionary<string, string> metadata)
		{
			Texture = texture;
			Metadata = metadata;
		}

		public DataNode ToDataNode()
		{
			if (!Metadata.ContainsKey("Comment"))
			{
				return new DataNode();
			}

			DataNode node = DataLoader.Load(Metadata["Comment"]);
			if (Metadata.ContainsKey("Title") && !node.Values.ContainsKey("Title"))
			{
				node.Values.Add("TITLE", Metadata["Title"]);
			}
			return node;
		}
	}
}
