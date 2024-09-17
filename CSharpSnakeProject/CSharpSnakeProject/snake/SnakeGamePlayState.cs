using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using mySnake.shared;
using Shared;

namespace mySnake.snake
{
    internal enum SnakeDir
    {
        Up, Down, Left, Right
    }
    internal class SnakeGamePlayState : BaseGameState
    {
        private struct Cell
        {
            public int x; public int y;

            public Cell(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        private List<Cell> _body = new();
        private SnakeDir currentDir = SnakeDir.Left;

        private float _timeToMove = 0f;

        public void SetDirection(SnakeDir dir)
        {
            currentDir = dir;
        }

        public override void Reset()
        {
            _body.Clear();
            currentDir = SnakeDir.Left;
            _body.Add(new(0, 0));
            _timeToMove = 0f;
        }

        public override void Update(float deltaTime)
        {
            _timeToMove -= deltaTime;
            if (_timeToMove > 0f)
                return;

            _timeToMove = 1f / 4;
            var head = _body[0];
            var nextCell = ShiftTo(head, currentDir);

            _body.RemoveAt(_body.Count - 1);
            _body.Insert(0, nextCell);

            Console.WriteLine($"{_body[0].x}, {_body[0].y}");
        }

        private Cell ShiftTo(Cell from, SnakeDir toDir)
        {
            switch (toDir)
            {
                case SnakeDir.Up:
                    return new Cell(from.x, from.y + 1);
                case SnakeDir.Down:
                    return new Cell(from.x, from.y - 1);
                case SnakeDir.Left:
                    return new Cell(from.x - 1, from.y);
                case SnakeDir.Right:
                    return new Cell(from.x + 1, from.y);
            }

            return from;
        }


    }
}
