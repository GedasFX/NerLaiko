using System;
using System.Collections.Generic;
using System.Text;

namespace FridgeAPI
{
    public interface IFridge
    {
        IEnumerable<KeyValuePair<string,int>> GetActivityLog(Guid fridgeId);
    }
}
