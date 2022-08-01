using System.Drawing;
using System.Numerics;
using System.Xml.Xsl.Runtime;
using System.Net.Http.Headers;
using System.Net.Mime;

namespace BalloonAce;

public class Game1 : Game
{
    Texture2D planeTexture; 

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        planeTexture = ContentIterator.Load<Texture2D>("plane");

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

        _spriteBatch.Begin(); 
        _spriteBatch.Draw(planeTexture, new Vector2(2, 2), Color.Black);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
