using System.ComponentModel;

namespace MultithreadingExamples.WpfApp.Infrastructure
{
    public interface IViewModel : INotifyPropertyChanged
    {
        void Loaded();
    }
}