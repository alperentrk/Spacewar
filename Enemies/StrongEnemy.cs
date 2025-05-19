using Raylib_cs;
using spacewarke.Enemies;
using System.Numerics;

public class StrongEnemy : Enemy
{
    private float attackCooldown = 1.0f; 
    private float cooldownTimer = 0.0f;  
    private List<Bullet> bullets;

    public StrongEnemy(float startX, float startY)
        : base(startX, startY, 1.5f, 100, 40, new Vector2(100, 100), "C:/Users/alper/Desktop/KODMOD/Spaceresim/StrongEnemy.png")
    {
        bullets = new List<Bullet>();
    }

    public override void Move()
    {
        position.X -= speed;
    }

    public override void Attack(Spaceship player)
    {
        cooldownTimer += Raylib.GetFrameTime();

        if (cooldownTimer >= attackCooldown)
        {
            bullets.Add(new Bullet(position.X, position.Y, new Vector2(-1, 0)));
            cooldownTimer = 0.0f;
        }

        UpdateBullets(); 
    }

    private void UpdateBullets()
    {
        foreach (Bullet bullet in bullets)
        {
            bullet.Move();
            bullet.Deactivate();
        }
        bullets.RemoveAll(b => !b.isActive);
    }

    public override void Draw()
    {
        base.Draw();
        foreach (Bullet bullet in bullets)
        {
            bullet.Draw();
        }
    }

    public List<Bullet> GetBullets()
    {
        return bullets;
    }

}
