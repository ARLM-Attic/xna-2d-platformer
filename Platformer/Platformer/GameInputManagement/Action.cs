using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Platformer.GameInputManagement
{
    public class Action
    {
        #region Fields
        List<Keys> keysList = new List<Keys>();
        List<Buttons> buttonsList = new List<Buttons>();
        InputManager manager = null;
        bool currentStatus = false;
        bool previousStatus = false;

        public bool IsDown { get { return currentStatus; } }
        public bool IsTapped { get { return currentStatus && !previousStatus; } }
        public string Name { get; set; }
        #endregion

        #region Methods
        public Action(InputManager manager, string name)
        {
            this.manager = manager;
            this.Name = name;
        }

        public Action Add(Keys key)
        {
            if (!keysList.Contains(key))
                keysList.Add(key);
            return this;
        }

        public Action Add(Buttons button)
        {
            if (!buttonsList.Contains(button))
                buttonsList.Add(button);
            return this;
        }

        internal void Update(KeyboardState kbState, GamePadState gpState)
        {
            previousStatus = currentStatus;
            currentStatus = false;
            foreach (Keys k in keysList)
            {
                if (kbState.IsKeyDown(k))
                    currentStatus = true;
            }
            foreach (Buttons b in buttonsList)
            {
                if(gpState.IsButtonDown(b))
                    currentStatus = true;
            }
        }
        #endregion
    }
}
