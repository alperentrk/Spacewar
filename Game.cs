using System;
using System.Numerics;
using Raylib_cs;
using System.IO;
using spacewarke;
using spacewarke.Enemies;

public class Game
{
    private bool isRunning;
    private bool isGameOver;
    private bool isGameWon;
    private Spaceship player;
    private List<Enemy> enemies;
    private Random Random;
    private float spawnTimer;
    private int score;
    private int level;

    Texture2D GameBackground;
    Rectangle retryButton = new Rectangle(850, 600, 200, 50);

    public Game()
    {
        StartGame();
    }

    private void StartGame()
    {
        isRunning = true;
        isGameOver = false;
        isGameWon = false;
        player = new Spaceship(120, 540, "C:/Users/alper/Desktop/KODMOD/Spaceresim/Gemi.png");
        enemies = new List<Enemy>();
        Random = new Random();
        spawnTimer = 0;
        score = 0;
        level = 1;
        GameBackground = Raylib.LoadTexture("C:/Users/alper/Desktop/KODMOD/Spaceresim/Oyun.png");
    }

    public void Run()
    {
        while (!Raylib.WindowShouldClose())
        {
            if (isGameWon)
            {
                DrawWinScreen();
            }
            else if (isGameOver)
            {
                DrawGameOverScreen();
            }
            else if (isRunning)
            {
                UpdateGame();
                DrawGame();
            }
        }
    }

    private void UpdateGame()
    {
        float deltaTime = Raylib.GetFrameTime();
        spawnTimer += deltaTime;

        if (spawnTimer > 2.0f)
        {
            AddRandomEnemy();
            spawnTimer = 0;
        }

        player.Move();
        player.Shoot();
        player.UpdateBullets();

        foreach (Enemy enemy in enemies)
        {
            enemy.Move();

            if (enemy is BossEnemy bossEnemy)
            {
                bossEnemy.Attack(player);
                bossEnemy.GetBullets().ForEach(b => b.Move());
                CollisionDetector.CheckBossBulletCollision(bossEnemy.GetBullets(), player);
            }
            else if (enemy is StrongEnemy strongEnemy)
            {
                strongEnemy.Attack(player);
                strongEnemy.GetBullets().ForEach(b => b.Move());
                CollisionDetector.CheckBossBulletCollision(strongEnemy.GetBullets(), player);
            }
        }

        CheckCollisions();

        if (player.GetHealth() <= 0)
        {
            EndGame();
        }

        if (score >= 1500)
        {
            isGameWon = true;
            isRunning = false;
            SaveScore();
        }
        else if (score >= 800)
        {
            level = 3;
        }
        else if (score >= 300)
        {
            level = 2;
        }
        else
        {
            level = 1;
        }
    }

    private void CheckCollisions()
    {
        CollisionDetector.CheckBulletCollision(player.bullets, enemies, player, UpdateScore);
        CollisionDetector.CheckPlayerCollision(player, enemies);
    }

    private void EndGame()
    {
        isRunning = false;
        isGameOver = true;
        SaveScore();
    }

    private void SaveScore()
    {
        string filePath = "C:\\Users\\alper\\Desktop\\spacewarke";
        try
        {
            File.AppendAllText(filePath, $"Score: {score}, Date: {DateTime.Now}\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Skor kaydedilirken hata oluştu: {ex.Message}");
        }
    }   

    private void DrawGameOverScreen()
    {
        Raylib.BeginDrawing();
        Raylib.DrawText("OYUNU KAYBETTINIZ!", 700, 500, 40, Color.Red);
        Raylib.DrawRectangleRec(retryButton, Color.DarkGray);
        Raylib.DrawText("Tekrar Oyna", 870, 615, 20, Color.White);

        if (Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            Vector2 mousePos = Raylib.GetMousePosition();
            if (Raylib.CheckCollisionPointRec(mousePos, retryButton))
            {
                StartGame();
            }
        }
        Raylib.EndDrawing();
    }

    private void DrawGame()
    {
        Raylib.BeginDrawing();
        Raylib.DrawTexture(GameBackground, 0, 0, Color.White);

        player.Draw();

        foreach (Enemy enemy in enemies)
        {
            enemy.Draw();

            if (enemy is BossEnemy bossEnemy)
            {
                bossEnemy.Draw();
            }
            else if (enemy is StrongEnemy strongEnemy)
            {
                strongEnemy.Draw();
            }
        }

        Raylib.DrawText($"Score: {score}", 20, 50, 20, Color.White);
        Raylib.DrawText($"Level: {level}", 20, 80, 20, Color.White);

        Raylib.EndDrawing();
    }

    private void DrawWinScreen()
    {
        Raylib.BeginDrawing();
        Raylib.DrawText("TEBRİKLER OYUNU KAZANDINIZ!!", 670, 500, 40, Color.Green);
        Raylib.DrawRectangleRec(retryButton, Color.DarkGray);
        Raylib.DrawText("Tekrar Oyna", 870, 615, 20, Color.White);

        if (Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            Vector2 mousePos = Raylib.GetMousePosition();
            if (Raylib.CheckCollisionPointRec(mousePos, retryButton))
            {
                StartGame();
            }
        }
        Raylib.EndDrawing();
    }

    private void AddRandomEnemy()
    {
        int enemyType = 0;

        if (level == 1)
        {
            enemyType = Random.Next(0, 2);
        }
        else if (level == 2)
        {
            enemyType = Random.Next(0, 3);
        }
        else if (level == 3)
        {
            enemyType = Random.Next(0, 4);
        }

        float startY = Random.Next(50, Raylib.GetScreenHeight() - 100);

        switch (enemyType)
        {
            case 0:
                enemies.Add(new BasicEnemy(1920, startY));
                break;
            case 1:
                enemies.Add(new FastEnemy(1920, startY));
                break;
            case 2:
                enemies.Add(new StrongEnemy(1920, startY));
                break;
            case 3:
                enemies.Add(new BossEnemy(1920, startY));
                break;
        }
    }

    private void UpdateScore(int points)
    {
        score += points;
    }
}
