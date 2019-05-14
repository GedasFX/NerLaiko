using System;

namespace FridgeAPI
{
    public interface IFridge
    {
        string GetFridgeContents(Guid fridgeId, string token);
    }
}
