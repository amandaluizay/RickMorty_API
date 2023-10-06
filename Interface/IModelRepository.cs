
using Interview_API.Models;

namespace Interview_API.Intefaces
{
    public interface IModelRepository : IRepository<LogModel>
    {
        public string Upload(IFormFile file);
        public int CountTotalLocation(List<Episode> episodes);
        public int CountTotalFemaleCharacter(List<Episode> episodes);
        public int CountTotalMaleCharacter(List<Episode> episodes);
        public int CountTotalGenderlessCharacter(List<Episode> episodes);
        public int CountTotalUnknowCharacter(List<Episode> episodes);
    }
}