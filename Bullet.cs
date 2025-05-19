using Raylib_cs;
using System.Numerics;

public class Bullet
{
    public Vector2 position; 
    public Vector2 direction; 
    public float speed;      
    public bool isActive;    

    
    public Bullet(float startX, float startY, Vector2 direction)
    {
        position = new Vector2(startX, startY);
        this.direction = direction; 
        speed = 10.0f;
        isActive = true;
    }

    public Bullet(float startX, float startY)
    {
        position = new Vector2(startX, startY);
        direction = new Vector2(1, 0);
        speed = 10.0f;
        isActive = true;
    }
    
    
    public void Move()
    {
        position += direction * speed;
    }

    
    public void Draw()
    {
        Raylib.DrawRectangle((int)position.X, (int)position.Y, 20, 5, Color.Yellow);
    }

   
    public void Deactivate()
    {
        if (position.X < 0 || position.X > Raylib.GetScreenWidth() ||
            position.Y < 0 || position.Y > Raylib.GetScreenHeight())
        {
            isActive = false;
        }
    }
}