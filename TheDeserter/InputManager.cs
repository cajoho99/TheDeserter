using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDeserter
{
    /// <summary>
    /// Manages all the inputs
    /// </summary>
    class InputManager
    {
        //Gets the keyboard state
        KeyboardState currentKeyState, previousKeyState;

        #region Singleton
        /// <summary>
        /// Singelton that makes it possible to access this class from anywhere
        /// </summary>
        
        private static InputManager _instance;
        public static InputManager Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new InputManager();
                }
                return _instance;
            }
        }
        #endregion

        /// <summary>
        /// The update function of the input manager
        /// </summary>
        public void Update()
        {
            previousKeyState = currentKeyState;
            currentKeyState = Keyboard.GetState();
        }

        /// <summary>
        /// Checks if the key(s) have been pressed
        /// </summary>
        /// <param name="keys">The keys that will be checked</param>
        /// <returns>state of button</returns>
        public bool KeyPressed(params Keys[] keys)
        {
            foreach(Keys key in keys)
            {
                if(currentKeyState.IsKeyDown(key) && previousKeyState.IsKeyUp(key))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if the key(s) have been released
        /// </summary>
        /// <param name="keys">The keys that will be checked</param>
        /// <returns>state of the button</returns>
        public bool KeyReleased(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (currentKeyState.IsKeyUp(key) && previousKeyState.IsKeyDown(key))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if the key(s) are being held down
        /// </summary>
        /// <param name="keys">The keys that will be checked</param>
        /// <returns>State of the keys</returns>
        public bool KeyDown(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (currentKeyState.IsKeyDown(key))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
