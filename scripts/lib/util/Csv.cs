using System;
using System.Collections.Generic;
using Godot;

namespace Zona
{
    namespace Util
    {
        public sealed class CSVClass
        {
            public string[,] data;
            public int rows;
            public int cols;
            public CSVClass(string[,] data, int rows, int cols)
            {
                this.data = data;
                this.rows = rows;
                this.cols = cols;
            }
        }
        public sealed class CSVParser
        {
            public static CSVClass parse(string filepath)
            {
                File file = new File();
                file.Open(filepath, 1);
                string text = file.GetAsText();
                file.Close();
                //text.Replace('\n', '\r');
                string[] lines = text.Split(new string[]  {"\r", "\r\n", "\n"}, StringSplitOptions.None);
                int rows = lines.Length;
                int cols = lines[0].Split(',').Length;
                string[,] values = new string[rows, cols];
                // We skip the header row and set i to 1
                for(int i = 1; i < rows; i++)
                {

                    string[] row = lines[i].Split(',');
                    for(int j = 0; j < row.Length; j++)
                    {
                        values[i,j] = row[j];
                    }
                }
                CSVClass cls = new CSVClass(values, rows, cols);
                return cls;
            }
        }
    }
}