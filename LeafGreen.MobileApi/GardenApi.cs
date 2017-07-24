using LeafGreen.Entities;
using System;

namespace LeafGreen.MobileApi
{
    public class GardenApi
    {
        private readonly string _deviceId;

        public GardenApi(string deviceId)
        {
            _deviceId = deviceId;
        }

        //public async Garden GetGardenByGardenId(int gardenId)
        //{
        //    return await null;
        //}
    }
}
