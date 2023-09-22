using System.Collections.Generic;
using Timesheet.Client.Utils;

namespace Timesheet.Client.Models
{
    public class ApiResponse<T>
    {
        public ResponseStatus Status { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }
    }
}
