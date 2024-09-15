using UI.Core;

namespace UI.MVVM.ViewModels;

public class HomeViewModel
{
    private readonly IFileService _fileService = new TextFileService();
    readonly IDialogService _dialogService = new DefaultDialogService();
    private string _codeToAnalyze = "";
    
    public RelayCommand StartAnalysisCommand { get; set; }
    public RelayCommand LoadFileCommand { get; set; }

    public HomeViewModel()
    {
        StartAnalysisCommand = new RelayCommand(StartAnalysis);
        LoadFileCommand = new RelayCommand(LoadFile);
    }

    //Better to store commands in separate files 
    void StartAnalysis(object parameter)
    {
        //работа с _codeToAnalyze
    }

    void LoadFile(object parameter)
    {
        try
        {
            if (_dialogService.OpenFileDialog() == true)
            {
                _codeToAnalyze = _fileService.OpenFile(_dialogService.FilePath);
                _dialogService.ShowMessage("Файл открыт");
            }
        }
        catch (Exception ex)
        {
            _dialogService.ShowMessage(ex.Message);
        }
    }
}