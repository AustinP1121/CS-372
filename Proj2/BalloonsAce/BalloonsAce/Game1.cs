using System; 
using System.Security.Cryptography;
using System.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BalloonsAce
{
    public class Game1 : Game
    {
        SpriteFont font; 
        string text = "0000";

        int greenScore = 0; 
        int yellowScore = 0;

        int randomX = new Random().Next(850,900);
        int randomY = new Random().Next(100,1000);

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        
        Circle planePlayer = new Circle();

        Circle greenCircle1 = new Circle();
        Circle greenCircle2 = new Circle(); 

        Circle yellowCircle1 = new Circle();
        Circle yellowCircle2 = new Circle();

        float planeSpeed = 200f; 
        float ballSpeed = 250f; 

        //plane texture
        Texture2D planeTexture;

        //green circle texture
        Texture2D greenCircleTexture; 

        //yellow circle texture 
        Texture2D yellowCircleTexture;

        public struct Circle
        {
            public Vector2 Center { get; set; }
            public float Radius { get; set; }
            
            public Circle(Vector2 center, float radius)
            {
                Center = center;
                Radius = radius;
            }

            public bool Contains(Vector2 point)
            {
                return ((point - Center).Length() <= Radius);
            }
    
            public bool Intersects(Circle other)
            {
                Console.Write((other.Center - Center).Length() < (other.Radius - Radius));
                return ((other.Center - Center).Length() < (other.Radius - Radius));
            }
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            //sets window size to 1200x900
            _graphics.PreferredBackBufferWidth = 900;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.ApplyChanges();

            //this looks good, **chef's kiss**
            planePlayer.Center = new Vector2(20, 50);
            planePlayer.Radius = 256; 

            greenCircle1.Center = new Vector2(randomX, randomY);
            greenCircle2.Center = new Vector2(randomX, randomY);

            greenCircle1.Radius = 55; 
            greenCircle2.Radius = 55;

            yellowCircle1.Center = new Vector2(randomX, randomY);
            yellowCircle2.Center = new Vector2(randomX, randomY); 

            yellowCircle1.Radius = 55; 
            yellowCircle2.Radius = 55; 

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("myfont");

            //plane and circle textures
            planeTexture = Content.Load<Texture2D>("planeDefault");
            greenCircleTexture = Content.Load<Texture2D>("greenCircleT");
            yellowCircleTexture = Content.Load<Texture2D>("yellowCircle1");
        }

        protected override void Update(GameTime gameTime)
        {
            var kstate = Keyboard.GetState();

            if(greenCircle1.Center.X < 3|| greenCircle1.Intersects(planePlayer))
            {
                if (greenCircle1.Intersects(planePlayer))
                {
                    greenScore += 1;
                    greenCircle1.Center = RespawnCircle();
                }
                else
                    greenCircle1.Center = RespawnCircle();
            }
            else if (greenCircle2.Center.X < 3 || greenCircle2.Intersects(planePlayer))
            {
                if (greenCircle2.Intersects(planePlayer))
                {
                    greenScore +=1;
                    greenCircle2.Center = RespawnCircle();
                }
                else 
                    greenCircle2.Center = RespawnCircle();
            }            
            else if (yellowCircle1.Center.X < 3 || yellowCircle1.Intersects(planePlayer))
            {
                if (yellowCircle1.Intersects(planePlayer))
                {
                    yellowScore -= 1;
                    yellowCircle1.Center = RespawnCircle();
                }
                else
                    yellowScore +=1;
                    yellowCircle1.Center = RespawnCircle();
            }
            else if (yellowCircle2.Center.X < 3 || yellowCircle2.Intersects(planePlayer))
            {
                if (yellowCircle2.Intersects(planePlayer))
                {
                    yellowScore -= 1;
                    yellowCircle2.Center = RespawnCircle();
                }
                else
                    yellowScore += 1; 
                    yellowCircle2.Center = RespawnCircle();
            }
            

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || 
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
                
            if (kstate.IsKeyDown(Keys.Up))
            {
                planePlayer.Center -= new Vector2(0, planeSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }

            if(kstate.IsKeyDown(Keys.Down))
            {
                planePlayer.Center += new Vector2(0, planeSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);//;
            }

            //works!!!
            greenCircle1.Center -= new Vector2(ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);
            greenCircle2.Center -= new Vector2(ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);

            yellowCircle1.Center -= new Vector2(ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);
            yellowCircle2.Center -= new Vector2(ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            var kstate = Keyboard.GetState();

            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.CornflowerBlue);
            
            _spriteBatch.Begin();
            Vector2 textMiddlePoint = font.MeasureString(text) / 2;

            Vector2 greenPosition = new Vector2(Window.ClientBounds.Width - 350, 
                Window.ClientBounds.Height / 16);
            Vector2 yellowPosition = new Vector2(Window.ClientBounds.Width - 350, 
                Window.ClientBounds.Height / 13);
            Vector2 messagePosition = new Vector2(Window.ClientBounds.Width - 350, 
                Window.ClientBounds.Height / 11);

            _spriteBatch.DrawString(font, "Number of Green Balloons Hit: "+Convert.ToString(greenScore), 
                greenPosition, Microsoft.Xna.Framework.Color.LightGreen, 0, textMiddlePoint, 1.0f,
                SpriteEffects.None, .05f);
            _spriteBatch.DrawString(font, "Number of Yellow Balloons Avoided: "+Convert.ToString(yellowScore), 
                yellowPosition, Microsoft.Xna.Framework.Color.GreenYellow, 0, textMiddlePoint, 1.0f,
                SpriteEffects.None, .05f);
            _spriteBatch.DrawString(font, "Press Escape to Exit Game", 
                messagePosition, Microsoft.Xna.Framework.Color.Red, 0, textMiddlePoint, 1.0f,
                SpriteEffects.None, .05f);

            _spriteBatch.Draw(planeTexture, planePlayer.Center, 
                 Microsoft.Xna.Framework.Color.White); 
            
            _spriteBatch.Draw(greenCircleTexture, greenCircle1.Center, 
                 Microsoft.Xna.Framework.Color.CornflowerBlue);
            _spriteBatch.Draw(greenCircleTexture, greenCircle2.Center, 
                 Microsoft.Xna.Framework.Color.CornflowerBlue);
            
             _spriteBatch.Draw(yellowCircleTexture, yellowCircle1.Center, 
                 Microsoft.Xna.Framework.Color.Yellow);
            _spriteBatch.Draw(yellowCircleTexture, yellowCircle2.Center, 
                 Microsoft.Xna.Framework.Color.Yellow);
                       
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public Vector2 RespawnCircle() 
        {   
            int randomX = 900;
            int randomY = new Random().Next(0,900);

            Vector2 newCenter = new Vector2(randomX, randomY);

            return newCenter; 
        }
    }
}