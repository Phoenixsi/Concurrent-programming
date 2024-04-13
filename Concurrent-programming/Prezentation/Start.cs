using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Prezentation
{
    public class Start : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        View view;

        public Start(View view)
        {
            this.view = view;
        }

        public bool CanExecute(object? parameter)
        {
            if (parameter != null)
            {
                return int.TryParse(parameter.ToString(), out int value) && value > 0;
            }
            return false;
        }


        public void PokePossibleExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Execute(object? parameter)
        {
            view.OnStartCommand();
        }
    }
}
