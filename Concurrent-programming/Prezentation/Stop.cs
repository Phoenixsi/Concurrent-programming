using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Prezentation
{
    public class Stop : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        View view;

        public Stop(View view)
        {
            this.view = view;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            this.view.OnStopCommand();
        }
    }
}
