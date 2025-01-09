using System.Numerics;
using System.Runtime.InteropServices;
using Raylib_cs;
using static Raylib_cs.Raylib;

public static class MainClass{

    public static float zoom = 400;
    public static float xOffset = 0;
    public static float yoffset = 0;

    public static void Main(){
        InitWindow(1000,1000, "Mandelbrot set");

        while(!WindowShouldClose()){
            BeginDrawing();

            if(IsKeyDown(KeyboardKey.Left)){
                xOffset -= 0.1f;
            }
            if(IsKeyDown(KeyboardKey.Right)){
                xOffset += 0.1f;
            }
            if(IsKeyDown(KeyboardKey.Up)){
                xOffset -= 0.1f;
            }
            if(IsKeyDown(KeyboardKey.Down)){
                xOffset += 0.1f;
            }
            if(IsKeyDown(KeyboardKey.W)){
                zoom += 10;
            }
            if(IsKeyDown(KeyboardKey.S)){
                zoom -= 10;
            }

            Image image = GenImageColor(1000, 1000, Color.Black);

            for(int x = 0; x < 1000; x++){
                for(int y = 0; y < 1000; y++){

                    float val = Mandelbrot((x - 650) / zoom + xOffset, (y - 500) / zoom + yoffset);

                    Color c = new Color(0, val / maxDepth, val / maxDepth);
                    

                    ImageDrawPixel(ref image, x, y, c);
                }
            }

            Texture2D texture2D = LoadTextureFromImage(image);

            DrawTexture(texture2D, 0, 0, Color.White);

            EndDrawing();
        }

        CloseWindow();
    }

    public static int maxDepth = 100;
    public static float escapeDist = 2;
    public static float Mandelbrot(float x, float y){
        bool isComplete = false;
        int i = 0;

        Complex c = new Complex(x, y);
        Complex z = new Complex(0, 0) + c;

        while(!isComplete){
            if(z.Magnitude > escapeDist){
                return i;
            }
            if(i >= maxDepth){
                return 0;
            }

            z = z * z + c;
            i++;
        }

        return 0;
    }
}