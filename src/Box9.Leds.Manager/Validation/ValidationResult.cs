using System.Collections.Generic;
using System.Linq;

namespace Box9.Leds.Manager.Validation
{
    public class ValidationResult
    {
        public IEnumerable<string> Errors { get; private set; }

        public bool OK { get; private set; }

        public ValidationResult(IEnumerable<string> errors = null)
        {
            OK = errors == null || !errors.Any();

            if (OK)
            {
                Errors = new List<string>();
            }
            else
            {
                Errors = errors;
            }
        }
    }
}
