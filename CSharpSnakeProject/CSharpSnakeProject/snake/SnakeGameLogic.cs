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
        private bool _newGamePending = false;
        private int _currLevel = 0;
        private ShowTextState _showTextState = new(2f);


        public void GotoGameplay()
        {
            _gameplayState.Level = _currLevel;
            _gameplayState.FieldWidth = ScreenWidth;
            _gameplayState.FieldHeight = ScreenHeight;
            ChangeState(_gameplayState);
            _gameplayState.Reset();
        }

        private void GotoGameOver()
        {
            _currLevel = 0;
            _newGamePending = true;
            _showTextState.Text = $"Game Over!!!";
            ChangeState(_showTextState);
        }

        private void GotoNextLevel()
        {
            _currLevel++;
            _newGamePending = false;
            _showTextState.Text = $"Level {_currLevel}";
            ChangeState(_showTextState);
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
            if (CurrentState != null && !CurrentState.IsDone())
                return;
            if (CurrentState == null || CurrentState == _gameplayState && !_gameplayState.GameOver)
            {
                GotoNextLevel();
            }
            else if (CurrentState == _gameplayState && _gameplayState.GameOver)
            {
                GotoGameOver();
            }
            else if (CurrentState != _gameplayState && _newGamePending)
            {
                GotoNextLevel();
            }
            else if (CurrentState != _gameplayState && !_newGamePending)
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
