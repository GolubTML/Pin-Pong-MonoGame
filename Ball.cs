using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Ball
{
    private Texture2D _ball_texture;
    public Vector2 velocity;
    public Vector2 position;
    public int scale;

    public Ball(Texture2D texture, Vector2 vel, Vector2 pos)
    {
        _ball_texture = texture;
        velocity = vel;
        position = pos;

        scale = texture.Width;
    }

    public void Update(GameTime gameTime, int screenWidth, int screenHeight)
    {
        position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (position.Y <= 0 || position.Y + scale >= screenHeight)
        {
            velocity.Y = -velocity.Y;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_ball_texture, position, Color.White);
    }

    public bool CheckCollision(Paddle paddle)
    {
        Rectangle ball_rect = new Rectangle((int)position.X, (int)position.Y, scale, scale);
        Rectangle paddle_rect = new Rectangle((int)paddle.position.X, (int)paddle.position.Y, paddle.width, paddle.height);

        return ball_rect.Intersects(paddle_rect);
    }

    public void ResetBall(Vector2 vel, Vector2 pos)
    {
        velocity = vel;
        position = pos;
    }
}