using System;

public static class OperatorFactory 
{
    public static Operator CreateOperator(string action)
    {
        Operator operatorType;
        switch(action)
        {
            case "add":
                operatorType = new Add();
                break;
            case "mult":
                operatorType = new Multiply();
                break;
            default:
                throw new ArgumentException("Invalid parameter");
        }
        return operatorType;
    }
}

public abstract class Operator 
{
    public abstract float Update(float parameter, float value);
}

public class Multiply : Operator
{
    public override float Update(float parameter, float value)
    {
        return parameter * value;
    }
}

public class Add : Operator
{
    public override float Update(float parameter, float value)
    {
        return parameter + value;
    }
}