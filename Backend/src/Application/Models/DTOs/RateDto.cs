using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application;

public record RateDto(
    decimal Value,
    DateTime LastUpdated,
    string BaseCurrency,
    string TargetCurrency);