#region License
/* Bridle
 * Copyright 2017 Walter Barrett
 *
 * This project is released under the Microsoft Public
 * License. This file is dual-licensed under the MS-PL
 * and the 3-Clause BSD License.
 *
 * See LICENSE.MD for details.
 */
#endregion

using System;

namespace Microsoft.Xna.Framework.Input.Bridle
{
    public static class InputExtensionMethods
    {
        public static Input ToRaw(this Input input)
        {
            return (Input)((int)input & 0xFFFF);
        }

        internal static InputType GetInputType(this Input input)
        {
            int value = (int)input & 0xFFFF;
            if (value >= 256 * 3)
            {
                return InputType.None;
            }

            if (value >= 256 * 2)
            {
                return InputType.MouseButton;
            }

            if (value >= 256)
            {
                return InputType.Button;
            }

            if (value >= 0)
            {
                return InputType.Key;
            }

            return InputType.None;
        }

        internal static Keys GetKey(this Input input)
        {
            int value = (int)input & 0xFFFF;
            if (value >= 256)
            {
                return Keys.None;
            }

            if (value >= 0)
            {
                return (Keys)value;
            }

            return Keys.None;
        }

        internal static Buttons GetButton(this Input input)
        {
            int value = (int)input & 0xFFFF;
            if (value >= 256 * 2)
            {
                return 0;
            }

            if (value >= 256)
            {
                return (Buttons)Math.Pow(2, value - 256);
            }

            return 0;
        }

        internal static MouseButtons GetMouseButton(this Input input)
        {
            int value = (int)input & 0xFFFF;
            if (value >= 256 * 3)
            {
                return MouseButtons.None;
            }

            if (value >= 256 * 2)
            {
                return (MouseButtons)Math.Pow(2, value - 512);
            }

            return MouseButtons.None;
        }
    }

    public enum Input
	{
		None = 0,
		KeyBack = 8,
		KeyTab = 9,
		KeyEnter = 13,
		KeyPause = 19,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "KeyCaps")]
		KeyCapsLock = 20,
		KeyKana = 21,
		KeyKanji = 25,
		KeyEscape = 27,
		KeyImeConvert = 28,
		KeyImeNoConvert = 29,
		KeySpace = 32,
		KeyPageUp = 33,
		KeyPageDown = 34,
		KeyEnd = 35,
		KeyHome = 36,
		KeyLeft = 37,
		KeyUp = 38,
		KeyRight = 39,
		KeyDown = 40,
		KeySelect = 41,
		KeyPrint = 42,
		KeyExecute = 43,
		KeyPrintScreen = 44,
		KeyInsert = 45,
		KeyDelete = 46,
		KeyHelp = 47,
		KeyD0 = 48,
		KeyD1 = 49,
		KeyD2 = 50,
		KeyD3 = 51,
		KeyD4 = 52,
		KeyD5 = 53,
		KeyD6 = 54,
		KeyD7 = 55,
		KeyD8 = 56,
		KeyD9 = 57,
		KeyA = 65,
		KeyB = 66,
		KeyC = 67,
		KeyD = 68,
		KeyE = 69,
		KeyF = 70,
		KeyG = 71,
		KeyH = 72,
		KeyI = 73,
		KeyJ = 74,
		KeyK = 75,
		KeyL = 76,
		KeyM = 77,
		KeyN = 78,
		KeyO = 79,
		KeyP = 80,
		KeyQ = 81,
		KeyR = 82,
		KeyS = 83,
		KeyT = 84,
		KeyU = 85,
		KeyV = 86,
		KeyW = 87,
		KeyX = 88,
		KeyY = 89,
		KeyZ = 90,
		KeyLeftWindows = 91,
		KeyRightWindows = 92,
		KeyApps = 93,
		KeySleep = 95,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Num")]
		KeyNumPad0 = 96,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Num")]
		KeyNumPad1 = 97,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Num")]
		KeyNumPad2 = 98,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Num")]
		KeyNumPad3 = 99,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Num")]
		KeyNumPad4 = 100,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Num")]
		KeyNumPad5 = 101,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Num")]
		KeyNumPad6 = 102,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Num")]
		KeyNumPad7 = 103,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Num")]
		KeyNumPad8 = 104,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Num")]
		KeyNumPad9 = 105,
		KeyMultiply = 106,
		KeyAdd = 107,
		KeySeparator = 108,
		KeySubtract = 109,
		KeyDecimal = 110,
		KeyDivide = 111,
		KeyF1 = 112,
		KeyF2 = 113,
		KeyF3 = 114,
		KeyF4 = 115,
		KeyF5 = 116,
		KeyF6 = 117,
		KeyF7 = 118,
		KeyF8 = 119,
		KeyF9 = 120,
		KeyF10 = 121,
		KeyF11 = 122,
		KeyF12 = 123,
		KeyF13 = 124,
		KeyF14 = 125,
		KeyF15 = 126,
		KeyF16 = 127,
		KeyF17 = 128,
		KeyF18 = 129,
		KeyF19 = 130,
		KeyF20 = 131,
		KeyF21 = 132,
		KeyF22 = 133,
		KeyF23 = 134,
		KeyF24 = 135,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Num")]
		KeyNumLock = 144,
		KeyScroll = 145,
		KeyLeftShift = 160,
		KeyRightShift = 161,
		KeyLeftControl = 162,
		KeyRightControl = 163,
		KeyLeftAlt = 164,
		KeyRightAlt = 165,
		KeyBrowserBack = 166,
		KeyBrowserForward = 167,
		KeyBrowserRefresh = 168,
		KeyBrowserStop = 169,
		KeyBrowserSearch = 170,
		KeyBrowserFavorites = 171,
		KeyBrowserHome = 172,
		KeyVolumeMute = 173,
		KeyVolumeDown = 174,
		KeyVolumeUp = 175,
		KeyMediaNextTrack = 176,
		KeyMediaPreviousTrack = 177,
		KeyMediaStop = 178,
		KeyMediaPlayPause = 179,
		KeyLaunchMail = 180,
		KeySelectMedia = 181,
		KeyLaunchApplication1 = 182,
		KeyLaunchApplication2 = 183,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Oem")]
		KeyOemSemicolon = 186,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Oem")]
		KeyOemPlus = 187,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Oem")]
		KeyOemComma = 188,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Oem")]
		KeyOemMinus = 189,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Oem")]
		KeyOemPeriod = 190,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Oem")]
		KeyOemQuestion = 191,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Oem")]
		KeyOemTilde = 192,
		KeyChatPadGreen = 202,
		KeyChatPadOrange = 203,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Oem")]
		KeyOemOpenBrackets = 219,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Oem")]
		KeyOemPipe = 220,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Oem")]
		KeyOemCloseBrackets = 221,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Oem")]
		KeyOemQuotes = 222,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Oem")]
		KeyOem8 = 223,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Oem")]
		KeyOemBackslash = 226,
		KeyProcessKey = 229,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Oem")]
		KeyOemCopy = 242,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Oem")]
		KeyOemAuto = 243,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Oem")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Enl")]
		KeyOemEnlW = 244,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Attn")]
		KeyAttn = 246,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Crsel")]
		KeyCrsel = 247,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Exsel")]
		KeyExsel = 248,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Eof")]
		KeyEraseEof = 249,
		KeyPlay = 250,
		KeyZoom = 251,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Pa")]
		KeyPa1 = 253,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Oem")]
		KeyOemClear = 254,
		ButtonDPadUp = 256,
		ButtonDPadDown = 257,
		ButtonDPadLeft = 258,
		ButtonDPadRight = 259,
		ButtonStart = 260,
		ButtonBack = 261,
		ButtonLeftStick = 262,
		ButtonRightStick = 263,
		ButtonLeftShoulder = 264,
		ButtonRightShoulder = 265,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1700:DoNotNameEnumValuesReserved")]
		ButtonReserved0 = 266,
		ButtonBigButton = 267,
		ButtonA = 268,
		ButtonB = 269,
		ButtonX = 270,
		ButtonY = 271,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1700:DoNotNameEnumValuesReserved")]
		ButtonReserved1 = 272,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1700:DoNotNameEnumValuesReserved")]
		ButtonReserved2 = 273,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1700:DoNotNameEnumValuesReserved")]
		ButtonReserved3 = 274,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1700:DoNotNameEnumValuesReserved")]
		ButtonReserved4 = 275,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1700:DoNotNameEnumValuesReserved")]
		ButtonReserved5 = 276,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Thumbstick")]
		ButtonLeftThumbstickLeft = 277,
		ButtonRightTrigger = 278,
		ButtonLeftTrigger = 279,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Thumbstick")]
		ButtonRightThumbstickUp = 280,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Thumbstick")]
		ButtonRightThumbstickDown = 281,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Thumbstick")]
		ButtonRightThumbstickRight = 282,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Thumbstick")]
		ButtonRightThumbstickLeft = 283,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Thumbstick")]
		ButtonLeftThumbstickUp = 284,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Thumbstick")]
		ButtonLeftThumbstickDown = 285,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Thumbstick")]
		ButtonLeftThumbstickRight = 286,
		MouseButton1 = 512,
		MouseButton2 = 513,
		MouseButton3 = 514,
		MouseButton4 = 515,
		MouseButton5 = 516,
		MouseButton6 = 517,
		MouseButton7 = 518,
		MouseButton8 = 519,
		MouseButton9 = 520,
		MouseButton10 = 521,
		MouseButton11 = 522,
		MouseButton12 = 523,
		MouseButton13 = 524,
		MouseButton14 = 525,
		MouseButton15 = 526,
		MouseButton16 = 527,
		MouseWheelUp = 528,
		MouseWheelDown = 529,
		HoldKeyBack = 65544,
		HoldKeyTab = 65545,
		HoldKeyEnter = 65549,
		HoldKeyPause = 65555,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "KeyCaps")]
		HoldKeyCapsLock = 65556,
		HoldKeyKana = 65557,
		HoldKeyKanji = 65561,
		HoldKeyEscape = 65563,
		HoldKeyImeConvert = 65564,
		HoldKeyImeNoConvert = 65565,
		HoldKeySpace = 65568,
		HoldKeyPageUp = 65569,
		HoldKeyPageDown = 65570,
		HoldKeyEnd = 65571,
		HoldKeyHome = 65572,
		HoldKeyLeft = 65573,
		HoldKeyUp = 65574,
		HoldKeyRight = 65575,
		HoldKeyDown = 65576,
		HoldKeySelect = 65577,
		HoldKeyPrint = 65578,
		HoldKeyExecute = 65579,
		HoldKeyPrintScreen = 65580,
		HoldKeyInsert = 65581,
		HoldKeyDelete = 65582,
		HoldKeyHelp = 65583,
		HoldKeyD0 = 65584,
		HoldKeyD1 = 65585,
		HoldKeyD2 = 65586,
		HoldKeyD3 = 65587,
		HoldKeyD4 = 65588,
		HoldKeyD5 = 65589,
		HoldKeyD6 = 65590,
		HoldKeyD7 = 65591,
		HoldKeyD8 = 65592,
		HoldKeyD9 = 65593,
		HoldKeyA = 65601,
		HoldKeyB = 65602,
		HoldKeyC = 65603,
		HoldKeyD = 65604,
		HoldKeyE = 65605,
		HoldKeyF = 65606,
		HoldKeyG = 65607,
		HoldKeyH = 65608,
		HoldKeyI = 65609,
		HoldKeyJ = 65610,
		HoldKeyK = 65611,
		HoldKeyL = 65612,
		HoldKeyM = 65613,
		HoldKeyN = 65614,
		HoldKeyO = 65615,
		HoldKeyP = 65616,
		HoldKeyQ = 65617,
		HoldKeyR = 65618,
		HoldKeyS = 65619,
		HoldKeyT = 65620,
		HoldKeyU = 65621,
		HoldKeyV = 65622,
		HoldKeyW = 65623,
		HoldKeyX = 65624,
		HoldKeyY = 65625,
		HoldKeyZ = 65626,
		HoldKeyLeftWindows = 65627,
		HoldKeyRightWindows = 65628,
		HoldKeyApps = 65629,
		HoldKeySleep = 65631,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Num")]
		HoldKeyNumPad0 = 65632,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Num")]
		HoldKeyNumPad1 = 65633,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Num")]
		HoldKeyNumPad2 = 65634,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Num")]
		HoldKeyNumPad3 = 65635,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Num")]
		HoldKeyNumPad4 = 65636,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Num")]
		HoldKeyNumPad5 = 65637,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Num")]
		HoldKeyNumPad6 = 65638,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Num")]
		HoldKeyNumPad7 = 65639,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Num")]
		HoldKeyNumPad8 = 65640,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Num")]
		HoldKeyNumPad9 = 65641,
		HoldKeyMultiply = 65642,
		HoldKeyAdd = 65643,
		HoldKeySeparator = 65644,
		HoldKeySubtract = 65645,
		HoldKeyDecimal = 65646,
		HoldKeyDivide = 65647,
		HoldKeyF1 = 65648,
		HoldKeyF2 = 65649,
		HoldKeyF3 = 65650,
		HoldKeyF4 = 65651,
		HoldKeyF5 = 65652,
		HoldKeyF6 = 65653,
		HoldKeyF7 = 65654,
		HoldKeyF8 = 65655,
		HoldKeyF9 = 65656,
		HoldKeyF10 = 65657,
		HoldKeyF11 = 65658,
		HoldKeyF12 = 65659,
		HoldKeyF13 = 65660,
		HoldKeyF14 = 65661,
		HoldKeyF15 = 65662,
		HoldKeyF16 = 65663,
		HoldKeyF17 = 65664,
		HoldKeyF18 = 65665,
		HoldKeyF19 = 65666,
		HoldKeyF20 = 65667,
		HoldKeyF21 = 65668,
		HoldKeyF22 = 65669,
		HoldKeyF23 = 65670,
		HoldKeyF24 = 65671,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Num")]
		HoldKeyNumLock = 65680,
		HoldKeyScroll = 65681,
		HoldKeyLeftShift = 65696,
		HoldKeyRightShift = 65697,
		HoldKeyLeftControl = 65698,
		HoldKeyRightControl = 65699,
		HoldKeyLeftAlt = 65700,
		HoldKeyRightAlt = 65701,
		HoldKeyBrowserBack = 65702,
		HoldKeyBrowserForward = 65703,
		HoldKeyBrowserRefresh = 65704,
		HoldKeyBrowserStop = 65705,
		HoldKeyBrowserSearch = 65706,
		HoldKeyBrowserFavorites = 65707,
		HoldKeyBrowserHome = 65708,
		HoldKeyVolumeMute = 65709,
		HoldKeyVolumeDown = 65710,
		HoldKeyVolumeUp = 65711,
		HoldKeyMediaNextTrack = 65712,
		HoldKeyMediaPreviousTrack = 65713,
		HoldKeyMediaStop = 65714,
		HoldKeyMediaPlayPause = 65715,
		HoldKeyLaunchMail = 65716,
		HoldKeySelectMedia = 65717,
		HoldKeyLaunchApplication1 = 65718,
		HoldKeyLaunchApplication2 = 65719,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Oem")]
		HoldKeyOemSemicolon = 65722,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Oem")]
		HoldKeyOemPlus = 65723,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Oem")]
		HoldKeyOemComma = 65724,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Oem")]
		HoldKeyOemMinus = 65725,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Oem")]
		HoldKeyOemPeriod = 65726,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Oem")]
		HoldKeyOemQuestion = 65727,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Oem")]
		HoldKeyOemTilde = 65728,
		HoldKeyChatPadGreen = 65738,
		HoldKeyChatPadOrange = 65739,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Oem")]
		HoldKeyOemOpenBrackets = 65755,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Oem")]
		HoldKeyOemPipe = 65756,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Oem")]
		HoldKeyOemCloseBrackets = 65757,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Oem")]
		HoldKeyOemQuotes = 65758,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Oem")]
		HoldKeyOem8 = 65759,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Oem")]
		HoldKeyOemBackslash = 65762,
		HoldKeyProcessKey = 65765,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Oem")]
		HoldKeyOemCopy = 65778,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Oem")]
		HoldKeyOemAuto = 65779,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Oem")]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Enl")]
		HoldKeyOemEnlW = 65780,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Attn")]
		HoldKeyAttn = 65782,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Crsel")]
		HoldKeyCrsel = 65783,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Exsel")]
		HoldKeyExsel = 65784,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Eof")]
		HoldKeyEraseEof = 65785,
		HoldKeyPlay = 65786,
		HoldKeyZoom = 65787,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Pa")]
		HoldKeyPa1 = 65789,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Oem")]
		HoldKeyOemClear = 65790,
		HoldButtonDPadUp = 65792,
		HoldButtonDPadDown = 65793,
		HoldButtonDPadLeft = 65794,
		HoldButtonDPadRight = 65795,
		HoldButtonStart = 65796,
		HoldButtonBack = 65797,
		HoldButtonLeftStick = 65798,
		HoldButtonRightStick = 65799,
		HoldButtonLeftShoulder = 65800,
		HoldButtonRightShoulder = 65801,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1700:DoNotNameEnumValuesReserved")]
		HoldButtonReserved0 = 65802,
		HoldButtonBigButton = 65803,
		HoldButtonA = 65804,
		HoldButtonB = 65805,
		HoldButtonX = 65806,
		HoldButtonY = 65807,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1700:DoNotNameEnumValuesReserved")]
		HoldButtonReserved1 = 65808,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1700:DoNotNameEnumValuesReserved")]
		HoldButtonReserved2 = 65809,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1700:DoNotNameEnumValuesReserved")]
		HoldButtonReserved3 = 65810,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1700:DoNotNameEnumValuesReserved")]
		HoldButtonReserved4 = 65811,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1700:DoNotNameEnumValuesReserved")]
		HoldButtonReserved5 = 65812,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Thumbstick")]
		HoldButtonLeftThumbstickLeft = 65813,
		HoldButtonRightTrigger = 65814,
		HoldButtonLeftTrigger = 65815,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Thumbstick")]
		HoldButtonRightThumbstickUp = 65816,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Thumbstick")]
		HoldButtonRightThumbstickDown = 65817,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Thumbstick")]
		HoldButtonRightThumbstickRight = 65818,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Thumbstick")]
		HoldButtonRightThumbstickLeft = 65819,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Thumbstick")]
		HoldButtonLeftThumbstickUp = 65820,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Thumbstick")]
		HoldButtonLeftThumbstickDown = 65821,
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Thumbstick")]
		HoldButtonLeftThumbstickRight = 65822,
		HoldMouseButton1 = 66048,
		HoldMouseButton2 = 66049,
		HoldMouseButton3 = 66050,
		HoldMouseButton4 = 66051,
		HoldMouseButton5 = 66052,
		HoldMouseButton6 = 66053,
		HoldMouseButton7 = 66054,
		HoldMouseButton8 = 66055,
		HoldMouseButton9 = 66056,
		HoldMouseButton10 = 66057,
		HoldMouseButton11 = 66058,
		HoldMouseButton12 = 66059,
		HoldMouseButton13 = 66060,
		HoldMouseButton14 = 66061,
		HoldMouseButton15 = 66062,
		HoldMouseButton16 = 66063,
		MoveMouseButton1 = 131584,
		MoveMouseButton2 = 131585,
		MoveMouseButton3 = 131586,
		MoveMouseButton4 = 131587,
		MoveMouseButton5 = 131588,
		MoveMouseButton6 = 131589,
		MoveMouseButton7 = 131590,
		MoveMouseButton8 = 131591,
		MoveMouseButton9 = 131592,
		MoveMouseButton10 = 131593,
		MoveMouseButton11 = 131594,
		MoveMouseButton12 = 131595,
		MoveMouseButton13 = 131596,
		MoveMouseButton14 = 131597,
		MoveMouseButton15 = 131598,
		MoveMouseButton16 = 131599,
	}
}
