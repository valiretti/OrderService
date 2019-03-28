using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Model
{
    public class ExecutorPage
    {
        public IEnumerable<ExecutorPageViewModel> Executors { get; set; }

        public int TotalCount { get; set; }
    }
}
