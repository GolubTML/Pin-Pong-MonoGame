using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Real_Pin_Pong;

public class Real_Pin_Pong : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private SpriteFont _spriteFont;

    private Ball _ball;
    private Paddle _player1;
    private Paddle _player2;

    private int _screenWidth = 650;
    private int _screenHeight = 650;
    private int _player1_score = 0;
    private int _player2_score = 0;

    public Real_Pin_Pong()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";

        _graphics.PreferredBackBufferWidth = _screenWidth;
        _graphics.PreferredBackBufferHeight = _screenHeight;

        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _spriteFont = Content.Load<SpriteFont>("Font");

        Texture2D paddle_tex = new Texture2D(GraphicsDevice, 10, 50);
        Color[] paddel_data = new Color[10 * 50];
        for (int i = 0; i < paddel_data.Length; i++)
            paddel_data[i] = Color.White;
        
        paddle_tex.SetData(paddel_data);

        Texture2D ball_tex = new Texture2D(GraphicsDevice, 5, 5);
        Color[] ball_data = new Color[5 * 5];
        for (int i = 0; i < ball_data.Length; i++)
            ball_data[i] = Color.White;
        
        ball_tex.SetData(ball_data);

        _player1 = new Paddle(paddle_tex, new Vector2(20, _screenHeight / 2 - paddle_tex.Height / 2), 300f);
        _player2 = new Paddle(paddle_tex, new Vector2(_screenHeight - 40, _screenHeight / 2 - paddle_tex.Height / 2), 300f);

        _ball = new Ball(ball_tex, new Vector2(200f, 150f), new Vector2(_screenWidth / 2, _screenHeight / 2));
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        KeyboardState keyState = Keyboard.GetState();

        bool player1_up = keyState.IsKeyDown(Keys.W);
        bool player1_down = keyState.IsKeyDown(Keys.S);
        _player1.Update(gameTime, player1_up, player1_down, _screenHeight);

        bool player2_up = keyState.IsKeyDown(Keys.Up);
        bool player2_down = keyState.IsKeyDown(Keys.Down);
        _player2.Update(gameTime, player2_up, player2_down, _screenHeight);

        _ball.Update(gameTime, _screenWidth, _screenHeight);

        if (_ball.CheckCollision(_player1) || _ball.CheckCollision(_player2))
        {
            _ball.velocity = -_ball.velocity;
        }

        if (_ball.position.X <= 0)
        {
            _player2_score++;
            _ball.ResetBall(new Vector2(200f, 150f), new Vector2(_screenWidth / 2, _screenHeight / 2));
        }
        else if (_ball.position.X + _ball.scale>= _screenWidth)
        {
            _player1_score++;
            _ball.ResetBall(new Vector2(200f, 150f), new Vector2(_screenWidth / 2, _screenHeight / 2));
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();

        _ball.Draw(_spriteBatch);
        _player1.Draw(_spriteBatch);
        _player2.Draw(_spriteBatch);

        _spriteBatch.DrawString(_spriteFont, $"Player 1: {_player1_score}", new Vector2(10, 10), Color.White);
        _spriteBatch.DrawString(_spriteFont, $"Player 2: {_player2_score}", new Vector2(_screenWidth - 150, 10), Color.White);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}