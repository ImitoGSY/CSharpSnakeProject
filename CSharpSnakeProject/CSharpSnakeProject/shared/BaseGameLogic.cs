using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSnakeProject.shared

{
    public abstract class BaseGameLogic : ConsoleInput.IArrowListener
    {
        protected BaseGameState? CurrentState { get; private set; }
        protected float Time { get; private set; }
        protected int ScreenWidth { get; private set; }
        protected int ScreenHeight { get; private set; }
        public abstract void OnArrowUp();
        public abstract void OnArrowDown();
        public abstract void OnArrowLeft();
        public abstract void OnArrowRight();

        public abstract void Update(float deltaTime);
        public abstract ConsoleColor[] CreatePalette();

        protected void ChangeState(BaseGameState? state)
        {
            CurrentState?.Reset();
            CurrentState = state;
        }

        public void DrawNewState(float deltaTime, ConsoleRenderer renderer)
        {
            Time += deltaTime;
            ScreenWidth = renderer.Width;
            ScreenHeight = renderer.Height;

            CurrentState?.Update(deltaTime);
            CurrentState?.Draw(renderer);

            Update(deltaTime);
        }

        public void InitializeInput(ConsoleInput input)
        {
            input.Subscribe(this);
        }




    }
}
