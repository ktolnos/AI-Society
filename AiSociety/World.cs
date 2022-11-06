using System;
using System.Diagnostics;
using Eto.Drawing;

namespace AiSociety
{
    public class World
    {

        public int Width;
        public int Height;
        public int StartPopulation;

        private Entity[,] _entities;

        public Graphics Graphics;

        private Random rd;

        public World(int width = 100, int height = 100, int startPopulation = 200, Graphics graphics = null)
        {
            Width = width;
            Height = height;
            StartPopulation = startPopulation;
            Graphics = graphics;
        
            rd = new Random(13123);
            _entities = new Entity[Width, Height];
            Console.Write(Width +",  "+ Height);
            var entityProbability = (float) StartPopulation / Height / Width;
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    if (rd.NextDouble() < entityProbability)
                    {
                        var e = new Entity();
                        e.FillRandom();
                        _entities[i, j] = e;
                    }
                }
            }
        }

        public void Draw()
        {
            if (Graphics == null)
            {
                return;
            }
            
            Stopwatch sw = new Stopwatch();

            sw.Start();

            World w = new World(1000, 1000, 10000);

            sw.Stop();

            Console.WriteLine("Elapsed={0}",sw.Elapsed);
            
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    if (_entities[i, j] != null)
                    {
                        Graphics.DrawRectangle(new Color((float)_entities[i, j].Race, 0.2f, 0.3f), i, j, 1, 1);
                    }
                }
            }
        }
    }
}