using Raylib_cs;
using System.Numerics;

namespace spacewarke.Enemies
{
    public class FastEnemy : Enemy
    {
        private float evadeCooldown = 2.0f;
        private float evadeTimer = 0.0f;

        public FastEnemy(float startX, float startY)
            : base(startX, startY, 5.0f, 30, 10, new Vector2(80, 80), "C:/Users/alper/Desktop/KODMOD/Spaceresim/FastEnemy.png")
        { }

        public override void Move()
        {
            position.X -= speed;
            evadeTimer += Raylib.GetFrameTime();
            if (evadeTimer >= evadeCooldown)
            {
                position.Y += (Raylib.GetRandomValue(0, 1) == 0 ? -30 : 30);
                evadeTimer = 0.0f;
            }
        }
    }
}