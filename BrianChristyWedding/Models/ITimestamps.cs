using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrianChristyWedding.Models
{
    public interface ITimestamps
    {
        DateTime Created { get; set; }
        DateTime Updated { get; set; }
    }
}
