using Windows.System;

namespace PacWoman.GameServices
{
    public static class ControlKeys
    {
        public static VirtualKey PlayerRunLeft { get; set; } = VirtualKey.Left;

        public static VirtualKey PlayerRunRight { get; set; } = VirtualKey.Right;

        public static VirtualKey PlayerRunUp { get; set; } = VirtualKey.Up;

        public static VirtualKey PlayerRunDown { get; set; } = VirtualKey.Down;
    }
}
