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

namespace Microsoft.Xna.Framework.Input.Bridle
{
	public struct ControllerData
	{
		public string LabelButtonA, LabelButtonB;
		public string LabelButtonX, LabelButtonY;
		public string LabelButtonLB, LabelButtonRB;
		public string LabelButtonLT, LabelButtonRT;
		public string LabelButtonBack, LabelButtonStart;
		public string LabelButtonPushLA, LabelButtonPushRA;
		public string LabelButtonLeftStick,
			LabelButtonLeftStickHorizontal,
			LabelButtonLeftStickVertical,
			LabelButtonLeftStickLeft,
			LabelButtonLeftStickRight,
			LabelButtonLeftStickUp,
			LabelButtonLeftStickDown;
		public string LabelButtonRightStick,
			LabelButtonRightStickHorizontal,
			LabelButtonRightStickVertical,
			LabelButtonRightStickLeft,
			LabelButtonRightStickRight,
			LabelButtonRightStickUp,
			LabelButtonRightStickDown;
		public string LabelButtonDPad,
			LabelButtonDPadHorizontal,
			LabelButtonDPadVertical,
			LabelButtonDPadLeft,
			LabelButtonDPadRight,
			LabelButtonDPadUp,
			LabelButtonDPadDown;
		public bool FeatureButtonGuide,
			FeatureRumble, FeatureWireless;

		public ControllerData(
			string labelButtonA = "A", string labelButtonB = "B",
			string labelButtonX = "X", string labelButtonY = "Y",
			string labelButtonLB = "LB", string labelButtonRB = "RB",
			string labelButtonLT = "LT", string labelButtonRT = "RT",
			string labelButtonBack = "Back", string labelButtonStart = "Start",
			string labelButtonPushLA = "Push Left Stick", string labelButtonPushRA = "Push Right Stick",
			string labelButtonLeftStick = "Left Stick",
			string labelButtonLeftStickHorizontal = "Left Stick Left/Right",
			string labelButtonLeftStickVertical = "Left Stick Up/Down",
			string labelButtonLeftStickLeft = "Left Stick Left",
			string labelButtonLeftStickRight = "Left Stick Right",
			string labelButtonLeftStickUp = "Left Stick Up",
			string labelButtonLeftStickDown = "Left Stick Down",
			string labelButtonRightStick = "Right Stick",
			string labelButtonRightStickHorizontal = "Right Stick Left/Right",
			string labelButtonRightStickVertical = "Right Stick Up/Down",
			string labelButtonRightStickLeft = "Right Stick Left",
			string labelButtonRightStickRight = "Right Stick Right",
			string labelButtonRightStickUp = "Right Stick Up",
			string labelButtonRightStickDown = "Right Stick Down",
			string labelButtonDPad = "D-Pad",
			string labelButtonDPadHorizontal = "D-Pad Left/Right",
			string labelButtonDPadVertical = "D-Pad Up/Down",
			string labelButtonDPadLeft = "D-Pad Left",
			string labelButtonDPadRight = "D-Pad Right",
			string labelButtonDPadUp = "D-Pad Up",
			string labelButtonDPadDown = "D-Pad Down",
			bool featureButtonGuide = true,
			bool featureRumble = true,
			bool featureWireless = false)
		{
			LabelButtonA = labelButtonA;
			LabelButtonB = labelButtonB;
			LabelButtonX = labelButtonX;
			LabelButtonY = labelButtonY;
			LabelButtonLB = labelButtonLB;
			LabelButtonRB = labelButtonRB;
			LabelButtonLT = labelButtonLT;
			LabelButtonRT = labelButtonRT;
			LabelButtonBack = labelButtonBack;
			LabelButtonStart = labelButtonStart;
			LabelButtonPushLA = labelButtonPushLA;
			LabelButtonPushRA = labelButtonPushRA;

			LabelButtonLeftStick = labelButtonLeftStick;
			LabelButtonLeftStickHorizontal = labelButtonLeftStickHorizontal;
			LabelButtonLeftStickVertical = labelButtonLeftStickVertical;
			LabelButtonLeftStickLeft = labelButtonLeftStickLeft;
			LabelButtonLeftStickRight = labelButtonLeftStickRight;
			LabelButtonLeftStickUp = labelButtonLeftStickUp;
			LabelButtonLeftStickDown = labelButtonLeftStickDown;

			LabelButtonRightStick = labelButtonRightStick;
			LabelButtonRightStickHorizontal = labelButtonRightStickHorizontal;
			LabelButtonRightStickVertical = labelButtonRightStickVertical;
			LabelButtonRightStickLeft = labelButtonRightStickLeft;
			LabelButtonRightStickRight = labelButtonRightStickRight;
			LabelButtonRightStickUp = labelButtonRightStickUp;
			LabelButtonRightStickDown = labelButtonRightStickDown;

			LabelButtonDPad = labelButtonDPad;
			LabelButtonDPadHorizontal = labelButtonDPadHorizontal;
			LabelButtonDPadVertical = labelButtonDPadVertical;
			LabelButtonDPadLeft = labelButtonDPadLeft;
			LabelButtonDPadRight = labelButtonDPadRight;
			LabelButtonDPadUp = labelButtonDPadUp;
			LabelButtonDPadDown = labelButtonDPadDown;

			FeatureButtonGuide = featureButtonGuide;

			FeatureRumble = featureRumble;
			FeatureWireless = featureWireless;
		}
	}
}
