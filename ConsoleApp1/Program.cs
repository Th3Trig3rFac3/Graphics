namespace ConsoleApp1
{
    // C# 7.0 program for implementing Sutherland–Hodgman
    // algorithm for polygon clipping
    internal class Program
    {
        const int MAX_POINTS = 20;
        static int y_iteration = 1;
        static int x_iteration = 1;

        // Returns x-value of point of intersection of two lines
        static int x_intersect(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4)
        {
            int num = (x1 * y2 - y1 * x2) * (x3 - x4) -
                (x1 - x2) * (x3 * y4 - y3 * x4);
            int den = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);
            /*
            Console.Write($"Num: {num} "); Console.Write($"Den: {den} "); Console.Write($"Num / Den: {num / den} "); Console.Write($"X Iteration: {x_iteration}"); x_iteration++;
            Console.WriteLine();
            */
            return num / den;
        }

        // Returns y-value of point of intersection of two lines
        static int y_intersect(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4) //Mi kerül bele?
        {
            int num = (x1 * y2 - y1 * x2) * (y3 - y4) -
                (y1 - y2) * (x3 * y4 - y3 * x4);
            int den = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);
            /*
            Console.Write($"Num: {num} "); Console.Write($"Den: {den} "); Console.Write($"Num / Den: {num / den} "); Console.Write($"Y Iteration: {y_iteration}"); y_iteration++;
            Console.WriteLine();
            */
            return num / den;
        }

        // This functions clips all the edges w.r.t one clip
        // edge of clipping area
        static void clip(ref int[][] poly_points, ref int poly_size,
                            ref int x1, ref int y1, ref int x2, ref int y2)
        {
            int[,] new_points = new int[MAX_POINTS, 2];
            int new_poly_size = 0;

            for (int i = 0; i < poly_size; i++)
            {
                // i and k form a line in polygon
                int k = (i + 1) % poly_size;
                int ix = poly_points[i][0], iy = poly_points[i][1];
                int kx = poly_points[k][0], ky = poly_points[k][1];

                // Calculating position of first point
                // w.r.t. clipper line
                int i_pos = (x2 - x1) * (iy - y1) - (y2 - y1) * (ix - x1);

                // Calculating position of second point
                // w.r.t. clipper line
                int k_pos = (x2 - x1) * (ky - y1) - (y2 - y1) * (kx - x1);

                // Case 1 : When both points are inside
                if (i_pos < 0 && k_pos < 0)
                {
                    //Only second point is added
                    new_points[new_poly_size, 0] = kx;
                    new_points[new_poly_size, 1] = ky;
                    new_poly_size++;
                }

                // Case 2: When only first point is outside
                else if (i_pos >= 0 && k_pos < 0)
                {
                    // Point of intersection with edge
                    // and the second point is added
                    new_points[new_poly_size, 0] = x_intersect(x1,
                                    y1, x2, y2, ix, iy, kx, ky);
                    new_points[new_poly_size, 1] = y_intersect(x1,
                                    y1, x2, y2, ix, iy, kx, ky);
                    new_poly_size++;

                    new_points[new_poly_size, 0] = kx;
                    new_points[new_poly_size, 1] = ky;
                    new_poly_size++;
                }

                // Case 3: When only second point is outside
                else if (i_pos < 0 && k_pos >= 0)
                {
                    //Only point of intersection with edge is added
                    new_points[new_poly_size, 0] = x_intersect(x1,
                                      y1, x2, y2, ix, iy, kx, ky);
                    new_points[new_poly_size, 1] = y_intersect(x1,
                                      y1, x2, y2, ix, iy, kx, ky);
                    new_poly_size++;
                }

                // Case 4: When both points are outside
                else
                {
                    // No points are added
                }
            }
            // Copying new points into original array
            // and changing the no. of vertices
            poly_size = new_poly_size;
            for (int i = 0; i < poly_size; i++)
            {
                poly_points[i][0] = new_points[i, 0];
                poly_points[i][1] = new_points[i, 1];
            }
        }

        // Implements Sutherland–Hodgman algorithm
        static void suthHodgClip(ref int[][] poly_points, ref int poly_size,
                  ref int[][] clipper_points, ref int clipper_size)
        {
            // i and k are two consecutive indexes
            for (int i = 0; i < clipper_size; i++)
            {
                int k = (i + 1) % clipper_size;

                // We pass the current array of vertices, it's size
                // and the end points of the selected clipper line
                clip(ref poly_points, ref poly_size,
                    ref clipper_points[i][0],
                    ref clipper_points[i][1],
                    ref clipper_points[k][0],
                    ref clipper_points[k][1]);
            }

            // Printing vertices of clipped polygon
            for (int i = 0; i < poly_size; i++)
                Console.WriteLine("({0},{1}) ", poly_points[i][0], poly_points[i][1]);
        }


        static void Main(string[] args)
        {
            // Defining polygon vertices in clockwise order
            int poly_size = 3;
            int[][] poly_points = new int[20][] {
               new int[2] { 100,150},
               new int[2] { 200,250},
               new int[2] { 300,200},
               new int[2] { 0,0},
               new int[2] { 0,0},
               new int[2] { 0,0},
               new int[2] { 0,0},
               new int[2] { 0,0},
               new int[2] { 0,0},
               new int[2] { 0,0},
               new int[2] { 0,0},
               new int[2] { 0,0},
               new int[2] { 0,0},
               new int[2] { 0,0},
               new int[2] { 0,0},
               new int[2] { 0,0},
               new int[2] { 0,0},
               new int[2] { 0,0},
               new int[2] { 0,0},
               new int[2] { 0,0},
            };

            // Defining clipper polygon vertices in clockwise order
            // 1st Example with square clipper
            /*
            int clipper_size = 4;
            int[][] clipper_points = new int[][]{ //square
                new int[2] { 150, 150 },
                new int[2] { 150, 200 },
                new int[2] { 200, 200 },
                new int[2] { 200, 150 }
            };
            */
            //Target output: (150, 162) (150, 200) (200, 200) (200, 174)


            // 2nd Example with triangle clipper
            
            int clipper_size = 3;
            int[][] clipper_points = new int[][] {
                new int[2] {100,300},
                new int[2] {300,300},
                new int[2] {200,100}
            };
            
            //Target output: (242, 185) (166, 166) (150, 200) (200, 250) (260, 220) 

            //Calling the clipping function
            suthHodgClip(ref poly_points, ref poly_size, ref clipper_points,
                         ref clipper_size);

            Console.ReadKey();
        }
    }
}