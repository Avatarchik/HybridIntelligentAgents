public interface IReinforcementLearning : ILearningMethod
{
  int StateCount { get; }
  int ActionCount { get; }
  int CurrentState { get; }
  int SelectedAction { get; }
  double Fitness { get; }

  /// <summary>
  ///   Begin learning process.
  /// </summary>
  /// <param name="state">Initial state.</param>
  void Begin(int state);

  /// <summary>
  ///   Perform learning step.
  /// </summary>
  /// <param name="reward">Gained reward.</param>
  /// <param name="nextState">Entered state after performing action.</param>
  void Step(double reward, int nextState);
}
