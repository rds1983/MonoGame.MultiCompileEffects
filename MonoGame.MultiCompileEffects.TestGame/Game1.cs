using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGame.MultiCompileEffects.TestGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _defaultFont;
        private readonly PerspectiveCamera _camera = new PerspectiveCamera();
        private Effect _simpleEffect;
        private Effect _lightningEffect;
        private readonly Cube _cube = new Cube();
        private int _rotationAngle;
        private bool _lightningOn = true;
        private bool _keyCDown;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _defaultFont = Content.Load<SpriteFont>("Default");

            var mcEffect = Content.Load<MultiCompileEffect>("BasicEffect");

            _simpleEffect = new Effect(GraphicsDevice, mcEffect.GetDefaultVariant());
            _lightningEffect = new Effect(GraphicsDevice, mcEffect.GetVariant(MultiCompileEffect.BuildKey(new[] { "LIGHTNING" })));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (Keyboard.GetState().IsKeyDown(Keys.C) && !_keyCDown)
            {
                _lightningOn = !_lightningOn;
                _keyCDown = true;
            }
            else if (!Keyboard.GetState().IsKeyDown(Keys.C) && _keyCDown)
            {
                _keyCDown = false;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Update camera
            _camera.Viewport = new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            // Prepare matrices
            var viewProjection = _camera.View * _camera.Projection;

            var angle = MathHelper.ToRadians(_rotationAngle);
            var transform = Matrix.CreateRotationX(angle) * Matrix.CreateRotationY(angle);

            ++_rotationAngle;
            while (_rotationAngle >= 360)
            {
                _rotationAngle -= 360;
            }

            var worldViewProj = transform * viewProjection;
            var worldInverseTranspose = Matrix.Transpose(Matrix.Invert(transform));

            // Update effect parameters
            var effect = _lightningOn ? _lightningEffect : _simpleEffect;
            
            effect.Parameters["worldViewProj"].SetValue(worldViewProj);
            effect.Parameters["worldInverseTranspose"].SetValue(worldInverseTranspose);
            effect.Parameters["diffuseColor"].SetValue(Color.Green.ToVector4());

            if (_lightningOn)
            {
                // Update lightning parameters
                effect.Parameters["dirLight0Direction"].SetValue(new Vector3(0, 0, -1));
                effect.Parameters["dirLight0DiffuseColor"].SetValue(Color.White.ToVector4());
            }

            // TODO: Add your drawing code here
            foreach (var t in effect.Techniques)
            {
                foreach (var p in t.Passes)
                {
                    p.Apply();

                    _cube.Render(GraphicsDevice);
                }
            }

            _spriteBatch.Begin();

            _spriteBatch.DrawString(_defaultFont,
                string.Format("Lightning: {0}. Press 'C' to switch.", _lightningOn ? "on" : "off"),
                new Vector2(0, 0), Color.White);

            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
