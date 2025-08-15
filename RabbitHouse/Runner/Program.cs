namespace Runner
{
    internal partial class Program
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
                for (int row = 0; row < arrangement.TotalColumns; row++)
                {
                    for(int col = 0; col < arrangement.TotalRows; col++)
                    {
                        Coordinates? up = GetUpAdjacent(row, col);
                        Coordinates? down = GetDownAdjacent(arrangement, row, col);
                        Coordinates? left = GetLeftAdjacent(row, col);
                        Coordinates? right = GetRightAdjacent(arrangement, row, col);

                        Coordinates?[] adjacentCells = [up, down, left, right];

                        var curr = arrangement[row, col];
                        var balance = curr - 1;
                        foreach (var cell in adjacentCells)
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

        private static Coordinates? GetRightAdjacent(RabbitHouseArrangement arrangement, int row, int col)
        {
            return col + 1 < arrangement.TotalRows ? new Coordinates(row, col + 1) : null;
        }

        private static Coordinates? GetLeftAdjacent(int row, int col)
        {
            return col - 1 >= 0 ? new Coordinates(row, col - 1) : null;
        }

        private static Coordinates? GetDownAdjacent(RabbitHouseArrangement arrangement, int row, int col)
        {
            return row + 1 < arrangement.TotalColumns ? new Coordinates(row + 1, col) : null;
        }

        private static Coordinates? GetUpAdjacent(int row, int col)
        {
            return row - 1 >= 0 ? new Coordinates(row - 1, col) : null;
        }
    }
}
