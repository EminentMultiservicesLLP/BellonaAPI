using BellonaAPI.Models.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellonaAPI.DataAccess.Interface
{
    public interface IBrandRepository
    {
        IEnumerable<Brand> GetBrands(int? iBrandId = 0);
        bool UpdateBrand(Brand _data);
        bool DeleteBrand(int BrandId);
    }
}
