public interface IState
{
    void Tick(); // Defines state action
    void OnEnter(); // Defines initial parameters
    void OnExit(); // Cleans state
}
