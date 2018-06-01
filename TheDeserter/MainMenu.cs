using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDeserter
{
    /// <summary>
    /// Main menu object
    /// </summary>
    class MainMenu
    {
        //Enums are used for futureproofing
        enum MenuItems
        {
            Play = 0,
            Exit = 1
        }

        // Sets the starting value to play
        MenuItems menuItems = MenuItems.Play;


        
        //Button and cursor texture and position
        private Texture2D playButton,         endButton,         cursor;
        private Vector2   playButtonPosition, endButtonPosition, cursorPosition;

        //Title texture and position
        private Texture2D title;
        private Vector2 titlePosition;

        public Rectangle MenuSize;

        /// <summary>
        /// Constructor for the main menu
        /// </summary>
        /// <param name="Content">Main Content Manager</param>
        public MainMenu(ContentManager Content)
        {
            // Load Textures
            playButton = Content.Load<Texture2D>("PlayButton");
            endButton = Content.Load<Texture2D>("EndButton");
            cursor = Content.Load<Texture2D>("MenuCursor");
            title = Content.Load<Texture2D>("TitleLogo");
            //Sets the size of the menu
            MenuSize = new Rectangle(0, 0, title.Width, 800);

            
            titlePosition = new Vector2(0, 0);

            // Set button position
            playButtonPosition = new Vector2((MenuSize.Width - playButton.Width) / 2, 132 + title.Height);
            endButtonPosition = new Vector2(((MenuSize.Width - playButton.Width) / 2), playButtonPosition.Y + playButton.Height + 132);

            cursorPosition = playButtonPosition;

        }


        public void Update(Game1 game)
        {
            // Sets cursorPosition according to menuItems
            cursorPosition = new Vector2(cursorPosition.X, playButtonPosition.Y + (playButton.Height + 132) * (int)menuItems);

            // Down
            if (InputManager.Instance.KeyPressed(Keys.S, Keys.Down))
            {
                menuItems++;
            }
            // Up
            if(InputManager.Instance.KeyPressed(Keys.W, Keys.Up))
            {
                menuItems--;
            }

            //Locks menuItems to 0 and 1
            menuItems = (MenuItems)(Math.Abs((int)menuItems));
            menuItems = (MenuItems)((int)menuItems % 2);

            //Checks for an enter press
            if (InputManager.Instance.KeyPressed(Keys.Enter))
            {
                if(menuItems == MenuItems.Play)
                {
                    game.gameState = Game1.GameState.Running;
                }
                else if(menuItems == MenuItems.Exit)
                {
                    game.Exit();
                }
            }

        }


        /// <summary>
        /// Main draw function for the Main Menu
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playButton, playButtonPosition, Color.White);
            spriteBatch.Draw(endButton,  endButtonPosition,  Color.White);
            spriteBatch.Draw(cursor,     cursorPosition,     Color.White);
            spriteBatch.Draw(title,      titlePosition,      Color.White);
        }

    }
}
