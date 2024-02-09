public class CharacterMoveGameState : IGameState
{
    public virtual void Enter()
    {
    }

    public virtual IGameState Update()
    {
        return this;
    }

    public virtual void Exit()
    {
    }
}
