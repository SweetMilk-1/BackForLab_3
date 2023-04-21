using BackForLab_3.Models.Dto;

namespace BackForLab_3.Services.Dictionaries
{
    public interface IDictionaryService
    {
        List<DictionaryDto> GetAuthors();
        List<DictionaryDto> GetGenres();
    }
}
