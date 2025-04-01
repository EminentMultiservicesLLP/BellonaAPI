using BellonaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellonaAPI.DataAccess.Interface
{
    public interface IHelpFileRepository
    {
        List<MenuList> GetAllMenuList(string userId);
        List<ManualData> GetHelpFileData(int ModuleId, int FormId);
        bool SaveHelpFileEditorData(HelpFileModel model);
    }
}
