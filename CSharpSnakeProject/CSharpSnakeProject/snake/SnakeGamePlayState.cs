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

        private SnakeDir _currentDir = SnakeDir.Right;
        private float _timeToMove = 0f;
        private List<Cell> _body = new();

        const char squareSymbol = '■';

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
            _currentDir = SnakeDir.Right;
            _body.Add(new(middleX + 3, middleY));
            _timeToMove = 0f;
        }

        public override void Update(float deltaTime)
        {
            _timeToMove -= deltaTime;
            if (_timeToMove > 0f)
                return;
            _timeToMove = 1f / 5;
            Cell head = _body[0];
            Cell nextCell = ShiftTo(head, _currentDir);
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
            foreach (Cell cell in _body)
            {
                renderer.SetPixel(cell.X, cell.Y, squareSymbol, 3);
            }
        }
    }
}
