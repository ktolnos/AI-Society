using System;

namespace AiSociety
{
    public class Entity
    {
        public int Age { get; private set; }
        public double Race {  get; private set; }

        private Random rd;

        public Entity()
        {
            rd = new Random();
        }

        public void FillRandom()
        {
            Race = rd.NextDouble();
        }

        public void Live()
        {
            Age++;
        }
    }
}