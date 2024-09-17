using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_Utility.Commands
{
    public class AsyncRelayCommand: AsyncCommandBase
    {
        private readonly Func<Task> _callBack;

        public AsyncRelayCommand(Func<Task> callBack, Action<Exception> onException): base(onException)
        {
            _callBack = callBack;
        }

        protected override async Task ExecuteAsync(object parameter)
        {
            await _callBack();
        }
    }
}
