using System.Collections.Generic;
using OpenTabletDriver.Native.Linux.Evdev;
using OpenTabletDriver.Platform.Keyboard;

namespace OpenTabletDriver.Daemon.Library.Interop.Input.Keyboard
{
    public class LinuxKeysProvider : IKeyMapper
    {
        private static readonly Dictionary<BindableKey, EventCode> _platformBindings = new()
        {
            [BindableKey.Cancel] = EventCode.KEY_CANCEL,
            [BindableKey.Backspace] = EventCode.KEY_BACKSPACE,
            [BindableKey.Tab] = EventCode.KEY_TAB,
            [BindableKey.Clear] = EventCode.KEY_CLEAR,
            [BindableKey.Enter] = EventCode.KEY_ENTER,
            [BindableKey.Shift] = EventCode.KEY_LEFTSHIFT,
            [BindableKey.Control] = EventCode.KEY_LEFTCTRL,
            [BindableKey.Alt] = EventCode.KEY_LEFTALT,
            [BindableKey.Pause] = EventCode.KEY_PAUSE,
            [BindableKey.CapsLock] = EventCode.KEY_CAPSLOCK,
            [BindableKey.Escape] = EventCode.KEY_ESC,
            [BindableKey.Space] = EventCode.KEY_SPACE,
            [BindableKey.PageUp] = EventCode.KEY_PAGEUP,
            [BindableKey.PageDown] = EventCode.KEY_PAGEDOWN,
            [BindableKey.End] = EventCode.KEY_END,
            [BindableKey.Home] = EventCode.KEY_HOME,
            [BindableKey.Left] = EventCode.KEY_LEFT,
            [BindableKey.Up] = EventCode.KEY_UP,
            [BindableKey.Right] = EventCode.KEY_RIGHT,
            [BindableKey.Down] = EventCode.KEY_DOWN,
            [BindableKey.Select] = EventCode.KEY_SELECT,
            [BindableKey.Print] = EventCode.KEY_PRINT,
            // [BindableKey.Execute]
            [BindableKey.PrintScreen] = EventCode.KEY_PRINT,
            [BindableKey.Insert] = EventCode.KEY_INSERT,
            [BindableKey.Delete] = EventCode.KEY_DELETE,
            [BindableKey.Help] = EventCode.KEY_HELP,
            [BindableKey.D0] = EventCode.KEY_0,
            [BindableKey.D1] = EventCode.KEY_1,
            [BindableKey.D2] = EventCode.KEY_2,
            [BindableKey.D3] = EventCode.KEY_3,
            [BindableKey.D4] = EventCode.KEY_4,
            [BindableKey.D5] = EventCode.KEY_5,
            [BindableKey.D6] = EventCode.KEY_6,
            [BindableKey.D7] = EventCode.KEY_7,
            [BindableKey.D8] = EventCode.KEY_8,
            [BindableKey.D9] = EventCode.KEY_9,
            [BindableKey.A] = EventCode.KEY_A,
            [BindableKey.B] = EventCode.KEY_B,
            [BindableKey.C] = EventCode.KEY_C,
            [BindableKey.D] = EventCode.KEY_D,
            [BindableKey.E] = EventCode.KEY_E,
            [BindableKey.F] = EventCode.KEY_F,
            [BindableKey.G] = EventCode.KEY_G,
            [BindableKey.H] = EventCode.KEY_H,
            [BindableKey.I] = EventCode.KEY_I,
            [BindableKey.J] = EventCode.KEY_J,
            [BindableKey.K] = EventCode.KEY_K,
            [BindableKey.L] = EventCode.KEY_L,
            [BindableKey.M] = EventCode.KEY_M,
            [BindableKey.N] = EventCode.KEY_N,
            [BindableKey.O] = EventCode.KEY_O,
            [BindableKey.P] = EventCode.KEY_P,
            [BindableKey.Q] = EventCode.KEY_Q,
            [BindableKey.R] = EventCode.KEY_R,
            [BindableKey.S] = EventCode.KEY_S,
            [BindableKey.T] = EventCode.KEY_T,
            [BindableKey.U] = EventCode.KEY_U,
            [BindableKey.V] = EventCode.KEY_V,
            [BindableKey.W] = EventCode.KEY_W,
            [BindableKey.X] = EventCode.KEY_X,
            [BindableKey.Y] = EventCode.KEY_Y,
            [BindableKey.Z] = EventCode.KEY_Z,
            [BindableKey.LeftWindows] = EventCode.KEY_LEFTMETA,
            [BindableKey.RightWindows] = EventCode.KEY_RIGHTMETA,
            [BindableKey.Applications] = EventCode.KEY_LEFTMETA,
            [BindableKey.Sleep] = EventCode.KEY_SLEEP,
            [BindableKey.Numpad0] = EventCode.KEY_KP0,
            [BindableKey.Numpad1] = EventCode.KEY_KP1,
            [BindableKey.Numpad2] = EventCode.KEY_KP2,
            [BindableKey.Numpad3] = EventCode.KEY_KP3,
            [BindableKey.Numpad4] = EventCode.KEY_KP4,
            [BindableKey.Numpad5] = EventCode.KEY_KP5,
            [BindableKey.Numpad6] = EventCode.KEY_KP6,
            [BindableKey.Numpad7] = EventCode.KEY_KP7,
            [BindableKey.Numpad8] = EventCode.KEY_KP8,
            [BindableKey.Numpad9] = EventCode.KEY_KP9,
            [BindableKey.Multiply] = EventCode.KEY_KPASTERISK,
            [BindableKey.Add] = EventCode.KEY_KPPLUS,
            [BindableKey.Separator] = EventCode.KEY_KPCOMMA,
            [BindableKey.Subtract] = EventCode.KEY_KPMINUS,
            [BindableKey.Decimal] = EventCode.KEY_KPDOT,
            [BindableKey.Divide] = EventCode.KEY_KPSLASH,
            [BindableKey.F1] = EventCode.KEY_F1,
            [BindableKey.F2] = EventCode.KEY_F2,
            [BindableKey.F3] = EventCode.KEY_F3,
            [BindableKey.F4] = EventCode.KEY_F4,
            [BindableKey.F5] = EventCode.KEY_F5,
            [BindableKey.F6] = EventCode.KEY_F6,
            [BindableKey.F7] = EventCode.KEY_F7,
            [BindableKey.F8] = EventCode.KEY_F8,
            [BindableKey.F9] = EventCode.KEY_F9,
            [BindableKey.F10] = EventCode.KEY_F10,
            [BindableKey.F11] = EventCode.KEY_F11,
            [BindableKey.F12] = EventCode.KEY_F12,
            [BindableKey.F13] = EventCode.KEY_F13,
            [BindableKey.F14] = EventCode.KEY_F14,
            [BindableKey.F15] = EventCode.KEY_F15,
            [BindableKey.F16] = EventCode.KEY_F16,
            [BindableKey.F17] = EventCode.KEY_F17,
            [BindableKey.F18] = EventCode.KEY_F18,
            [BindableKey.F19] = EventCode.KEY_F19,
            [BindableKey.F20] = EventCode.KEY_F20,
            [BindableKey.F21] = EventCode.KEY_F21,
            [BindableKey.F22] = EventCode.KEY_F22,
            [BindableKey.F23] = EventCode.KEY_F23,
            [BindableKey.F24] = EventCode.KEY_F24,
            [BindableKey.NumLock] = EventCode.KEY_NUMLOCK,
            [BindableKey.ScrollLock] = EventCode.KEY_SCROLLLOCK,
            [BindableKey.LeftShift] = EventCode.KEY_LEFTSHIFT,
            [BindableKey.RightShift] = EventCode.KEY_RIGHTSHIFT,
            [BindableKey.LeftControl] = EventCode.KEY_LEFTCTRL,
            [BindableKey.RightControl] = EventCode.KEY_RIGHTCTRL,
            [BindableKey.LeftAlt] = EventCode.KEY_LEFTALT,
            [BindableKey.RightAlt] = EventCode.KEY_RIGHTALT,
            [BindableKey.BrowserBack] = EventCode.KEY_BACK,
            [BindableKey.BrowserForward] = EventCode.KEY_FORWARD,
            [BindableKey.BrowserRefresh] = EventCode.KEY_REFRESH,
            [BindableKey.BrowserStop] = EventCode.KEY_STOP,
            [BindableKey.BrowserSearch] = EventCode.KEY_SEARCH,
            [BindableKey.BrowserFavorites] = EventCode.KEY_FAVORITES,
            [BindableKey.BrowserHome] = EventCode.KEY_HOMEPAGE,
            [BindableKey.VolumeMute] = EventCode.KEY_MUTE,
            [BindableKey.VolumeDown] = EventCode.KEY_VOLUMEDOWN,
            [BindableKey.VolumeUp] = EventCode.KEY_VOLUMEUP,
            [BindableKey.MediaNextTrack] = EventCode.KEY_NEXTSONG,
            [BindableKey.MediaPreviousTrack] = EventCode.KEY_PREVIOUSSONG,
            [BindableKey.MediaStop] = EventCode.KEY_STOPCD,
            [BindableKey.MediaPlayPause] = EventCode.KEY_PLAYPAUSE,
            [BindableKey.LaunchMail] = EventCode.KEY_MAIL,
            [BindableKey.SelectMedia] = EventCode.KEY_MEDIA,
            // [BindableKey.LaunchApplication1]
            // [BindableKey.LaunchApplication2]
            [BindableKey.Oem1] = EventCode.KEY_SEMICOLON,
            [BindableKey.OemPlus] = EventCode.KEY_EQUAL,
            [BindableKey.OemComma] = EventCode.KEY_COMMA,
            [BindableKey.OemMinus] = EventCode.KEY_MINUS,
            [BindableKey.OemPeriod] = EventCode.KEY_DOT,
            [BindableKey.OemQuestion] = EventCode.KEY_SLASH,
            [BindableKey.OemTilde] = EventCode.KEY_GRAVE,
            [BindableKey.OemOpenBracket] = EventCode.KEY_LEFTBRACE,
            [BindableKey.OemPipe] = EventCode.KEY_BACKSLASH,
            [BindableKey.OemCloseBracket] = EventCode.KEY_RIGHTBRACE,
            [BindableKey.OemQuotes] = EventCode.KEY_APOSTROPHE,
            // [BindableKey.Oem8]
            [BindableKey.OemBackslash] = EventCode.KEY_102ND,
        };

        public object this[BindableKey key] => _platformBindings[key];
        public IEnumerable<BindableKey> GetBindableKeys() => _platformBindings.Keys;
    }
}
