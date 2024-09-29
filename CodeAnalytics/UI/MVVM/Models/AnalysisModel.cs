using System.ComponentModel;
using System.Runtime.CompilerServices;
using CoreTypes.Classes;

namespace UI.MVVM.Models;

public class AnalysisModel : INotifyPropertyChanged
{
    private string _textToAnalyze;
    private Lexer _lexer;
    
    private HashSet<Metriс> _operandsSet;
    private HashSet<Metriс> _operatorsSet;
    private int _programDictionary;
    private int _programLength;
    private int _programVolume;

    #region Propeties for private values

    public HashSet<Metriс> OperandsSet
    {
        get => _operandsSet;
        set
        {
            _operandsSet = value;
            OnPropertyChanged();
        }
    }
    
    public HashSet<Metriс> OperatorsSet
    {
        get => _operatorsSet;
        set
        {
            _operatorsSet = value;
            OnPropertyChanged();
        }
    }
    
    public int ProgramDictionary
    {
        get => _programDictionary;
        set
        {
            _programDictionary = value;
            OnPropertyChanged();
        }
    }
    
    public int ProgramLength
    {
        get => _programLength;
        set
        {
            _programLength = value;
            OnPropertyChanged();
        }
    }
    
    public int ProgramVolume
    {
        get => _programVolume;
        set
        {
            _programVolume = value;
            OnPropertyChanged();
        }
    }
    
    #endregion
    
    /* INotifyPropertyChanged implementation */
    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
    public void StartCodeAnalysis(string codeToAnalyze)
    {
        _lexer = new Lexer(codeToAnalyze);

        OperandsSet = _lexer.GetOperandsSet();
        OperatorsSet = _lexer.GetOperatorsSet();
        
        (int, int, int) additionalMetrics = _lexer.GetTextParams(OperandsSet, OperatorsSet);

        ProgramDictionary = additionalMetrics.Item1;
        ProgramLength = additionalMetrics.Item2;
        ProgramVolume = additionalMetrics.Item3;
    }
}