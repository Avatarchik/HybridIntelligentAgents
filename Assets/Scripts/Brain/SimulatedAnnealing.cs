using System;
using Brain;

public class SimulatedAnnealing
{
  private double _temperature;
  private readonly double _coolingRate;
  private readonly double[] _parameters;
  private double _currentFitness;

  public double[] Solution { get; }
  public double Fitness { get; private set; }

  public SimulatedAnnealing(double[] initialParameters, double initialFitness, double initialTemperature,
    double coolingRate)
  {
    _parameters = initialParameters;
    Solution = initialParameters;
    _currentFitness = initialFitness;
    Fitness = initialFitness;
    _temperature = initialTemperature;
    _coolingRate = coolingRate;
  }

  public void Step(double[] neighbourSolution, double neighbourFitness)
  {
    if (neighbourFitness > _currentFitness &&
        Math.Exp((_currentFitness - neighbourFitness) / _temperature) > Utility.RandomDouble()) {
      for (var i = 0; i < Solution.Length; i++) {
        _parameters[i] = neighbourSolution[i];
      }
      _currentFitness = neighbourFitness;
    }

    if (_currentFitness > Fitness) {
      Fitness = _currentFitness;
      for (var i = 0; i < Solution.Length; i++) {
        Solution[i] = neighbourSolution[i];
      }
    }

    _temperature *= 1.0 - _coolingRate;
  }
}