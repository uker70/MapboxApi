using System.Collections.Generic;
using MapboxApi.DTOs;

namespace MapboxApi.Data
{
    public interface IApi
    {
        IEnumerable<PinReadDTO> GetPins();
        PinReadDTO GetPin();
        PinReadDTO GetPin(int id);
        PinReadDTO GetPin(string title);
        PinReadDTO GetPin(float longitude, float latitude);

        int CreatePin(PinCreateDTO createPin);
        void UpdatePin(PinUpdateDTO updatePin);
        void DeletePin(int id);
    }
}
