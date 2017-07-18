public class Attribute
{
    /// <summary>
    /// The base value to deduct from an attribute
    /// </summary>
    private const int AttributeBaseDeduction = 10;


    /// <summary>
    /// The type of the attribute
    /// </summary>
    private AttributeType _type;

    /// <summary>
    /// The value of the attribute
    /// </summary>
    private int _value;


    /// <summary>
    /// Default constructor
    /// </summary>
    private Attribute()
    {
        // deliberately left empty
    }

    /// <summary>
    /// Overloaded constructor
    /// </summary>
    public Attribute(AttributeType attributeType, int value)
    {
        _type = attributeType;
        _value = value;
    }


    /// <summary>
    /// Possible attribute types
    /// </summary>
    public enum AttributeType { Agility, Intellect, Strength, Will };


    /// <summary>
    /// Returns the type of the attribute
    /// </summary>
    public AttributeType Type
    {
        get { return _type; }
    }

    /// <summary>
    /// Returns the value of the attribute
    /// </summary>
    public int Value
    {
        get { return _value; }
    }

    /// <summary>
    /// Returns the modified value of the attribute
    /// </summary>
    public int ModifiedValue
    {
        get { return (_value - AttributeBaseDeduction); }
    }
}
