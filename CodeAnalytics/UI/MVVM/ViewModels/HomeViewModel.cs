using UI.Core;
using UI.MVVM.Models;

namespace UI.MVVM.ViewModels;

public class HomeViewModel : ObservableObject
{
    /* code analytics */
    private string _codeToAnalyze = "";
    private AnalysisModel _analysisModel;

    public AnalysisModel AnalysisModelEntity
    {
        get => _analysisModel;
        set
        {
            _analysisModel = value;
            OnPropertyChanged();
        }
    }

    /* utility */
    private readonly IFileService _fileService = new TextFileService();
    readonly IDialogService _dialogService = new DefaultDialogService();
    
    /* commands */
    public RelayCommand StartAnalysisCommand { get; set; }
    public RelayCommand LoadFileCommand { get; set; }
    

    public HomeViewModel()
    {
        StartAnalysisCommand = new RelayCommand(StartAnalysis);
        LoadFileCommand = new RelayCommand(LoadFile);
        _analysisModel = new AnalysisModel();
    }

    //Better to store commands in separate files 
    void StartAnalysis(object parameter)
    {
        _analysisModel.StartCodeAnalysis(_codeToAnalyze);
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