using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Platformer.GameInputManagement
{
    public class InputManager
    {
        #region Singleton pattern
        private static InputManager _instance;
        public static InputManager Instance
        {
            get { return _instance ?? (_instance = new InputManager()); }
        }

        private InputManager() { }
        #endregion 

        #region Fields
        readonly List<Action> _actions = new List<Action>();
        #endregion

        #region Methods
        public Action AddAction(String actionName)
        {
            var a = new Action(this, actionName);
            _actions.Add(a);
            return a;
        }

        public Action this[String actionName]
        {
            get
            {
                return _actions.Find(x => x.Name == actionName);
            }
        }

        public void Update()
        {
            var kbState = Keyboard.GetState(PlayerIndex.One);
            var gpState = GamePad.GetState(PlayerIndex.One);

            foreach (var a in _actions)
            {
                a.Update(kbState, gpState);
            }
        }
        #endregion
    }
}
