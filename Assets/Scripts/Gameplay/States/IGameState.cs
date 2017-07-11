namespace Gameplay.States
{
    /// <summary>
    /// I Game State Interface
    /// </summary>
    public interface IGameState
    {
        void Begin();
        void Update();
        void End();
    }
}
