public class Action
{
    /// <summary>
    /// Threshold for critical success
    /// </summary>
    private const int CriticalSuccessValue = 20;

    /// <summary>
    /// Threshold for critical failure
    /// </summary>
    private const int CriticalFailureValue = 0;


    /// <summary>
    /// Holds the outcome of the action
    /// </summary>
    private OutcomeType _outcome;

    /// <summary>
    /// The target result for a successful outcome
    /// </summary>
    private int _targetResult = 0;

    /// <summary>
    /// The action's result
    /// </summary>
    private int _result = 0;

    /// <summary>
    /// The player attributes to apply as modifiers
    /// </summary>
    private AttributeCollection _playerAttributes = null;

    /// <summary>
    /// The number of boons to apply as a modifier
    /// </summary>
    private int _boons = 0;

    /// <summary>
    /// The number of banes to apply as a modifier
    /// </summary>
    private int _banes = 0;

    /// <summary>
    /// The results for rolled situational concerns
    /// </summary>
    private int[] _situationalConcernResults = null;


    /// <summary>
    /// Default constructor
    /// </summary>
    public Action(AttributeCollection playerAttributes, int boons, int banes)
    {
        _playerAttributes = playerAttributes;

        DetermineSituationalConcerns(boons, banes);
    }


    /// <summary>
    /// Possible outcomes of the action
    /// </summary>
    public enum OutcomeType { CriticalFailure, CriticalSuccess, Failure, Success };


    /// <summary>
    /// Returns the outcome of the action
    /// </summary>
    public OutcomeType Outcome
    {
        get { return _outcome; }
    }

    /// <summary>
    /// Returns the results rolled for situation concerns (boons or banes)
    /// </summary>
    public int[] SituationalConcernResults
    {
        get { return _situationalConcernResults; }
    }


    /// <summary>
    /// Resolves the outcome of a challenge
    /// </summary>
    /// <param name="targetResult">The target result for a successful outcome</param>
    public void Challenge(int targetResult)
    {
        _targetResult = targetResult;

        ResolveAction();
    }

    /// <summary>
    /// Resolves the outcome of an attack
    /// </summary>
    /// <param name="defense">The defence value to beat for a successful outcome</param>
    public void Attack(int defense)
    {
        _targetResult = defense;

        ResolveAction();
     }

    /// <summary>
    /// Determins the number of boons or banes to be used to modify the action's result
    /// </summary>
    /// <param name="boons">The number of boons to apply</param>
    /// <param name="banes">The number of banes to apply</param>
    private void DetermineSituationalConcerns(int boons, int banes)
    {
        if (boons == banes)
        {
            _boons = 0;
            _banes = 0;
        }
        else if(boons > banes)
        {
            _boons = boons - banes;

            CalculateSituationalConcernModifiers(_boons);
        }
        else if (banes > boons)
        {
            _banes = banes - boons;

            CalculateSituationalConcernModifiers(_banes);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void ResolveAction()
    {
        RollD20();

        ApplyPlayerAttributeModifiers();
        ApplySituationalConcernModifiers();

        CalculateOutcome();
    }

    /// <summary>
    /// Applies the result of a rolled 1D20 to the action's result
    /// </summary>
    private void RollD20()
    {
        _result = Dice.RollDie(Dice.DieType.D20);
    }

    /// <summary>
    /// Applies any player modifiers to the action's current result
    /// </summary>
    private void ApplyPlayerAttributeModifiers()
    {
        if(_playerAttributes != null)
        {
            foreach (Attribute attribute in _playerAttributes)
            {
                _result += attribute.ModifiedValue;
            }
        }
    }

    /// <summary>
    /// Applies any situational concern modifiers to the action's current result
    /// </summary>
    private void ApplySituationalConcernModifiers()
    {
        if (_boons > 0)
        {
            _result += _situationalConcernResults[0];
        }

        if (_banes > 0)
        {            
            _result -= _situationalConcernResults[0];
        }
    }

    /// <summary>
    /// Calculates the situational concerns for the specified number of dice
    /// </summary>
    /// <param name="numberOfDice">The number of dice to roll</param>
    private void CalculateSituationalConcernModifiers(int numberOfDice)
    {
        _situationalConcernResults = Dice.Roll(Dice.DieType.D6, numberOfDice, Dice.SortOrder.Descending);
    }

    /// <summary>
    /// Calculates the outcome of the action based on the result and target result values
    /// </summary>
    private void CalculateOutcome()
    {
        if (_result <= CriticalFailureValue)
        {
            _outcome = OutcomeType.CriticalFailure;
        }
        else if(_result > CriticalFailureValue && _result < _targetResult)
        {
            _outcome = OutcomeType.Failure;
        }
        else if (_result >= CriticalSuccessValue && _result >= _targetResult)
        {
            _outcome = OutcomeType.CriticalSuccess;
        }
        else if (_result >= _targetResult && _result < CriticalSuccessValue)
        {
            _outcome = OutcomeType.Success;
        }
    }
}
