using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using CSharpSnakeProject.shared;

namespace CSharpSnakeProject.snake
{
    public enum SnakeDir
    {
        Up, Down, Left, Right
    }
    public class SnakeGamePlayState : BaseGameState
    {
        public int FieldWidth { get; set; }
        public int FieldHeight { get; set; }
        public int Level { get; set; }
        public bool GameOver { get; private set; }
        public bool HasWon {  get; private set; }

        private SnakeDir _currentDir = SnakeDir.Right;
        private float _timeToMove = 0f;
        private List<Cell> _body = new();
        private Cell _apple = new();
        private Random _random = new();

        const char squareSymbol = '■';
        const char circleSymbol = '*';

        private struct Cell
        {
            public int X;
            public int Y;
            public Cell(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

       
        public void SetDirection(SnakeDir dir)
        {
            _currentDir = dir;
        }

        public override void Reset()
        {
            _body.Clear();
            int middleY = FieldHeight / 2;
            int middleX = FieldWidth / 2;
            GameOver = false;
            HasWon = false;
            _currentDir = SnakeDir.Right;
            _body.Add(new(middleX + 3, middleY));
            _apple = new(middleX - 3, middleY);
            _timeToMove = 0f;
        }

        public override void Update(float deltaTime)
        {
            _timeToMove -= deltaTime;
            if (_timeToMove > 0f || GameOver)
                return;

            _timeToMove = 1f / (5f + Level);
            Cell head = _body[0];
            Cell nextCell = ShiftTo(head, _currentDir);

            if (nextCell.Equals(_apple))
            {
                _body.Insert(0, _apple);
                HasWon = _body.Count >= Level + 3;
                GenerateApple();
                return;
            }
            if (nextCell.X < 0 || nextCell.Y < 0 || nextCell.X >= FieldWidth || nextCell.Y >= FieldHeight)
            {
                GameOver = true;
                return;
            }
            
            _body.RemoveAt(_body.Count - 1);
            _body.Insert(0, nextCell);
        }

        private Cell ShiftTo(Cell from, SnakeDir toDir)
        {
            switch (toDir)
            {
                case SnakeDir.Up:
                    return new Cell(from.X, from.Y - 1);
                case SnakeDir.Down:
                    return new Cell(from.X, from.Y + 1);
                case SnakeDir.Left:
                    return new Cell(from.X - 1, from.Y);
                case SnakeDir.Right:
                    return new Cell(from.X + 1, from.Y);
            }
            return from;
        }


        public override void Draw(ConsoleRenderer renderer)
        {
            renderer.DrawString($"Level: {Level}", 3, 1, ConsoleColor.White);
            renderer.DrawString($"Score: {_body.Count - 1}", 3, 2, ConsoleColor.White);

            foreach (Cell cell in _body)
            {
                renderer.SetPixel(cell.X, cell.Y, squareSymbol, 3);
            }
            renderer.SetPixel(_apple.X, _apple.Y, circleSymbol, 1);
        }

        private void GenerateApple()
        {
            Cell cell;
            cell.X = _random.Next(FieldWidth);
            cell.Y = _random.Next(FieldHeight);

            if (_body[0].Equals(cell))
            {
                if (cell.Y > FieldHeight / 2)
                {
                    cell.Y--;
                }
                else
                {
                    cell.Y++;
                }
            }
            _apple = cell;
        }
        public override bool IsDone()
        {
            return GameOver || HasWon;
        }
    }
}
