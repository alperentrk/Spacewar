using Raylib_cs;
using System.Numerics;

namespace spacewarke.Enemies
{
    public class BasicEnemy : Enemy
    {
        private float cooldownTimer = 0.0f;

        public BasicEnemy(float startX, float startY)
            : base(startX, startY, 2.0f, 50, 10, new Vector2(70, 70), "C:/Users/alper/Desktop/KODMOD/Spaceresim/BasicEnemy.png")
        { }

        public override void Move()
        {
            position.X -= speed;
        }
    }
}