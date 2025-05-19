using Raylib_cs;
using spacewarke.Enemies;
using System.Collections.Generic;

public class CollisionDetector
{
  
    public static void CheckBulletCollision(List<Bullet> bullets, List<Enemy> enemies, Spaceship player, System.Action<int> updateScore)
    {
        foreach (Bullet bullet in bullets)
        {
            foreach (Enemy enemy in enemies)
            {
                if (Raylib.CheckCollisionRecs(
                    new Rectangle(bullet.position.X, bullet.position.Y, 20, 5),
                    new Rectangle(enemy.GetPosition().X, enemy.GetPosition().Y, enemy.GetSize().X, enemy.GetSize().Y))) 
                {
                    bullet.isActive = false; 
                    enemy.TakeDamage(20);    

                   
                    if (enemy.GetHealth() <= 0)
                    {
                        if (enemy is BasicEnemy)
                            updateScore(30); 
                        else if (enemy is FastEnemy)
                            updateScore(20); 
                        else if (enemy is StrongEnemy)
                            updateScore(50); 
                        else if (enemy is BossEnemy)
                            updateScore(100); 

                        player.Heal(5); 
                    }
                }
            }
        }

        
        enemies.RemoveAll(e => e.GetHealth() <= 0);
    }

   
    public static void CheckPlayerCollision(Spaceship player, List<Enemy> enemies)
    {
        List<Enemy> enemiesToRemove = new List<Enemy>();

        foreach (Enemy enemy in enemies)
        {
            if (Raylib.CheckCollisionRecs(
                new Rectangle(player.GetPosition().X, player.GetPosition().Y, player.GetSize().X, player.GetSize().Y),
                new Rectangle(enemy.GetPosition().X, enemy.GetPosition().Y, enemy.GetSize().X, enemy.GetSize().Y)))
            {
                if (enemy is BasicEnemy)
                    player.TakeDamage(20); 
                else if (enemy is FastEnemy)
                    player.TakeDamage(10); 
                else if (enemy is StrongEnemy)
                    player.TakeDamage(40); 
                else if (enemy is BossEnemy)
                    player.TakeDamage(60); 

                
                enemy.Destroy();
                enemiesToRemove.Add(enemy);
            }
        }

        
        foreach (var enemy in enemiesToRemove)
        {
            enemies.Remove(enemy);
        }
    }


  
    public static void CheckBossBulletCollision(List<Bullet> bullets, Spaceship player)
    {
        foreach (Bullet bullet in bullets)
        {
            if (Raylib.CheckCollisionRecs(
                new Rectangle(bullet.position.X, bullet.position.Y, 20, 5), 
                new Rectangle(player.GetPosition().X, player.GetPosition().Y, 130, 130))) 
            {
                bullet.isActive = false; 
                player.TakeDamage(20);   
            }
        }

        bullets.RemoveAll(b => !b.isActive);
    }
}

    
    
