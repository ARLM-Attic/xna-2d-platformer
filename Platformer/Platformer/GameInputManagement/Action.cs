using System.Linq;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Platformer.Properties;

namespace Platformer.GameInputManagement
{
    public class Action
    {
        #region Fields

        readonly List<Keys> _keysList = new List<Keys>();
        readonly List<Buttons> _buttonsList = new List<Buttons>();
        [UsedImplicitly] InputManager _manager;
        bool _previousStatus;

        public bool IsDown { get; private set; }
        public bool IsTapped { get { return IsDown && !_previousStatus; } }
        public string Name { get; set; }
        #endregion

        #region Methods
        public Action(InputManager manager, string name)
        {
            _previousStatus = false;
            IsDown = false;
            _manager = manager;
            Name = name;
        }

        public Action Add(Keys key)
        {
            if (!_keysList.Contains(key))
                _keysList.Add(key);
            return this;
        }

        public Action Add(Buttons button)
        {
            if (!_buttonsList.Contains(button))
                _buttonsList.Add(button);
            return this;
        }

        internal void Update(KeyboardState kbState, GamePadState gpState)
        {
            _previousStatus = IsDown;
            IsDown = false;
            foreach (var k in _keysList.Where(kbState.IsKeyDown))
                IsDown = true;
            foreach (var b in _buttonsList.Where(gpState.IsButtonDown))
                IsDown = true;
        }
        #endregion
    }
}
