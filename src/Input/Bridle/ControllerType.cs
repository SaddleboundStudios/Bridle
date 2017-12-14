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
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Xna.Framework.Input.Bridle
{
	public enum ControllerType
	{
		None = 0,
		Unknown = 1,
		Xbox360ControllerWired = 0x78696e70,
		SuncomSFXGamepadUSB = 0x3b0704a1,
		LogitechDualActionGamepad = 0x6d0416c2,
		LogitechF710 = 0x6d0419c2, // DirectInput mode
    }

    static public class ControllerTypeExtensionMethods
    {
        static public string GetControllerName(this ControllerType type)
        {
            switch (type)
            {
                case ControllerType.None:
                    return "None";
                case ControllerType.LogitechDualActionGamepad:
                    return "Logitech Dual Action Gamepad";
                case ControllerType.LogitechF710:
                    return "Logitech F710";
                case ControllerType.SuncomSFXGamepadUSB:
                    return "Suncom SFX Gamepad USB";
                case ControllerType.Xbox360ControllerWired:
                    return "Microsoft Xbox 360 Controller";
            }
            return "Unknown";
        }

        public static ControllerType GetControllerTypeFromGUID(this string guid)
        {
            if (string.IsNullOrWhiteSpace(guid))
            {
                return ControllerType.None;
            }
            int result;
            if (int.TryParse(guid, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out result))
            {
                return (ControllerType)result;
            }
            return ControllerType.Unknown;
        }

        public static ControllerData GetControllerData(this ControllerType type)
        {
            if (type == ControllerType.None)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (_controllerData.ContainsKey(type))
            {
                return _controllerData[type];
            }

            return _controllerData[ControllerType.Unknown];
        }

        private static Dictionary<ControllerType, ControllerData> _controllerData = new Dictionary<ControllerType, ControllerData>()
        {
            {
                ControllerType.Unknown, new ControllerData() // Default to an Xbox 360 controller.
			},
            {
                ControllerType.Xbox360ControllerWired, new ControllerData()
            },
            {
                ControllerType.LogitechF710, new ControllerData(
                    featureButtonGuide:false,
                    featureWireless:true)
            },
            {
                ControllerType.LogitechDualActionGamepad, new ControllerData(
                    labelButtonX:"1",
                    labelButtonA:"2",
                    labelButtonB:"3",
                    labelButtonY:"4",
                    labelButtonLB:"5",
                    labelButtonRB:"6",
                    labelButtonLT:"7",
                    labelButtonBack:"9",
                    labelButtonStart:"10",
                    featureButtonGuide:false,
                    featureRumble:false)
            },
        };
    }
}
