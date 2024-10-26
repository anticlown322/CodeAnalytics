using UI.Core;
using UI.MVVM.Models;

namespace UI.MVVM.ViewModels;

public class JilbaViewModel  : ObservableObject
{
    /* code analytics */
    private string _codeToAnalyze = "";
    private JilbaAnalysisModel _jilbaAnalysisModel;
    
    public JilbaAnalysisModel JilbaAnalysisModelEntity
    {
        get => _jilbaAnalysisModel;
        set
        {
            _jilbaAnalysisModel = value;
            OnPropertyChanged();
        }
    }
    
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
        //_jilbaAnalysisModel.StartCodeAnalysis(_codeToAnalyze);
    }

    void LoadFile(object parameter)
    {
        try
        {
            if (_dialogService.OpenFileDialog())
                _codeToAnalyze = _fileService.OpenFile(_dialogService.FilePath);
        }
        catch (Exception ex)
        {
            _dialogService.ShowMessage(ex.Message);
        }
    }
}