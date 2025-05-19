using System;
using System.Numerics;
using Raylib_cs;

namespace spacewarke
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Raylib.InitWindow(1920, 1080, "SpaceWar Game");
            Raylib.SetTargetFPS(60);

            
            Texture2D backgroundImage = Raylib.LoadTexture("C:/Users/alper/Desktop/KODMOD/Spaceresim/Giris.png");

            
            Rectangle startButton = new Rectangle(705, 625, 455, 205);
            bool isGameStarted = false;

            
            while (!Raylib.WindowShouldClose())
            {
                if (!isGameStarted && Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    Vector2 mousePos = Raylib.GetMousePosition();
                    if (Raylib.CheckCollisionPointRec(mousePos, startButton))
                    {
                        isGameStarted = true; 
                    }
                }
               
                Raylib.BeginDrawing();

                if (!isGameStarted)
                {
                    Raylib.DrawTexture(backgroundImage, 0, 0, Color.White);
                    Raylib.DrawRectangleRec(startButton, new Color(0, 0, 0, 0)); 
                }
                else
                {
                    Game game = new Game();
                    game.Run(); 

                    isGameStarted = false;
                    break; 
                }

                Raylib.EndDrawing();
            }

            Raylib.UnloadTexture(backgroundImage);
            Raylib.CloseWindow();
        }
    }
}
