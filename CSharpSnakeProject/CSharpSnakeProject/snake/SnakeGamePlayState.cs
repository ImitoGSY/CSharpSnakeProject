using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using mySnake.shared;

namespace mySnake.snake
{
    public enum SnakeDir
    {
        Up, Down, Left, Right
    }
    public class SnakeGameplayState : BaseGameState
    {
        private SnakeDir _currentDir = SnakeDir.Right;
        private float _timeToMove = 0f;
        private List<Cell> _body = new();
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

        private Cell ShiftTo(SnakeDir dir, Cell curCell)
        {
            switch (dir)
            {
                case SnakeDir.Up:
                    return new Cell(curCell.X, curCell.Y + 1);
                    break;
                case SnakeDir.Down:
                    return new Cell(curCell.X, curCell.Y - 1);
                    break;
                case SnakeDir.Left:
                    return new Cell(curCell.X - 1, curCell.Y);
                    break;
                case SnakeDir.Right:
                    return new Cell(curCell.X + 1, curCell.Y);
                    break;
            }
            return curCell;
        }
        public override void Reset()
        {
            _body.Clear();
            _currentDir = SnakeDir.Right;
            _body.Add(new Cell(0, 0));
            _timeToMove = 0f;
        }

        public override void Update(float deltaTime)
        {

            _timeToMove -= deltaTime;
            if (_timeToMove > 0)
                return;
            else
                _timeToMove = 1f / 5;

            Cell head = _body[0];
            Cell nextCell = ShiftTo(_currentDir, head);
            _body.RemoveAt(_body.Count - 1);
            _body.Insert(0, nextCell);
            Console.WriteLine($"_body_X {_body[0].X}, _body_Y {_body[0].Y}");
        }
    }
}
