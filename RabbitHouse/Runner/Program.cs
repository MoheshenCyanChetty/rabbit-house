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

                CreateSafeArrangement(arrangement);

                Console.WriteLine("NEW");
                arrangement.Visualise();
                Console.WriteLine();
                var safeMessage = arrangement.IsSafe() ? "SAFE" : "NOT SAFE";

                Console.WriteLine($"This arrangement is {safeMessage}");
                Console.WriteLine($"Blocks Added => {arrangement.GetTotalAddedBlocks()}");
                Console.WriteLine();
            }


            Console.WriteLine("Hello, World!");
        }

        public static RabbitHouseArrangement CreateSafeArrangement(RabbitHouseArrangement arrangement)
        {
            while(!arrangement.IsSafe())
            {
                for (int row = 0; row < arrangement.TotalRows; row++)
                {
                    for(int col = 0; col < arrangement.TotalColumns; col++)
                    {
                        Coordinates?[] adjacentCells = GetAdjacentCells(row, col, arrangement);

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

        private static Coordinates?[] GetAdjacentCells(int row, int col, RabbitHouseArrangement arrangement)
        {
            Coordinates? up = row - 1 >= 0 ? new Coordinates(row - 1, col) : null;
            Coordinates? down = row + 1 < arrangement.TotalRows ? new Coordinates(row + 1, col) : null;
            Coordinates? left = col - 1 >= 0 ? new Coordinates(row, col - 1) : null;
            Coordinates? right = col + 1 < arrangement.TotalColumns ? new Coordinates(row, col + 1) : null;

            return [up, down, left, right];
        }
    }
}
