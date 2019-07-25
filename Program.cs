using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace searchplus
{
    public class Program
    {
        private int viewLines = 2;
        private string search = "";
        private string file = "";
        static void Main(string[] args)
        {
            var app = new Program();
            if (args.Length < 1)
            {
                Console.WriteLine("parameter <filename> missing");
                paramInfo();
                return;
            }
            else
            {
                app.file = args[0];
            }
            if (args.Length < 2)
            {
                Console.WriteLine("parameter <searchString> missing");
                paramInfo();
                return;
            }
            else
            {
                app.search = args[1];
            }
            if (args.Length == 3 )
            {
                try
                {
                    app.viewLines = Convert.ToInt32(args[2]);
                }
                catch (Exception)
                {
                }
            }
            analyze(app.file, app.search, app.viewLines);
            Console.WriteLine("done");

        }

        private static void analyze(string textFile, string search, int linesCount)
        {
            linesCount++;
            var readArray = new String[linesCount];

            int cnt = 0;
            int hits = 0;
            int displayNext = 0;
            using (var reader = new System.IO.StreamReader(textFile))
            {
                while ((readArray[cnt % linesCount] = reader.ReadLine()) != null)
                {
                    if (readArray[cnt % linesCount].Contains(search))
                    {
                        hits++;
                        var current = cnt % linesCount;
                        var back = ((cnt % linesCount) + 1) % linesCount;
                        for (int i = back; i < linesCount; i++)
                        {
                            Console.WriteLine(readArray[i]);
                        }
                        for (int i = 0; i <= (cnt % linesCount); i++)
                        {
                            if (i == current)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                            }
                            Console.WriteLine(readArray[i]);
                            Console.ResetColor();
                        }
                        displayNext = linesCount;
                    }
                    if (displayNext > 0) {
                        Console.WriteLine(readArray[cnt % linesCount]);
                    }
                    displayNext--;
                    cnt++;
                }
            }
            Console.Write(System.Environment.NewLine);
            Console.WriteLine($"Line count: {cnt:n}");
            Console.WriteLine($"Hit count: {hits:n}");
        }

        private static void paramInfo()
        {
            Console.WriteLine("searchplus <filename> <searchString> <displayLinesCount");
        }
    }
}
