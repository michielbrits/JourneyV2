using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace JourneyIntoNyx
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Map map;
        Player player;
        Camera camera;
        Texture2D background;
        Rectangle mainFrame;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

      
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            map = new Map();
            player = new Player();
            camera = new Camera(GraphicsDevice.Viewport);
            base.Initialize();
        }

     
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Tiles.Content = Content;
            map.Generate(new int[,]
            {
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
                {0,0,0,0,0,2,0,0,0,0,1,2,2,3,0,0, },
                {0,1,3,0,0,0,0,0,0,2,0,0,0,0,0,0, },
                {2,3,0,0,0,2,0,0,0,0,0,2,0,0,0,0, },
                {3,0,0,0,1,3,0,0,1,0,0,0,0,0,0,0, },
                {3,0,0,1,2,2,3,0,0,0,0,0,0,0,0,0, },
                {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,3, },
            }, 64);
            player.Load(Content);

            // TODO: use this.Content to load your game content here
        }

       
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update(gameTime);
            foreach (CollisionTiles tile in map.CollisionTiles)
            {
                player.Collision(tile.Rectangle, map.Width, map.Heigth);
                camera.Update(player.Position, map.Width, map.Heigth);
            }
                
            base.Update(gameTime);
        }

       
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend,null,null,null,null,camera.Transform);
            map.Draw(spriteBatch);
            player.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
