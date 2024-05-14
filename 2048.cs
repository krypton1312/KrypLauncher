using System;
using System.Text;
using System.Runtime.Serialization;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Game_2048
{
    public delegate void EndGameEventHandler();
    public delegate void ScoreOverflowEventHandler();
    public delegate void NewTileEventHandler(int x, int y);

    [Serializable]
    public sealed class _2048
    {
        public event EndGameEventHandler EndGameEvent;
        public event ScoreOverflowEventHandler ScoreOverflowEvent;
        public event NewTileEventHandler NewTileEvent;

        private int minValue;                   // Минимально выпадающий блок
        private int score;                      // Очки
        private int[,] matrix;                  // Сама матрица, содержащая структуру игры
        private LimitedStack<int[,]> history;   // Ограниченный стек действий
        private LimitedStack<int> scoreHistory; // Ограниченный стек очков
        private int _historyMaxLength = 5;       // Количество разрешенных отмен операций

        public _2048(int rows, int cells, int minValue = 2)
        {
            if (rows < 2 || cells < 2)
                throw new ArgumentException("Розмір матриці не може бути меншим 2х2.");

            this.minValue = minValue;
            New(rows, cells);
        }

        public bool TrySaveGame(String file)
        {
            if (file == null || !File.Exists(file)) return false;
            try
            {
                using (BinaryWriter bw = new BinaryWriter(new FileStream(file, FileMode.Truncate)))
                {
                    bw.Write(minValue);
                    bw.Write(score);
                    bw.Write(matrix.GetLength(0));
                    bw.Write(matrix.GetLength(1));
                    foreach (int i in matrix)
                        bw.Write(i);
                    bw.Write(_historyMaxLength);

                    return true;
                }
            }
            catch (IOException)
            {
                return false;
            }
            catch
            {
                return false;
            }
        }
        public bool TryLoadGame(String file)
        {
            if (file == null || !File.Exists(file)) return false;
            try
            {
                using (BinaryReader br = new BinaryReader(new FileStream(file, FileMode.OpenOrCreate)))
                {
                    minValue = br.ReadInt32();
                    score = br.ReadInt32();
                    matrix = new int[br.ReadInt32(), br.ReadInt32()];
                    for (int i = 0; i < matrix.GetLength(0); i++)
                    {
                        for (int j = 0; j < matrix.GetLength(1); j++)
                        {
                            matrix[i, j] = br.ReadInt32();
                        }
                    }
                    _historyMaxLength = br.ReadInt32();
                    return true;
                }
            }
            catch (IOException)
            {
                return false;
            }
            catch
            {
                return false;
            }
        }
        /// Генерация нового поля.

        private void New(int rows, int cells)
        {
            Random random = new Random();
            matrix = new int[rows, cells];
            int count = rows > cells ? random.Next(1, cells) : random.Next(1, rows);

            while (count != 0)
            {
                NewElement();
                count--;
            }
            history = new LimitedStack<int[,]>(_historyMaxLength);
            scoreHistory = new LimitedStack<int>(_historyMaxLength);
        }

        /// Генерация нового блока

        private void NewElement()
        {
            Random random = new Random();
            int size = matrix.Length;
            int rows = matrix.GetLength(0);
            int cells = matrix.GetLength(1);

            int x = random.Next(0, rows);
            int y = random.Next(0, cells);

            while (matrix[x, y] != 0)
            {
                x = random.Next(0, rows);
                y = random.Next(0, cells);
            }
            // Шанс выпадения удвоенного блока = 1/10
            if (random.Next(0, 10) == 1)
                matrix[x, y] = minValue + minValue;
            else
                matrix[x, y] = minValue;

            NewTileEvent?.Invoke(x, y);

            if (!_matrixOperations.CanBeMoved(matrix))
                EndGameEvent();
        }


        /// Получение копии матрицы игры.

        /// <returns>Матрица игры.</returns>
        public int[,] GetMatrix()
        {
            int[,] temporary = new int[matrix.GetLength(0), matrix.GetLength(1)];
            Array.Copy(matrix, temporary, matrix.Length);
            return temporary;
        }


        /// Сдвиг блоков вниз.


        public bool TryMoveDown()
        {
            return Move(down: true);
        }

        ///  Сдвиг блоков вверх.


        public bool TryMoveUp()
        {
            return Move(up: true);

        }

        ///  Сдвиг блоков влево.


        public bool TryMoveLeft()
        {
            return Move(left: true);
        }

        /// Сдвиг блоков вправо.


        public bool TryMoveRight()
        {
            return Move(right: true);
        }

        private bool Move(bool down = false, bool up = false, bool left = false, bool right = false)
        {
            if (!down && !up && !left && !right) throw new ArgumentException("1 argument must be true.");

            int rows = matrix.GetLength(0);
            int cells = matrix.GetLength(1);
            int moveScore = 0;
            bool oneMoved = false;

            int originalScore = score;
            int[,] original_matrix = new int[rows, cells];
            Array.Copy(matrix, original_matrix, matrix.Length);
            int untill;
            if (down || up) untill = cells;
            else untill = rows;
            for (int i = 0; i < untill; i++)
            {
                int[] line = null;
                int lineScore = 0;
                if (down)
                    line = _matrixOperations.SelectRow(matrix, 0, i, rows - 1, i);
                else if (up)
                    line = _matrixOperations.SelectRow(matrix, rows - 1, i, 0, i);
                else if (left)
                    line = _matrixOperations.SelectRow(matrix, i, cells - 1, i, 0);
                else if (right)
                    line = _matrixOperations.SelectRow(matrix, i, 0, i, cells - 1);
                try
                {
                    lineScore = _matrixOperations.LineShiftAttempt(line);
                }
                catch (OverflowException)
                {
                    ScoreOverflowEvent?.Invoke();
                }
                if (lineScore >= 0)
                {
                    oneMoved = true;
                    if (down)
                        _matrixOperations.SetLine(matrix, line, 0, i, rows - 1, i);
                    else if (up)
                        _matrixOperations.SetLine(matrix, line, rows - 1, i, 0, i);
                    else if (left)
                        _matrixOperations.SetLine(matrix, line, i, cells - 1, i, 0);
                    else if (right)
                        _matrixOperations.SetLine(matrix, line, i, 0, i, cells - 1);
                    moveScore += lineScore;
                }
            }
            if (oneMoved)
            {
                try
                {
                    score += moveScore;
                }
                catch (OverflowException)
                {
                    ScoreOverflowEvent?.Invoke();
                }
                history.Push(original_matrix);
                scoreHistory.Push(originalScore);
                NewElement();
                return true;
            }
            return false;
        }


        /// Возвращает true, если разрешено отменить ход.

        public bool UndoAllowed()
        {
            return history.Count == 0 ? false : true;
        }

        /// Отмена одного хода.

        public void Undo()
        {
            if (history.Count == 0)
                return;

            matrix = history.Pop();
            score = scoreHistory.Pop();
        }

        public int Score => score;
        public int RowCount => matrix.GetLength(0);
        public int ColumnCount => matrix.GetLength(1);

    }

    /*Некоторые операции с матрицей*/
    public static class _matrixOperations
    {
        // Выделение строки матрицы по заданным координатам.
        public static int[] SelectRow(int[,] _matrix, int x1, int y1, int x2, int y2)
        {
            if (_matrix == null) return null;

            if (x1 == x2)
            {
                int[] row = new int[Math.Abs(y2 - y1) + 1];
                if (y1 > y2)
                {
                    for (int i = y1, j = 0; i >= y2; i--, j++)
                    {
                        row[j] = _matrix[x1, i];
                    }
                }
                else
                {
                    for (int i = y1, j = 0; i <= y2; i++, j++)
                    {
                        row[j] = _matrix[x1, i];
                    }
                }
                return row;
            }
            else if (y1 == y2)
            {
                int[] row = new int[Math.Abs(x2 - x1) + 1];
                if (x1 > x2)
                {
                    for (int i = x1, j = 0; i >= x2; i--, j++)
                    {
                        row[j] = _matrix[i, y1];
                    }
                }
                else
                {
                    for (int i = x1, j = 0; i <= x2; i++, j++)
                    {
                        row[j] = _matrix[i, y1];
                    }
                }
                return row;
            }
            else
                throw new ArgumentException("Координати, що передаються, повинні утворювати рядок.");
        }

        // Установка строки матрицы.
        public static void SetLine(int[,] _matrix, int[] line, int x1, int y1, int x2, int y2)
        {
            if (line == null) return;

            if (x1 == x2)
            {
                if (y1 > y2)
                {
                    for (int i = y1, j = 0; i >= y2; i--, j++)
                    {
                        _matrix[x1, i] = line[j];
                    }
                }
                else
                {
                    for (int i = y1, j = 0; i <= y2; i++, j++)
                    {
                        _matrix[x1, i] = line[j];
                    }
                }
            }
            else if (y1 == y2)
            {
                if (x1 > x2)
                {
                    for (int i = x1, j = 0; i >= x2; i--, j++)
                    {
                        _matrix[i, y1] = line[j];
                    }
                }
                else
                {
                    for (int i = x1, j = 0; i <= x2; i++, j++)
                    {
                        _matrix[i, y1] = line[j];
                    }
                }
            }
            else
                throw new ArgumentException("Координати, що передаються, повинні утворювати рядок.");
        }

        // Сдвиг одной линии матрицы по правилам игры.
        public static int LineShiftAttempt(int[] line)
        {
            if (line == null || line.Length < 2)
                throw new ArgumentException("Aray length must be more than 1");

            bool[] connected = new bool[line.Length];
            bool moved = false;
            int score = 0;

            for (int i = 0; i < line.Length; i++)
            {
                for (int j = line.Length - 1; j > 0; j--)
                {
                    if (line[j - 1] != 0 && line[j - 1] == line[j] && !connected[j] && !connected[j - 1])
                    {
                        line[j] = checked(line[j] + line[j]);
                        line[j - 1] = 0;
                        score += line[j];
                        connected[j] = true;
                        moved = true;
                    }
                    else if (line[j - 1] != 0 && line[j] == 0)
                    {
                        line[j] = line[j - 1];
                        line[j - 1] = 0;
                        moved = true;
                    }
                }
            }
            return moved ? score : -1;
        }

        // Можно ли сдвинуть матрицу в какую-либо сторону по правилам игры.
        public static bool CanBeMoved(int[,] _matrix)
        {
            if (_matrix == null) return false;

            for (int i = 0; i < _matrix.GetLength(0); i++)
            {
                for (int j = 0; j < _matrix.GetLength(1); j++)
                {
                    if ((i > 0 && ((_matrix[i - 1, j] == _matrix[i, j]) || _matrix[i - 1, j] == 0)) ||
                        (i < _matrix.GetLength(0) - 1 && ((_matrix[i + 1, j] == _matrix[i, j]) || _matrix[i + 1, j] == 0)) ||
                        (j > 0 && ((_matrix[i, j - 1] == _matrix[i, j]) || _matrix[i, j - 1] == 0)) ||
                        (j < _matrix.GetLength(1) - 1 && ((_matrix[i, j + 1] == _matrix[i, j]) || _matrix[i, j + 1] == 0)))
                        return true;
                }
            }
            return false;
        }
    }

    /*Медленная реализация ограниченного стека*/
    public class LimitedStack<T> : Stack<T>
    {
        private int maxLength;

        public LimitedStack(int maxLength)
        {
            this.maxLength = maxLength;
        }

        new public void Push(T element)
        {
            if (Count == maxLength)
            {
                List<T> tempList = this.ToList<T>();
                for (int i = tempList.Count - 1; i > 0; i--)
                {
                    tempList[i] = tempList[i - 1];
                }
                for (int i = Count - 1; i > 0; i--)
                {
                    tempList[i] = tempList[i - 1];
                }
                tempList[0] = element;
                Clear();
                for (int i = tempList.Count - 1; i >= 0; i--)
                {
                    base.Push(tempList[i]);
                }

            }
            else
                base.Push(element);
        }
    }
}