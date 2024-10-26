namespace JilbaCoreTypes.Classes;

/// <summary>
/// 
/// </summary>
public class JilbaAnalyzer
{
    private string? _codeToAnalyze;
    private string? _cleanCode;
    private int _numOfOperators = 0;
    private int _numOfBranchStatements = 0;
    private double _relativeComplexity = 0.0;
    private int _maxNestingLevel = 0;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="codeToAnalyze"></param>
    public void AnalyzeCode(string codeToAnalyze)
    {
        _codeToAnalyze = codeToAnalyze;
        _cleanCode = JilbaUtility.RemoveComments(codeToAnalyze);
        _cleanCode = JilbaUtility.ClearStrings(_cleanCode);

        _numOfOperators = JilbaUtility.CountOperators(_cleanCode);
        _numOfBranchStatements = JilbaUtility.CountBranchingStatements(_cleanCode);
        _relativeComplexity = JilbaUtility.CalcRelativeComplexity(_numOfBranchStatements, _numOfOperators);
        _maxNestingLevel = JilbaUtility.CountMaxNestingLevel(_cleanCode);
    }

    public (int, int, double, int) OutputResult()
    {
        return (_numOfOperators, _numOfBranchStatements, _relativeComplexity, _maxNestingLevel);
    }
}