using Antlr4.Runtime;

namespace JilbaCoreTypes.Classes;

/// <summary>
/// Анализатор кода Scala по метрикам Джилба.
/// </summary>
public class JilbaAnalyzer
{
    private string? _codeToAnalyze;
    private int _numOfOperators;
    private int _numOfBranchStatements;
    private double _relativeComplexity;
    private int _maxNestingLevel;

    /// <summary>
    /// Основная функция. Запускает анализ кода. 
    /// </summary>
    /// <param name="codeToAnalyze">Код на Scala, который необходимо проанализировать</param>
    public void AnalyzeCode(string codeToAnalyze)
    {
        _codeToAnalyze = codeToAnalyze;
        const int _lastOperatorNumber = 61; //это номер последнего из типов токенов, принадлежащих операторам
        int currNestingLevel = 0;
        
        //get tokens
        AntlrInputStream inputStream = new AntlrInputStream(_codeToAnalyze);
        ScalaLexer lexer = new ScalaLexer(inputStream);
        CommonTokenStream tokens = new CommonTokenStream(lexer);
        tokens.Fill();
        IList<IToken> tokenList = tokens.GetTokens();
        
        //operators + branching + nesting
        foreach (IToken token in tokenList)
        {
            int tokenType = token.Type;

            if (tokenType <= _lastOperatorNumber)
                _numOfOperators++;
            
            if (ScalaLexer.IF == tokenType 
                || ScalaLexer.CASE == tokenType 
                || ScalaLexer.FOR == tokenType
                || ScalaLexer.FOR_SOME == tokenType
                || ScalaLexer.DO == tokenType
                || ScalaLexer.WHILE == tokenType)
                _numOfBranchStatements++;

            if (ScalaLexer.OPN_BRKT == tokenType)
                currNestingLevel++;

            if (ScalaLexer.CLS_BRKT == tokenType)
                currNestingLevel--;
            
            if (currNestingLevel > _maxNestingLevel)
                _maxNestingLevel = currNestingLevel;
        }

        //complexity
        _relativeComplexity = Math.Round(_numOfBranchStatements / (float)_numOfOperators, 5);
    }
    
    /// <summary>
    /// Возвращает метрики Джилба. Использовать только после того, как код был проанализирован.
    /// </summary>
    /// <returns>
    /// Кортеж из 4ёх метрик: <br/>
    /// - кол-ов операторов, <br/>
    /// - абсолютная сложность, <br/>
    /// - относительная сложность, <br/>
    /// - максимальная вложженность. 
    /// </returns>
    public (int, int, double, int) OutputResult()
    {
        return (_numOfOperators, _numOfBranchStatements, _relativeComplexity, _maxNestingLevel);
    }
}