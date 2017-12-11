using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework.Utilities;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        const ulong RandNumRounds = 256;

        private void Test_IRandom_Next(IRandom rand)
        {
            const int wmin = 0, wmax = int.MaxValue;
            int amin = 0, amax = 0;

            for (ulong j = 0; j < RandNumRounds; j++)
            {
                for (ulong i = 0; i < UInt16.MaxValue; i++)
                {
                    int n = rand.Next();
                    Assert.IsTrue(n >= 0);
                    amin = Math.Min(n, amin);
                    amax = Math.Max(n, amax);
                }
            }

            Console.WriteLine("Wanted: { " + wmin + ", " + wmax + " }");
            Console.WriteLine("Actual: { " + amin + ", " + amax + " }");
            Console.WriteLine("Delta:  { " + Delta(amin, wmin) + ", " + Delta(amax, wmax) + " }");
        }

        private void Test_IRandom_NextUInt32(IRandom rand)
        {
            const uint wmin = 0, wmax = uint.MaxValue;
            uint amin = 0, amax = 0;

            for (ulong j = 0; j < RandNumRounds; j++)
            {
                for (ulong i = 0; i < UInt16.MaxValue; i++)
                {
                    uint n = rand.NextUInt32();
                    //Assert.IsTrue(n >= 0);
                    amin = Math.Min(n, amin);
                    amax = Math.Max(n, amax);
                }
            }

            Console.WriteLine("Wanted: { " + wmin + ", " + wmax + " }");
            Console.WriteLine("Actual: { " + amin + ", " + amax + " }");
            Console.WriteLine("Delta:  { " + Delta(amin, wmin) + ", " + Delta(amax, wmax) + " }");
        }

        /*private void Test_IRandom_NextUInt32_2(IRandom rand)
        {
            uint wmin = 0, wmax = 31;
            uint amin = 0, amax = 0;

            for (ulong j = 0; j < RandNumRounds; j++)
            {
                for (ulong i = 0; i < UInt16.MaxValue; i++)
                {
                    uint n = rand.NextUInt32(wmin, wmax+1);
                    //Assert.IsTrue(n >= 0);
                    amin = Math.Min(n, amin);
                    amax = Math.Max(n, amax);
                }
            }

            Console.WriteLine("Wanted: { " + wmin + ", " + wmax + " }");
            Console.WriteLine("Actual: { " + amin + ", " + amax + " }");
            Console.WriteLine("Delta:  { " + Delta(amin, wmin) + ", " + Delta(amax, wmax) + " }");

            wmin = 32; wmax = 63;
            amin = UInt16.MaxValue; amax = 0;

            for (ulong j = 0; j < RandNumRounds; j++)
            {
                for (ulong i = 0; i < UInt16.MaxValue; i++)
                {
                    uint n = rand.NextUInt32(wmin, wmax+1);
                    //Assert.IsTrue(n >= 0);
                    amin = Math.Min(n, amin);
                    amax = Math.Max(n, amax);
                }
            }

            Console.WriteLine("Wanted: { " + wmin + ", " + wmax + " }");
            Console.WriteLine("Actual: { " + amin + ", " + amax + " }");
            Console.WriteLine("Delta:  { " + Delta(amin, wmin) + ", " + Delta(amax, wmax) + " }");
        }*/

        private void Test_IRandom_NextDouble(IRandom rand)
        {
            const double wmin = 0.0d, wmax = 1.0d;
            double amin = 0, amax = 0;

            for (ulong j = 0; j < RandNumRounds; j++)
            {
                for (ulong i = 0; i < UInt16.MaxValue; i++)
                {
                    double d = rand.NextDouble();
                    Assert.IsTrue(d >= 0.0d && d <= 1.0d);
                    amin = Math.Min(d, amin);
                    amax = Math.Max(d, amax);
                }
            }

            Console.WriteLine("Wanted: { " + wmin + ", " + wmax + " }");
            Console.WriteLine("Actual: { " + amin + ", " + amax + " }");
            Console.WriteLine("Delta:  { " + Math.Round(Delta(amin, wmin), 4) + ", " + Math.Round(Delta(amax, wmax), 4) + " }");
        }

        [TestMethod] public void Test_Xorshift_Next() { Test_IRandom_Next(new Xorshift(0)); }

        /*[TestMethod] public void Test_SystemRandom_Next() { Test_IRandom_Next(new SystemRandom(0)); }*/

        [TestMethod] public void Test_Xorshift_NextUInt32() { Test_IRandom_NextUInt32(new Xorshift(0)); }

        /*[TestMethod] public void Test_SystemRandom_NextUInt32() { Test_IRandom_NextUInt32(new SystemRandom(0)); }*/

        //[TestMethod] public void Test_Xorshift_NextUInt32_2() { Test_IRandom_NextUInt32_2(new Xorshift(0)); }

        /*[TestMethod] public void Test_SystemRandom_NextUInt32_2() { Test_IRandom_NextUInt32_2(new SystemRandom(0)); }*/

        [TestMethod] public void Test_Xorshift_NextDouble() { Test_IRandom_NextDouble(new Xorshift(0)); }

        /*[TestMethod] public void Test_SystemRandom_NextDouble() { Test_IRandom_NextDouble(new SystemRandom(0)); }*/
        
        private int Delta(int a, int b)
        {
            if (a > b)
            {
                return a - b;
            }
            return b - a;
        }

        private long Delta(uint a, uint b)
        {
            if (a > b) { return a - b; }
            return b - a;
        }

        private double Delta(double a, double b)
        {
            if (a > b) { return a - b; }
            return b - a;
        }
    }
}
