using UI.Core;
using UI.MVVM.Models;

namespace UI.MVVM.ViewModels;

public class JilbaViewModel  : ObservableObject
{
    private string _codeToAnalyze = "";
    private JilbaAnalysisModel _jilbaAnalysisModel;

    #region Properties for private fields

    public JilbaAnalysisModel JilbaAnalysisModelEntity
    {
        get => _jilbaAnalysisModel;
        set
        {
            _jilbaAnalysisModel = value;
            OnPropertyChanged();
        }
    }
    
    public string CodeToAnalyze
    {
        get => _codeToAnalyze;
        set
        {
            _codeToAnalyze = value;
            OnPropertyChanged();
        }
    }

    #endregion
    
    /* utility */
    private readonly IFileService _fileService = new TextFileService();
    readonly IDialogService _dialogService = new DefaultDialogService();
    
    /* commands */
    public RelayCommand StartAnalysisCommand { get; set; }
    public RelayCommand LoadFileCommand { get; set; }
    
    public JilbaViewModel()
    {
        StartAnalysisCommand = new RelayCommand(StartAnalysis);
        LoadFileCommand = new RelayCommand(LoadFile);
        _jilbaAnalysisModel = new JilbaAnalysisModel();
    }
    
    //Better to store commands in separate files 
    void StartAnalysis(object parameter)
    {
        _jilbaAnalysisModel.StartCodeAnalysis(CodeToAnalyze);
    }

    void LoadFile(object parameter)
    {
        try
        {
            if (_dialogService.OpenFileDialog())
            {
                CodeToAnalyze = _fileService.OpenFile(_dialogService.FilePath);
            }
                
        }
        catch (Exception ex)
        {
            _dialogService.ShowMessage(ex.Message);
        }
    }
}