namespace Runner
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var arrangements = new RabbitHouseParser().Parse(File.ReadAllLines("input.txt"));

            foreach (var arrangement in arrangements)
            {
                Console.WriteLine("OLD");
                arrangement.Visualise();
                Console.WriteLine();

                //var heightOfCell = arrangement[0, 0];
                //arrangement.SetHeightAt(0,0, 5);
                //arrangement.SetHeightAt(1, 0, 5);
                //bool isSafe = arrangement.IsSafe();
                //var changed = arrangement.GetTotalAddedBlocks();
                CreateSafeArrangement(arrangement);

                Console.WriteLine("NEW");
                arrangement.Visualise();
                Console.WriteLine();
                Console.WriteLine($"SAFE? => {arrangement.IsSafe()}");
                Console.WriteLine($"Blocks Added => {arrangement.GetTotalAddedBlocks()}");
                Console.WriteLine();
            }


            Console.WriteLine("Hello, World!");
        }

        public static RabbitHouseArrangement CreateSafeArrangement(RabbitHouseArrangement arrangement)
        {
            while(!arrangement.IsSafe())
            {
                for (int i = 0; i < arrangement.TotalColumns; i++)
                {
                    for(int j = 0; j < arrangement.TotalRows; j++)
                    {
                        Coordinates? up = null;
                        Coordinates? down = null;
                        Coordinates? left = null;
                        Coordinates? right = null;

                        if (i - 1 >= 0)
                        {
                            up = new Coordinates(i - 1, j);
                        }

                        if (i + 1 < arrangement.TotalColumns - 1)
                        {
                            down = new Coordinates(i + 1, j);
                        }

                        if (j - 1 >= 0)
                        {
                            left = new Coordinates(i, j - 1);
                        }

                        if (j + 1 < arrangement.TotalRows - 1)
                        {
                            right = new Coordinates(i, j + 1);
                        }

                        Coordinates?[] adjacents = { up, down, left, right };

                        var curr = arrangement[i, j];
                        var balance = curr - 1;
                        foreach(var cell in adjacents)
                        {
                            if (cell.HasValue && arrangement.GetHeightAt(cell.Value.x, cell.Value.y) < curr)
                            {
                                arrangement.SetHeightAt(cell.Value.x, cell.Value.y, balance);
                            }  
                        }
                    }
                }
        }

            return arrangement;
        }

        public struct Coordinates
        {
            public int x;
            public int y;

            public Coordinates(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }
    }
}
