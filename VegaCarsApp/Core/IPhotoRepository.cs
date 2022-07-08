using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VegaCarsApp.Core.Models;

namespace VegaCarsApp.Core
{
    public interface IPhotoRepository
    {
         Task<IEnumerable<Photo>> GetPhotos(int vehicleId);
    }
}