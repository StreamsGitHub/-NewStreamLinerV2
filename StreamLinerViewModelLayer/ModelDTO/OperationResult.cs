using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerViewModelLayer.ModelDTO
{
    public class OperationResult
    {
        public bool Success { get; set; }

        public List<string> Errors { get; set; } = new();

        public string? Message { get; set; }

        public static OperationResult Ok(string? message = null)
        {
            return new OperationResult
            {
                Success = true,
                Message = message
            };
        }

        public static OperationResult Fail(params string[] errors)
        {
            return new OperationResult
            {
                Success = false,
                Errors = errors.ToList()
            };
        }
    }
}
