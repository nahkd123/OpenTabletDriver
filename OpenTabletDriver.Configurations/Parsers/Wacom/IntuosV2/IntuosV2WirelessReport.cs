using System.Numerics;
using System.Runtime.CompilerServices;
using OpenTabletDriver.Plugin.Tablet;

namespace OpenTabletDriver.Configurations.Parsers.Wacom.IntuosV2
{
    public struct IntuosV2WirelessReport : ITabletReport, IEraserReport, ITiltReport, IProximityReport
    {
        public IntuosV2WirelessReport(byte[] report)
        {
            Raw = report;

            Position = new Vector2
            {
                X = Unsafe.ReadUnaligned<ushort>(ref report[2]),
                Y = Unsafe.ReadUnaligned<ushort>(ref report[4])
            };
            Tilt = new Vector2
            {
                X = (sbyte)report[8],
                Y = (sbyte)report[9]
            };
            Pressure = Unsafe.ReadUnaligned<ushort>(ref report[6]);

            PenButtons = new bool[]
            {
                report[1].IsBitSet(1),
                report[1].IsBitSet(2)
            };
            Eraser = report[1].IsBitSet(4);
            NearProximity = report[1].IsBitSet(5);
            HoverDistance = report[14];
        }

        public byte[] Raw { get; set; }
        public Vector2 Position { get; set; }
        public uint Pressure { get; set; }
        public bool[] PenButtons { get; set; }
        public bool Eraser { get; set; }
        public bool NearProximity { get; set; }
        public uint HoverDistance { get; set; }
        public Vector2 Tilt { get; set; }
    }
}
