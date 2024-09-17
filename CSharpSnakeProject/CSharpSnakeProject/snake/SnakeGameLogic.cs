using mySnake.shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySnake.snake
{
    public class SnakeGameLogic : BaseGameLogic
    {
        SnakeGameplayState gameplayState = new SnakeGameplayState();
        public override void Update(float deltaTime)
        {
            gameplayState.Update(deltaTime);
        }

        public override void OnArrowUp()
        {
            gameplayState.SetDirection(SnakeDir.Up);
        }

        public override void OnArrowDown()
        {
            gameplayState.SetDirection(SnakeDir.Down);
        }

        public override void OnArrowLeft()
        {
            gameplayState.SetDirection(SnakeDir.Left);
        }

        public override void OnArrowRight()
        {
            gameplayState.SetDirection(SnakeDir.Right);
        }

        public void GotoGameplay()
        {
            gameplayState.Reset();
        }
    }
}
