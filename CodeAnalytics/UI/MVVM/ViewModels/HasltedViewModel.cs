using UI.Core;
using UI.MVVM.Models;

namespace UI.MVVM.ViewModels;

public class HasltedViewModel : ObservableObject
{
    /* code analytics */
    private string _codeToAnalyze = "";
    private HasltedAnalysisModel _hasltedAnalysisModel;

    public HasltedAnalysisModel HasltedAnalysisModelEntity
    {
        get => _hasltedAnalysisModel;
        set
        {
            _hasltedAnalysisModel = value;
            OnPropertyChanged();
        }
    }

    /* utility */
    private readonly IFileService _fileService = new TextFileService();
    readonly IDialogService _dialogService = new DefaultDialogService();
    
    /* commands */
    public RelayCommand StartAnalysisCommand { get; set; }
    public RelayCommand LoadFileCommand { get; set; }
    

    public HasltedViewModel()
    {
        StartAnalysisCommand = new RelayCommand(StartAnalysis);
        LoadFileCommand = new RelayCommand(LoadFile);
        _hasltedAnalysisModel = new HasltedAnalysisModel();
    }

    //Better to store commands in separate files 
    void StartAnalysis(object parameter)
    {
        _hasltedAnalysisModel.StartCodeAnalysis(_codeToAnalyze);
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