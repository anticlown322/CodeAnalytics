using System.Text.RegularExpressions;

namespace JilbaCoreTypes.Classes;

/// <summary>
/// 
/// </summary>
public static class JilbaUtility
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="code"></param>
    /// <param name="pattern"></param>
    /// <returns></returns>
    public static string RemoveComments(string code)
    { 
        string pattern = @"(//.*)|(/\*.*?\*/)|(/\*.*)";
        return Regex.Replace(code, pattern, "");
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public static string ClearStrings(string code)
    { 
        string pattern = "^.s*$";
        return Regex.Replace(code, pattern, "");
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public static int CountOperators(string code)
    {
        int operatorCounter = 0;

        
        
        return operatorCounter;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public static int CountBranchingStatements(string code)
    {
        int branchingCounter = 0;

        return branchingCounter;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="branchingStatements"></param>
    /// <param name="operators"></param>
    /// <returns></returns>
    public static double CalcRelativeComplexity(int branchingStatements, int operators)
    {
        return 0 == operators ? 0 : branchingStatements / (double)operators; //double to not loss the fraction
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public static int CountMaxNestingLevel(string code)
    {
        int maxNestingLevel = 0;

        return maxNestingLevel;
    }
}