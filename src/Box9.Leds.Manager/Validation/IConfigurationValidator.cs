﻿using System.Threading.Tasks;
using Box9.Leds.Business.Configuration;

namespace Box9.Leds.Manager.Validation
{
    public interface IConfigurationValidator
    {
        ValidationResult Validate(LedConfiguration configuration);
    }
}
