using System;
using System.Numerics;
using Raylib_cs;

public class Spaceship
{
    private Vector2 position;   
    private Vector2 size;       
    private float speed;        
    private Texture2D texture;  
    private int health;         

    public List<Bullet> bullets;

    public Spaceship(float startX, float startY, string texturePath)
    {
        position = new Vector2(startX, startY);
        size = new Vector2(130, 130);
        speed = 8.0f;
        bullets = new List<Bullet>();
        texture = Raylib.LoadTexture(texturePath);
        health = 100; 
    }

    public void Move()
    {
        if (Raylib.IsKeyDown(KeyboardKey.W)) position.Y -= speed;
        if (Raylib.IsKeyDown(KeyboardKey.S)) position.Y += speed;
        if (Raylib.IsKeyDown(KeyboardKey.A)) position.X -= speed;
        if (Raylib.IsKeyDown(KeyboardKey.D)) position.X += speed;

        position.X = Math.Clamp(position.X, 0, Raylib.GetScreenWidth() - size.X);
        position.Y = Math.Clamp(position.Y, 0, Raylib.GetScreenHeight());
    }

    public void Shoot()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.Space))
        {
            bullets.Add(new Bullet(position.X + size.X / 2 + 2, position.Y));
        }
    }

    public void UpdateBullets()
    {
        foreach (Bullet bullet in bullets)
        {
            bullet.Move();
            bullet.Deactivate();
        }
        bullets.RemoveAll(b => !b.isActive);
    }

    public void Draw()
    {
        Raylib.DrawTexturePro(
            texture,
            new Rectangle(0, 0, texture.Width, texture.Height),
            new Rectangle(position.X, position.Y, size.X, size.Y),
            new Vector2(size.X / 2, size.Y / 2),
            0.0f,
            Color.White
        );

        
        Raylib.DrawRectangle(20, 20, 250, 30, Color.DarkGray);
        Raylib.DrawRectangle(20, 20, (int)(250 * (health / 100.0f)), 30, Color.Red);
        Raylib.DrawText($"{health}/100", 280, 20, 30, Color.White); 

        foreach (Bullet bullet in bullets)
        {
            bullet.Draw();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        health = Math.Max(health, 0); 
    }

    public void Heal(int amount)
    {
        health = Math.Min(health + amount, 100); 
    }

    public int GetHealth()
    {
        return health;
    }

    public Vector2 GetPosition()
    {
        return position;
    }

    public Vector2 GetSize()
    {
        return size;
    }

    public void Unload()
    {
        Raylib.UnloadTexture(texture);
    }

}