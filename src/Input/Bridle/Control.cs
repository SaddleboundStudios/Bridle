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
	public class Control
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")] public Input[] InputBindings;
		private readonly Controller _controller;
		private IPhysicalButton _physicalButton;
		private ModifierKeys _modifierKeys;

		public int Priority { get; set; }

		public string Name { get; set; }

		public int FramesEnabled { get; set; } = -1;

		public bool Enabled { get; set; } = true;

		// !!TODO: Put the modifiers on the input bindings, not in the control. This is *super* hacky.
		public Control(Controller controller, string name, int priority, Input[] inputs, ModifierKeys modifierKeys = ModifierKeys.None)
		{
			if (controller == null)
			{
				throw new ArgumentNullException(nameof(controller));
			}
			if (priority < 0 || priority > 9)
			{
				throw new ArgumentOutOfRangeException(nameof(priority));
			}

			_controller = controller;
			InputBindings = inputs;
			Priority = priority;
			Name = name;
			_modifierKeys = modifierKeys;
		}

		private bool _previousState;
		private bool _currentState;

		/// <summary>
		/// Returns true if the control was just pressed this frame, false otherwise.
		/// </summary>
		public bool Pressed()
		{
			if (_controller.Modifiers != _modifierKeys)
			{
				return false;
			}
			return !_previousState && _currentState;
		}

		private int _lastRecurring = 0;

		/// <summary>
		/// Returns true if the control was just pressed this frame or if it's repeated, false otherwise.
		/// </summary>
		public bool PressedRecurring(int initialDelay, int finalDelay)
		{
			return PressedRecurring(initialDelay, finalDelay, initialDelay * 4);
        }

		/// <summary>
		/// Returns true if the control was just pressed this frame or if it's repeated, false otherwise.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", MessageId = "initialDelay*10")]
		public bool PressedRecurring(int initialDelay, int finalDelay, int delayRampTime)
		{
			if (_currentState && !_previousState)
			{
				_lastRecurring = FramesEnabled;
                return true;
			}

			if (!_currentState)
			{
				return false;
			}

			int currentFrame = FramesEnabled - _lastRecurring;
			if (delayRampTime == -1)
			{
				delayRampTime = initialDelay * 4;
			}

			if (currentFrame < delayRampTime)
			{
				return (currentFrame % (initialDelay)) == 0;
			}

			return (currentFrame % finalDelay) == 0;
		}

		/// <summary>
		/// Returns true if the control was just pressed this frame or if it's repeated, false otherwise.
		/// </summary>
		/// <param name="isFresh">If this is not a repeated press, this is set to true.</param>
		/// <param name="initialDelay"></param>
		/// <param name="finalDelay"></param>
		/// <param name="delayRampTime"></param>
		/// <returns></returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", MessageId = "initialDelay*10")]
		public bool PressedRecurring(ref bool isFresh, int initialDelay, int finalDelay, int delayRampTime = -1)
		{
			if (_currentState && !_previousState)
			{
				_lastRecurring = FramesEnabled;
				isFresh = true;
				return true;
			}

			if (!_currentState)
			{
				return false;
			}

			int currentFrame = FramesEnabled - _lastRecurring;
			if (delayRampTime == -1)
			{
				delayRampTime = initialDelay * 4;
			}

			if (currentFrame < delayRampTime)
			{
				return (currentFrame % (initialDelay)) == 0;
			}

			return (currentFrame % finalDelay) == 0;
		}

		/// <summary>
		/// Returns true if the control is still being held this frame after being held the last, false otherwise.
		/// </summary>
		public bool Held()
		{
			return _previousState && _currentState;
		}

		/// <summary>
		/// Returns true if the control was just released this frame, false otherwise.
		/// </summary>
		public bool Released()
		{
			return _previousState && !_currentState;
		}

		/// <summary>
		/// Returns true if the control is down, false if it is up.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public bool Down()
		{
			return _currentState;
		}

		public static void UpdateState(Control control)
		{
			if (control == null)
			{
				throw new ArgumentNullException(nameof(control));
			}
			
			control._previousState = control._currentState;
			if (control.Enabled)
			{
				control.FramesEnabled++;
			}
			else
			{
				control.FramesEnabled = -1;
			}

			if (control._physicalButton == null)
			{
				control._currentState = false;
				return;
			}

			if (control._controller.IsPressed(control._physicalButton))
			{
				control._physicalButton.Used = true;
				control._currentState = true;
			}
			else
			{
				//control._physicalButton.Used = true; //Is this needed?
				control._currentState = false;
				control._physicalButton = null;
			}
		}

		public static IPhysicalButton GetPhysicalButton(Control control)
		{
			if (control == null)
			{
				throw new ArgumentNullException(nameof(control));
			}

			return control._physicalButton;
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "SetOn")]
		public static void SetOn(Control control, IPhysicalButton physicalButton)
		{
			if (control == null)
			{
				throw new ArgumentNullException(nameof(control));
			}

			if (physicalButton == null)
			{
				throw new ArgumentNullException(nameof(physicalButton));
			}

			control._currentState = true;
			control._physicalButton = physicalButton;
			physicalButton.Used = true;
		}
	}
}
