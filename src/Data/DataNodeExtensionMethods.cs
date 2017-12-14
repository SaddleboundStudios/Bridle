using System;
using System.Globalization;

namespace Microsoft.Xna.Framework.Data
{
	public static class DataNodeExtensionMethods
	{

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "2#")]
		public static bool SetFromNode(this DataNode node, string key, ref string variable)
		{
			if (node == null) { return false; }
			if (key == null) { throw new ArgumentNullException(nameof(key)); }
			key = key.ToUpper(CultureInfo.InvariantCulture);
			if (!node.Values.ContainsKey(key)) { return false; }
			string value = node.Values[key];
			switch (DataNode.GetValueType(value))
			{
				case DataType.QuotedString:
					variable = value.Substring(1, value.Length - 2);
					return true;
				default:
					variable = value;
					return true;
			}

			/*variable = null;
			return false;*/
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "2#")]
		public static string GetRawValue(this DataNode node, string key)
		{
			if (node == null) { return null; }
			if (key == null) { throw new ArgumentNullException(nameof(key)); }
			key = key.Trim().ToUpper(CultureInfo.InvariantCulture);
			if (!node.Values.ContainsKey(key)) { return null; }
			string value = node.Values[key];
			switch (DataNode.GetValueType(value))
			{
				case DataType.QuotedString:
					return value.Substring(1, value.Length - 2);
				default:
					return value;
			}
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "2#")]
		public static bool SetFromNode(this DataNode node, string key, ref double variable)
		{
			if (node == null) { return false; }
			if (key == null) { throw new ArgumentNullException(nameof(key)); }
			key = key.ToUpper(CultureInfo.InvariantCulture);
			if (!node.Values.ContainsKey(key)) { return false; }
			string value = node.Values[key];
			switch (DataNode.GetValueType(value))
			{
				case DataType.Integer:
					variable = Convert.ToInt32(value, CultureInfo.InvariantCulture);
					return true;
				case DataType.Float:
					variable = Convert.ToDouble(value, CultureInfo.InvariantCulture);
					return true;
			}

			return false;
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "2#")]
		public static bool SetFromNode(this DataNode node, string key, ref float variable)
		{
			if (node == null) { return false; }
			if (key == null) { throw new ArgumentNullException(nameof(key)); }
			key = key.ToUpper(CultureInfo.InvariantCulture);
			if (!node.Values.ContainsKey(key)) { return false; }
			string value = node.Values[key];
			switch (DataNode.GetValueType(value))
			{
				case DataType.Integer:
					variable = Convert.ToInt32(value, CultureInfo.InvariantCulture);
					return true;
				case DataType.Float:
					return float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out variable);
			}

			return false;
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "2#")]
		public static bool SetFromNode(this DataNode node, string key, ref int variable)
		{
			if (node == null) { return false; }
			if (key == null) { throw new ArgumentNullException(nameof(key)); }
			key = key.ToUpper(CultureInfo.InvariantCulture);
			if (!node.Values.ContainsKey(key)) { return false; }
			string value = node.Values[key];
			switch (DataNode.GetValueType(value))
			{
				case DataType.Integer:
					variable = Convert.ToInt32(value, CultureInfo.InvariantCulture);
					return true;
			}

			return false;
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "2#")]
		public static bool SetFromNode(this DataNode node, string key, ref bool variable)
		{
			if (node == null) { return false; }
			if (key == null) { throw new ArgumentNullException(nameof(key)); }
			key = key.ToUpper(CultureInfo.InvariantCulture);
			if (!node.Values.ContainsKey(key)) { return false; }
			string value = node.Values[key].ToUpper(CultureInfo.InvariantCulture);
			switch (DataNode.GetValueType(node.Values[key]))
			{
				case DataType.Keyword:
					switch (value)
					{
						case "TRUE":
							//case "yes":
							//case "on":
							variable = true;
							return true;
						case "FALSE":
							//case "no":
							//case "off":
							variable = false;
							return true;
						default:
							return false;
					}
				default:
					return false;
			}
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "2#")]
        public static bool SetFromNode(this DataNode node, string key, ref Point variable)
        {
            if (node == null) { return false; }
            if (key == null) { throw new ArgumentNullException(nameof(key)); }
            key = key.ToUpper(CultureInfo.InvariantCulture);
            if (!node.Values.ContainsKey(key)) { return false; }
            string value = node.Values[key];

            if (!value.Contains(","))
            {
                return false;
            }
            string[] splitValue = value.Split(',');
            if (splitValue.Length != 2)
            {
                return false;
            }

            int x, y;
            string p1 = splitValue[0].Trim();
            string p2 = splitValue[1].Trim();

            switch (DataNode.GetValueType(p1))
            {
                case DataType.Integer:
                    if (!int.TryParse(p1, NumberStyles.Integer, CultureInfo.InvariantCulture, out x))
                    {
                        return false;
                    }
                    break;
                default:
                    return false;
            }

            switch (DataNode.GetValueType(p2))
            {
                case DataType.Integer:
                    if (!int.TryParse(p2, NumberStyles.Integer, CultureInfo.InvariantCulture, out y))
                    {
                        return false;
                    }
                    break;
                default:
                    return false;
            }

            variable = new Point(x, y);

            return true;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "2#")]
        public static bool SetFromNode(this DataNode node, string key, ref Vector2 variable)
        {
            if (node == null) { return false; }
            if (key == null) { throw new ArgumentNullException(nameof(key)); }
            key = key.ToUpper(CultureInfo.InvariantCulture);
            if (!node.Values.ContainsKey(key)) { return false; }
            string value = node.Values[key];

            if (!value.Contains(","))
            {
                return false;
            }
            string[] splitValue = value.Split(',');
            if (splitValue.Length != 2)
            {
                return false;
            }

            float x, y;
            int temp;

            string p1 = splitValue[0].Trim();
            string p2 = splitValue[1].Trim();

            switch (DataNode.GetValueType(p1))
            {
                case DataType.Integer:
                    if (!int.TryParse(p1, NumberStyles.Integer, CultureInfo.InvariantCulture, out temp))
                    {
                        return false;
                    }
                    x = temp;
                    break;
                case DataType.Float:
                    if (!float.TryParse(p1, NumberStyles.Float, CultureInfo.InvariantCulture, out x))
                    {
                        return false;
                    }
                    break;
                default:
                    return false;
            }

            switch (DataNode.GetValueType(p2))
            {
                case DataType.Integer:
                    if (!int.TryParse(p2, NumberStyles.Integer, CultureInfo.InvariantCulture, out temp))
                    {
                        return false;
                    }
                    y = temp;
                    break;
                case DataType.Float:
                    if (!float.TryParse(p2, NumberStyles.Float, CultureInfo.InvariantCulture, out y))
                    {
                        return false;
                    }
                    break;
                default:
                    return false;
            }

            variable = new Vector2(x, y);

            return true;
        }
    }
}
