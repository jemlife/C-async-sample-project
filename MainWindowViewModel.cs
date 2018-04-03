using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace AsyncProject
{
    public class MainWindowViewModel : BaseViewModel
    {
        public RelayCommand GoButtonCommand { get; private set; }

        public MainWindowViewModel()
        {

            CurrentProgress = 0;
            TextBlockContent = "";
            GoButtonCommand = new RelayCommand(StartProgressBarAsync);

        }

        private async void StartProgressBarAsync()
        {
            //IsRunning = true;
            ProgressValue();
          
            var progress = new Progress<string>(value =>
            {
                TextBlockContent = value;
            });

            await Task.Run(() => HandleFileAsync(progress));
            TextBlockContent = "Completed.";
            IsRunning = false;

            //await HandleFileAsync();

            //await Task.Run(() => Thread.Sleep(30000));
            //TextBlockContent = "Step 2";

            //await Task.Run(() => Thread.Sleep(30000));
            //TextBlockContent = "Step 3";

            //await Task.Run(() => Thread.Sleep(30000));
            //TextBlockContent = "Step 4";

        }

        private void LongRunningProcess(IProgress<string> progress)
        {

            for (int i = 0; i != 100; ++i)
            {
                Thread.Sleep(3000);
                if (progress != null)
                    DoProcess(progress);
            }

            //if (progress != null)
            //{
            //    progress.Report("Step 1");
            //    Thread.Sleep(5000);

            //    progress.Report("Step 2");
            //    Thread.Sleep(5000);

            //    progress.Report("Step 3");
            //    Thread.Sleep(5000);

            //    progress.Report("Step 4");

            //}

        }

        private async Task<int> HandleFileAsync(IProgress<string> progress)
        {
            var file =
                @"C:\Users\Jem\Documents\loremipsum.txt";

            var count = 0;

            using (StreamReader reader = new StreamReader(file))
            {
                var v = await reader.ReadToEndAsync();
                count += v.Length;

                for (int i = 0; i < 100; i++)
                {
                    DoProcess(progress);
                }
            }
            TextBlockLength = "Total length: " + count;

            return count;

        }

        private void DoProcess(IProgress<string> progress)
        {
            //for (int i = 0; i != 100; i++ )
            //{
            //    TextBlockContent = "Step 1";
            //    Thread.Sleep(1000);

            //    if (progress != null && i == 20)
            //    {
            //        progress.Report("Step 2");
            //    }
            //    else if (progress != null && i == 40)
            //    {
            //        progress.Report("Step 3");
            //    }
            //    else if (progress != null && i == 60)
            //    {
            //        progress.Report("Step 4");
            //    }
            //    else if (progress != null && i == 80)
            //    {
            //        progress.Report("Step 5");
            //    }
            //    else if (progress != null && i == 100)
            //    {
            //        progress.Report("Step 6");
            //    }
            //}

            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(1000);
                PercentComplete = i;
                progress.Report(PercentComplete + "% Complete");

            }
        }

        private async  void ProgressValue()
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Thread.Sleep(1000);
                    CurrentProgress = i;

                }
            });
          
        }

        private int m_currentProgress;

        public int CurrentProgress
        {
            get { return m_currentProgress; }
            set
            {
                m_currentProgress = value;
                RaisePropertyChanged(nameof(CurrentProgress));
            }
        }

        private string m_textBlockContent;

        public string TextBlockContent
        {
            get { return m_textBlockContent; }
            set
            {
                m_textBlockContent = value;
                RaisePropertyChanged(nameof(TextBlockContent));
            }
        }

        private bool m_isRunning;

        public bool IsRunning
        {
            get { return m_isRunning; }
            set
            {
                m_isRunning = value;
                RaisePropertyChanged(nameof(IsRunning));
            }
        }

        private string m_textBlockLength;

        public string TextBlockLength
        {
            get { return m_textBlockLength; }
            set
            {
                m_textBlockLength = value;
                RaisePropertyChanged(nameof(TextBlockLength));
            }
        }

        private int m_percentComplete;
        public int PercentComplete
        {
            get { return m_percentComplete; }
            set
            {
                m_percentComplete = value;
                RaisePropertyChanged(nameof(PercentComplete));
            }
        }
    }
}
