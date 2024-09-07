namespace Assets.Scripts.Enemies.StateMachine
{
    /// <summary>
    /// Animation state. The same parameter controls animation transitions.
    /// </summary>
    public enum EnemyState
    {
        Idle,
        Ready,
        Walk,
        Run,
        Jump,
        Die,
        Block
    }
}