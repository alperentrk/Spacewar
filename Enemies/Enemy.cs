using Raylib_cs;
using System.Numerics;

namespace spacewarke.Enemies
{
    public abstract class Enemy
    {
        protected Vector2 position;
        protected Vector2 size;
        protected float speed;
        protected int health;
        protected int damage; 
        protected Texture2D texture;

        public Enemy(float startX, float startY, float speed, int health, int damage, Vector2 size, string texturePath)
        {
            position = new Vector2(startX, startY);
            this.speed = speed;
            this.health = health;
            this.damage = damage; 
            this.size = size;
            texture = Raylib.LoadTexture(texturePath);
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

        public void TakeDamage(int damage)
        {
            health -= damage;
        }

        public abstract void Move();

        
        public virtual void Attack(Spaceship player)
        {
            if (Vector2.Distance(position, player.GetPosition()) < 150)
            {
                player.TakeDamage(damage); 
            }
        }

        public virtual void Draw()
        {
            Raylib.DrawTexturePro(
                texture,
                new Rectangle(0, 0, texture.Width, texture.Height),
                new Rectangle(position.X, position.Y, size.X, size.Y),
                new Vector2(0, 0),
                0.0f,
                Color.White
            );
        }

        public virtual void Destroy()
        {
            health = 0; 
        }
    }
}