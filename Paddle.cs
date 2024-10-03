using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Paddle
{
    private Texture2D _paddle_texture;
    public Vector2 position;
    public float velocity;
    public int width;
    public int height;

    public Paddle(Texture2D tex, Vector2 pos, float vel)
    {
        _paddle_texture = tex;
        position = pos;
        velocity = vel;

        width = tex.Width;
        height = tex.Height;
    }

    public void Update(GameTime gameTime, bool moveUp, bool moveDown, int screenHeight)
    {
        if (moveUp && position.Y > 0)
        {
            position.Y -= velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        if (moveDown && position.Y + height < screenHeight)
        {
            position.Y += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_paddle_texture, position, Color.White);
    }
}