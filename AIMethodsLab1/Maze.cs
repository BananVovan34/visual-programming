using System;
using System.Collections.Generic;
using System.Threading;

namespace visualprogramming.AIMethodsLab1
{
    public class Maze
    {
        public static int[,] maze =
        {
            {0,0,1,0,0,0,1,0,0,0},
            {1,0,1,0,1,0,1,0,1,0},
            {0,0,0,0,1,0,0,0,1,0},
            {1,1,1,0,1,1,1,0,1,0},
            {0,0,0,0,1,0,0,0,0,0},
            {0,1,0,1,1,1,0,1,1,0},
            {0,0,0,0,0,1,0,0,0,0},
            {0,1,1,1,0,1,0,1,0,1},
            {0,0,0,1,0,0,0,0,0,1},
            {0,1,0,0,0,1,0,1,0,0}
        };

        public static int n = 10;
        public static int m = 10;

        public static List<List<Point>> allPaths = new List<List<Point>>();
        public static List<List<Point>> allCorrectPaths = new List<List<Point>>();

        public static void AiMethodsLab1()
        {
            bool[,] visited = new bool[n, m];
            var path = new List<Point>();

            FindPaths(0, 0, path, visited);

            Console.WriteLine();
            Console.WriteLine($"Всего найдено путей: {allPaths.Count}");
            Console.WriteLine($"Количество верных путей: {allCorrectPaths.Count}");
        }

        static void FindPaths(int x, int y, List<Point> path, bool[,] visited)
        {
            if (x < 0 || y < 0 || x >= n || y >= m || maze[x, y] == 1 || visited[x, y])
            {
                return;
            }
            path.Add(new Point(x, y));
            visited[x, y] = true;

            PrintMaze(path);
            //Thread.Sleep(50);

            if (x == n - 1 && y == m - 1)
            {
                var successPath = new List<Point>(path);
                allCorrectPaths.Add(successPath);
                allPaths.Add(successPath);
                Console.WriteLine("Найден правильный путь!");
                //Thread.Sleep(100);
            }
            else
            {
                bool deadEnd = true;

                var moves = new List<Point>
                {
                    new Point(x+1, y), // вниз
                    new Point(x-1, y), // вверх
                    new Point(x, y+1), // вправо
                    new Point(x, y-1)  // влево
                };

                moves.Sort((a, b) =>
                    Heuristic(a.X, a.Y).CompareTo(Heuristic(b.X, b.Y)));

                foreach (var move in moves)
                {
                    int before = allCorrectPaths.Count;
                    FindPaths(move.X, move.Y, path, visited);
                    if (allCorrectPaths.Count > before) 
                        deadEnd = false;
                }

                if (deadEnd)
                {
                    allPaths.Add(new List<Point>(path));
                }
            }

            path.RemoveAt(path.Count - 1);
            visited[x, y] = false;
        }

        static int Heuristic(int x, int y)
        {
            return Math.Abs((n - 1) - x) + Math.Abs((m - 1) - y);
        }

        static void PrintMaze(List<Point> path)
        {
            Console.Clear();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (maze[i, j] == 1)
                        Console.Write("█ ");
                    else if (i == 0 && j == 0)
                        Console.Write("S ");
                    else if (i == n - 1 && j == m - 1)
                        Console.Write("F ");
                    else if (path.Contains(new Point(i, j)))
                        Console.Write("* ");
                    else
                        Console.Write("· ");
                }
                Console.WriteLine();
            }
        }
        
        public struct Point : IEquatable<Point>
        {
            public int X;
            public int Y;
            
            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public bool Equals(Point other) => X == other.X && Y == other.Y;
            public override bool Equals(object obj) => obj is Point p && Equals(p);
            public override int GetHashCode() => (X << 16) ^ Y;
        }
    }
}
