using System;
using UnityEditor;

public abstract class CharacterMoveGameState : GameState
{
    protected IGameState defaultNextState;
    protected Action onMoveComplete;
    protected bool isMoveComplete = false;

    public CharacterMoveGameState(GameManager gameManager) : base(gameManager)
    {
        this.onMoveComplete = () => this.isMoveComplete = true;
    }

    public override void Enter()
    {
        this.isMoveComplete = false;
    }

    public override IGameState Update()
    {
        if (this.isMoveComplete)
        {
            return this.GetNextState();
        }

        return this;
    }

    protected ResultType CheckResult()
    {
        return this.gameManager.CheckResult();
    }

    protected IGameState GetNextState()
    {
        ResultType result = this.CheckResult();

        if (result == ResultType.Win)
        {
            return new WinGameState(this.gameManager);
        }
        else if (result == ResultType.Lose)
        {
            return new LoseGameState(this.gameManager);
        }

        return this.defaultNextState;
    }
}
