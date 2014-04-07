using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Platformer.GameInputManagement
{
    public class InputManager
    {
        #region Singleton pattern
        private static InputManager instance;
        public static InputManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new InputManager();
                return instance;
            }
        }

        private InputManager() { }
        #endregion 

        #region Fields
        List<Action> actions = new List<Action>();
        #endregion

        #region Methods
        public Action AddAction(String actionName)
        {
            Action a = new Action(this, actionName);
            actions.Add(a);
            return a;
        }

        public Action this[String actionName]
        {
            get
            {
                return actions.Find(x => { return x.Name == actionName; });
            }
        }

        public void Update()
        {
            KeyboardState kbState = Keyboard.GetState(PlayerIndex.One);
            GamePadState gpState = GamePad.GetState(PlayerIndex.One);

            foreach (Action a in actions)
            {
                a.Update(kbState, gpState);
            }
        }
        #endregion
    }
}
