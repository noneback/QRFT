using ShellProgressBar;
using System;

namespace QRFT.Model {

    public class MProgressBar : IDisposable {
        private const int totalTicks = 100;

        private ProgressBarOptions options = new ProgressBarOptions {
            ForegroundColor = ConsoleColor.Yellow,
            ForegroundColorDone = ConsoleColor.DarkGreen,
            BackgroundColor = ConsoleColor.DarkGray,
            BackgroundCharacter = '#',
            ProgressBarOnBottom = true,
            ProgressCharacter = '-'
        };

        public ProgressBar Pbar { get; }

        public MProgressBar(string msg, ProgressBarOptions options = null) {
            if (options != null) {
                Pbar = new ProgressBar(totalTicks, msg, this.options);
            }
            Pbar = new ProgressBar(totalTicks, msg, this.options);
        }

        public void Tick(int value) {
            if (value > 100)
                Pbar.Tick(100);
            else if (value < 0)
                Pbar.Tick(0);
            else
                Pbar.Tick(value);
        }

        public void Dispose() {
            ((IDisposable)Pbar).Dispose();
        }
    }
}