using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarWars.Data
{
    public interface IDBService
    {
        Task<bool> CharacterInsert(Character characters);
        Task<IEnumerable<Character>> CharacterList();
        Task<Character> CharacterGetOne(int characterid);
        Task<bool> CharacterUpdate(Character characters);
        Task<bool> CharacterDelete(int characterid);
        
        //Episodes
        Task<IEnumerable<Episode>> GetCharacterEpisodesAppearance(int characterid);
        Task<IEnumerable<Episode>> GetAllEpisodes();

        //Friends
        Task<IEnumerable<string>> GetCharacterFriendsNames(int characterid);

    }
}