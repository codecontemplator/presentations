using System.Collections.Generic;
using System.Threading.Tasks;

namespace mbt_lib
{
    public interface IPersonStore
    {
        Task InsertOrReplace(PersonEntity person);
        Task<IEnumerable<PersonEntity>> Search(string nationalId, string country, string eyeColor);
    }
}
