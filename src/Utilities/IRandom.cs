namespace Microsoft.Xna.Framework.Utilities
{
    public interface IRandom
    {
        int Next();
        int Next(int maxValue);
        //int Next(int minValue, int maxValue);

        uint NextUInt32();
        uint NextUInt32(uint maxValue);
        //uint NextUInt32(uint minValue, uint maxValue);

        double NextDouble();

        byte[] NextBytes(byte[] buffer);
    }
}
