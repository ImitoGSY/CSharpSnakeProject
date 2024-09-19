using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpSnakeProject.shared;

namespace CSharpSnakeProject.snake
{
    public class SnakeGameLogic : BaseGameLogic
    {
        private SnakeGamePlayState _gameplayState = new();


        public void GotoGameplay()
        {
            _gameplayState.FieldWidth = ScreenWidth;
            _gameplayState.FieldHeight = ScreenHeight;
            ChangeState(_gameplayState);
            _gameplayState.Reset();
        }
        public override void OnArrowUp()
        {
            if (CurrentState != _gameplayState) return;
            _gameplayState.SetDirection(SnakeDir.Up);
        }

        public override void OnArrowDown()
        {
            if (CurrentState != _gameplayState) return;
            _gameplayState.SetDirection(SnakeDir.Down);
        }

        public override void OnArrowLeft()
        {
            if (CurrentState != _gameplayState) return;
            _gameplayState.SetDirection(SnakeDir.Left);
        }

        public override void OnArrowRight()
        {
            if (CurrentState != _gameplayState) return;
            _gameplayState.SetDirection(SnakeDir.Right);
        }

        public override void Update(float deltaTime)
        {
            _gameplayState.Update(deltaTime);
            if (CurrentState != _gameplayState)
            {
                GotoGameplay();
            }
        }

        public override ConsoleColor[] CreatePalette()
        {
            return
            [
                ConsoleColor.Green,
                ConsoleColor.Red,
                ConsoleColor.White,
                ConsoleColor.Blue,
            ];
        }
    }
}
