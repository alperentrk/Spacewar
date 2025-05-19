using Raylib_cs;
using System.Numerics;
using System.Collections.Generic;

namespace spacewarke.Enemies
{
    public class BossEnemy : Enemy
    {
        private float attackCooldown = 1.5f;
        private float cooldownTimer = 0.0f;
        private List<Bullet> bullets;

        public BossEnemy(float startX, float startY)
            : base(startX, startY, 1.0f, 200, 60, new Vector2(150, 150), "C:/Users/alper/Desktop/KODMOD/Spaceresim/BossEnemy.png")
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
                Vector2 direction = Vector2.Normalize(player.GetPosition() - position);
                bullets.Add(new Bullet(position.X, position.Y, direction)); 
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
}
