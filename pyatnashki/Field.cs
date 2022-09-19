using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace pyatnashki
{
    public class Field
    {
        List<ColumnDefinition> columnDefinitions = new List<ColumnDefinition>();
        List<RowDefinition> rowDefinitions = new List<RowDefinition>();

        Tile[,] tiles = null;

        int emptyRow = 0, emptyColumn = 0;

        public Field ()
        {
            EventHandler.GetInstance().onGetFieldWidth += GetFieldWidth;
        }

        public int GetFieldWidth()
        {
            return columnDefinitions.Count;
        }

        public bool CreateField(Grid grid, int rows, int columns)
        {
            tiles = new Tile[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                rowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(rowDefinitions[i]);
            }

            for (int i = 0; i < columns; i++)
            {
                columnDefinitions.Add(new ColumnDefinition());
                grid.ColumnDefinitions.Add(columnDefinitions[i]);
            }

            return RandomTiles(grid);
        }

        public bool UpdateField(Grid grid, int x, int y)
        {
            if (x == 0 && y == 0)
            {
                x = rowDefinitions.Count;
                y = columnDefinitions.Count;
            }

            rowDefinitions.Clear();
            columnDefinitions.Clear();

            grid.RowDefinitions.Clear();
            grid.ColumnDefinitions.Clear();

            grid.Children.Clear();

            return CreateField(grid, x, y);
        }

        public bool Move(Tile tile)
        {
            if (!CheckForMovable(tile)) return false;

            SendMoveToTiles(tile);

            return true;
        }

        private bool CheckForMovable(Tile tile)
        {
            if (Grid.GetRow(tile.button) == emptyRow || Grid.GetColumn(tile.button) == emptyColumn)
                return true;

            return false;
        }

        private void SendMoveToTiles(Tile tile)
        {
            int tileRow = Grid.GetRow(tile.button);
            int tileColumn = Grid.GetColumn(tile.button);
            int rowDirection = emptyRow - tileRow;
            int columnDirection = emptyColumn - tileColumn;

            for (int r = 0, rMax = Math.Abs(rowDirection); r < rMax; r++)
            {
                if (rowDirection > 0)
                {
                    SendMove(tiles[tileRow + rowDirection - 1 - r, tileColumn]);
                }
                if (rowDirection < 0)
                {
                    SendMove(tiles[tileRow + rowDirection + 1 + r, tileColumn]);
                }
            }

            for (int c = 0, cMax = Math.Abs(columnDirection); c < cMax; c++)
            {
                if (columnDirection > 0)
                {
                    SendMove(tiles[tileRow, tileColumn + columnDirection - 1 - c]);
                }
                if (columnDirection < 0)
                {
                    SendMove(tiles[tileRow, tileColumn + columnDirection + 1 + c]);
                }
            }
        }

        private void SendMove(Tile tile)
        {
            int tempRow = Grid.GetRow(tile.button),
                tempColumn = Grid.GetColumn(tile.button);

            EventHandler.GetInstance().OnAddScores();

            tiles[emptyRow, emptyColumn] = tiles[tempRow, tempColumn];
            tiles[tempRow, tempColumn] = null;
            tile.Move(emptyRow, emptyColumn);
            emptyRow = tempRow;
            emptyColumn = tempColumn;
        }

        private List<string> OrderedTiles()
        {
            List<string?> orderTiles = new List<string?>();

            for (int i = 1; i < tiles.Length; i++)
            {
                orderTiles.Add(i.ToString());
            }

            orderTiles.Add(null);

            return orderTiles;
        }

        private bool RandomTiles(Grid grid)
        {
            Random random = new Random();
            List<string> orderedTiles = OrderedTiles();

            for (int r = 0; r < rowDefinitions.Count; r++)
            {
                for (int c = 0; c < columnDefinitions.Count; c++)
                {
                    int rNum = random.Next(0, orderedTiles.Count);

                    if (orderedTiles[rNum] == null)
                    {
                        tiles[r, c] = null;
                        orderedTiles.RemoveAt(rNum);

                        emptyRow = r;
                        emptyColumn = c;

                        continue;
                    }

                    var t = new Tile(r, c, orderedTiles[rNum]);
                    tiles[r, c] = t;
                    grid.Children.Add(t.button);

                    orderedTiles.RemoveAt(rNum);
                }
            }

            var inversions = SumInversions();

            if (inversions % 2 != 0)
            {
                ReplaceTwoTiles();
            }

            return true;
        }

        private int CountInversions(int _r, int _c)
        {
            int inversions = 0;
            int tileNumber = _r * columnDefinitions.Count + _c;
            int tileValue = 0;
            int.TryParse(tiles[_r, _c].button.Content.ToString(), out tileValue);

            for (int r = 0, rMax = (int) Math.Floor((double) tileNumber / columnDefinitions.Count); r <= rMax; r++) // CHEK
            {
                for (int c = 0, cMax = (rMax - r) > 0 ? (columnDefinitions.Count - 1) : (tileNumber % columnDefinitions.Count); c <= cMax; c++)
                {
                    if (tiles[r, c] == null)
                        continue;

                    int tileToChek = 0;
                    int.TryParse(tiles[r, c].button.Content.ToString(), out tileToChek);

                    if (tileValue < tileToChek)
                    {
                        inversions++;
                    }
                }
            }

            return inversions;
        }

        private int SumInversions()
        {
            int inversions = 0;

            for (int r = 0; r < rowDefinitions.Count; r++)
            {
                for (int c = 0; c < columnDefinitions.Count; c++)
                {
                    if (tiles[r, c] != null)
                        inversions += CountInversions(r, c);

                    if (tiles[r, c] == null)
                    {
                        if (rowDefinitions.Count % 2 == 0 && columnDefinitions.Count % 2 == 0)
                            inversions += 1 + (r * columnDefinitions.Count + c) / columnDefinitions.Count;

                        if (rowDefinitions.Count % 2 != 0 && columnDefinitions.Count % 2 == 0)
                            inversions += (r * columnDefinitions.Count + c) / columnDefinitions.Count;
                    }
                }
            }

            return inversions;
        }

        private void ReplaceTwoTiles ()
        {
            Tile temporary = null;

            int tileIndex = 0;

            while (temporary == null)
            {
                int r1 = (int)Math.Floor((double)tileIndex / rowDefinitions.Count),
                    c1 = tileIndex % columnDefinitions.Count,
                    r2 = (int)Math.Floor((double)(tileIndex + 1) / rowDefinitions.Count), 
                    c2 = (tileIndex + 1) % columnDefinitions.Count;
                if (tiles[r1, c1] != null
                    && tiles[r2, c2] != null)
                {
                    temporary = tiles[r1, c1];

                    tiles[r1, c1].Move(r2, c2);
                    tiles[r2, c2].Move(r1, c1);

                    tiles[r1, c1] = tiles[r2, c2];
                    tiles[r2, c2] = temporary;

                    return;
                }

                tileIndex++;
            }
        }

        public int GetRows() => rowDefinitions.Count;

        public int GetColumns() => columnDefinitions.Count;
    }
}
