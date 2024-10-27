using System.ComponentModel;
using System.Runtime.CompilerServices;
using JilbaCoreTypes.Classes;

namespace UI.MVVM.Models;

public class JilbaAnalysisModel : INotifyPropertyChanged
{
    private JilbaAnalyzer _analyzer;

    private int _numOfOperators = 0;
    private int _numOfBranchStatements = 0;
    private double _relativeComplexity = 0.0;
    private int _maxNestingLevel = 0;

    #region MyRegion

    public int NumOfOperators
    {
        get => _numOfOperators;
        set
        {
            _numOfOperators = value;
            OnPropertyChanged();
        }
    }
    
    public int NumOfBranchStatements
    {
        get => _numOfBranchStatements;
        set
        {
            _numOfBranchStatements = value;
            OnPropertyChanged();
        }
    }
    
    public double RelativeComplexity
    {
        get => _relativeComplexity;
        set
        {
            _relativeComplexity = value;
            OnPropertyChanged();
        }
    }
    
    public int MaxNestingLevel
    {
        get => _maxNestingLevel;
        set
        {
            _maxNestingLevel = value;
            OnPropertyChanged();
        }
    }

    #endregion
    
    public void StartCodeAnalysis(string codeToAnalyze)
    {
        _analyzer = new JilbaAnalyzer();
        _analyzer.AnalyzeCode(codeToAnalyze);
        var analysisResult = _analyzer.OutputResult();

        NumOfOperators = analysisResult.Item1;
        NumOfBranchStatements = analysisResult.Item2;
        RelativeComplexity = analysisResult.Item3;
        MaxNestingLevel = analysisResult.Item4;
    }

    #region  INotifyPropertyChanged implementation
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion
}