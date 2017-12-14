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
using System.Linq;
using Microsoft.Xna.Framework.Utilities;

namespace Microsoft.Xna.Framework.Input.Bridle
{
	/// <summary>
	/// A class used to easily check what virtual buttons are pressed.
	/// </summary>
	public class Controller : OrderedDictionary<string, Control>
	{
		private KeyboardState _curKeyboard;
		private GamePadState _curGamepad;
		private MouseState _curMouse;
		private static int _oldMouseX;
		private static int _oldMouseY;
		public static int MouseX;
		public static int MouseY;
		public static bool MouseMoved;
		public static int LastDetent;
		public static bool MouseScrollUp;
		public static bool MouseScrollDown;

		public static readonly int Detent = 120;

		public ModifierKeys Modifiers { get; set; }

		public bool IsPressed(IPhysicalButton pb)
		{
			switch (pb.Input.GetInputType())
			{
				case InputType.Button:
				{
					return _curGamepad.IsButtonDown(pb.Input.GetButton());
				}
				case InputType.Key:
				{
					return _curKeyboard.IsKeyDown(pb.Input.GetKey());
				}
				case InputType.MouseButton:
				{
					return _curMouse.IsButtonDown(pb.Input.GetMouseButton());
				}
			}
			return false;
		}

		public void Update()
		{
			// !TODO: Throw an exception if we've never ordered our controls.
			_curKeyboard = Keyboard.GetState();
			Modifiers = GetModifierKeys(_curKeyboard);
			_curGamepad = GamePad.GetState(PlayerIndex.One);
			_curMouse = Mouse.GetState();
			_oldMouseX = MouseX;
			_oldMouseY = MouseY;
			MouseX = _curMouse.X;
			MouseY = _curMouse.Y;
			MouseMoved = _oldMouseX != _curMouse.X || _oldMouseY != _curMouse.Y;
			MouseScrollUp = false;
			MouseScrollDown = false;

			if (_curMouse.ScrollWheelValue > LastDetent)
			{
				if (_curMouse.ScrollWheelValue - LastDetent >= Detent)
				{
					LastDetent += Detent;
					MouseScrollUp = true;
				}
			}
			else if (_curMouse.ScrollWheelValue < LastDetent)
			{
				if (LastDetent - _curMouse.ScrollWheelValue >= Detent)
				{
					LastDetent -= Detent;
					MouseScrollDown = true;
				}
			}
			
			foreach (PhysicalButton pb in _physicalButtons)
			{
				pb.Used = false;
				if (IsPressed(pb))
				{
					pb.FramesDown++;
				}
				else
				{
					pb.FramesDown = -1;
				}
			}

			foreach (Control c in Values)
			{
				Control.UpdateState(c);
			}

			foreach (PhysicalButton pb in _physicalButtons)
			{
				bool temp = IsPressed(pb);
				if (!pb.Used && temp)
				{
					foreach (Control c in pb.Controls)
					{
						if ((c.Enabled && c.FramesEnabled >= pb.FramesDown) || Control.GetPhysicalButton(c) == pb)
						{
							if (c.Down()) //User might be trying to activate a seperate control.
							{
								continue;
							}

							Control.SetOn(c, pb);
							break;
						}
					}
				}
			}
		}

		private ModifierKeys GetModifierKeys(KeyboardState keyboardState)
		{
			ModifierKeys mk = ModifierKeys.None;
			if (keyboardState.IsKeyDown(Framework.Input.Keys.LeftAlt) || keyboardState.IsKeyDown(Framework.Input.Keys.RightAlt))
			{
				mk = mk | ModifierKeys.Alt;
			}
			if (keyboardState.IsKeyDown(Framework.Input.Keys.LeftControl) || keyboardState.IsKeyDown(Framework.Input.Keys.RightControl))
			{
				mk = mk | ModifierKeys.Ctrl;
			}
			if (keyboardState.IsKeyDown(Framework.Input.Keys.LeftShift) || keyboardState.IsKeyDown(Framework.Input.Keys.RightShift))
			{
				mk = mk | ModifierKeys.Shift;
			}
			return mk;
		}

		private List<PhysicalButton> _physicalButtons = new List<PhysicalButton>();

		public static List<Control> GetControls(Controller c)
		{
			return (List<Control>) c.Values;
		}

		/// <summary>
		/// Sort controls by priority
		/// </summary>
		public void OrderControls()
		{
		    SortValues((k, v) => -v.Priority);
            
			_physicalButtons = new List<PhysicalButton>();
			Dictionary<Input, PhysicalButton> pInputs = new Dictionary<Input, PhysicalButton>();
			foreach (Control c in Values)
			{
				if (c.InputBindings != null)
				{
					foreach (Input input in c.InputBindings)
					{
						if (!pInputs.ContainsKey(input))
						{
							pInputs[input] = new PhysicalButton(input);
						}
						PhysicalButton pb = pInputs[input];
						pb.Controls.Add(c);
					}
				}

				foreach (PhysicalButton pb in pInputs.Values)
				{
					_physicalButtons.Add(pb);
				}
			}
		}

		public Vector2 GetMousePos()
		{
			return new Vector2(MouseX, MouseY);
		}

		public void GetGUID()
		{
			ControllerType guid = GamePad.GetGUIDEXT(PlayerIndex.One).GetControllerTypeFromGUID();
			Console.WriteLine($"1: {guid:X}, {guid}, {guid.GetControllerName()}");
			guid = GamePad.GetGUIDEXT(PlayerIndex.Two).GetControllerTypeFromGUID();
			Console.WriteLine($"2: {guid:X}, {guid}, {guid.GetControllerName()}");
			guid = GamePad.GetGUIDEXT(PlayerIndex.Three).GetControllerTypeFromGUID();
			Console.WriteLine($"3: {guid:X}, {guid}, {guid.GetControllerName()}");
			guid = GamePad.GetGUIDEXT(PlayerIndex.Four).GetControllerTypeFromGUID();
			Console.WriteLine($"4: {guid:X}, {guid}, {guid.GetControllerName()}");
			//Console.WriteLine("----------");
			//SDL2.SDL.SDL_GameControllerAddMappingsFromFile("gamecontrollerdb.txt");
			/*for (int i = 0; i < SDL2.SDL.SDL_NumJoysticks(); i++)
			{
				Console.WriteLine($"{i}: {SDL2.SDL.SDL_IsGameController(i)}");
				Console.WriteLine($"{SDL2.SDL.SDL_GetError()}");
				Console.WriteLine($"{SDL2.SDL.SDL_GameControllerNameForIndex(i)}");
				IntPtr controller = SDL2.SDL.SDL_GameControllerOpen(i);
				if (controller == IntPtr.Zero)
				{
					Console.WriteLine($"Could not open game controller {i}: {SDL2.SDL.SDL_GetError()}");
				}

				Console.WriteLine($"MappingForGUID: {SDL2.SDL.SDL_GameControllerMappingForGUID(new Guid("3b0704a1000000000000504944564944"))}");
				Console.WriteLine($"SDL_GetError(): {SDL2.SDL.SDL_GetError()}");
			}*/
		}
	}
}
